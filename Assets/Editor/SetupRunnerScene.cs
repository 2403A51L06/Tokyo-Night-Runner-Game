#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using System.IO;

[InitializeOnLoad]
public static class SetupRunnerScene
{
    static SetupRunnerScene()
    {
        EditorApplication.delayCall += EnsureScene;
    }

    static void EnsureScene()
    {
        const string scenePath = "Assets/Scenes/Main.unity";
        if (File.Exists(scenePath)) return;
        Directory.CreateDirectory("Assets/Scenes");

        Scene scene = EditorSceneManager.NewScene(NewSceneSetup.EmptyScene, NewSceneMode.Single);
        RenderSettings.ambientLight = new Color(0.45f, 0.48f, 0.58f);
        GameObject lightObj = new GameObject("Directional Light");
        Light light = lightObj.AddComponent<Light>();
        light.type = LightType.Directional;
        light.intensity = 1.2f;
        lightObj.transform.rotation = Quaternion.Euler(45, -30, 0);

        GameObject player = GameObject.CreatePrimitive(PrimitiveType.Capsule);
        player.name = "Player";
        player.transform.position = new Vector3(0, 1f, 2f);
        player.GetComponent<Renderer>().sharedMaterial = MakeMaterial(new Color(0.15f, 0.65f, 1f));
        Collider old = player.GetComponent<Collider>();
        Object.DestroyImmediate(old);
        CharacterController cc = player.AddComponent<CharacterController>();
        cc.height = 2f; cc.radius = 0.45f; cc.center = new Vector3(0, 1f, 0);
        player.AddComponent<RunnerController>();

        GameObject camObj = new GameObject("Main Camera");
        Camera cam = camObj.AddComponent<Camera>();
        cam.tag = "MainCamera";
        camObj.AddComponent<AudioListener>();
        FollowCamera follow = camObj.AddComponent<FollowCamera>();
        follow.target = player.transform;
        camObj.transform.position = new Vector3(0, 5.5f, -6f);

        GameObject managerObj = new GameObject("Game Manager");
        GameManager manager = managerObj.AddComponent<GameManager>();
        GameObject spawnerObj = new GameObject("World Spawner");
        WorldSpawner spawner = spawnerObj.AddComponent<WorldSpawner>();
        spawner.player = player.transform;

        GameObject canvasObj = new GameObject("Game UI", typeof(Canvas), typeof(CanvasScaler), typeof(GraphicRaycaster));
        Canvas canvas = canvasObj.GetComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        CanvasScaler scaler = canvasObj.GetComponent<CanvasScaler>();
        scaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
        scaler.referenceResolution = new Vector2(1920, 1080);

        manager.scoreText = MakeText(canvasObj.transform, "Score", "SCORE  0", new Vector2(0.12f, 0.92f), 30);
        manager.coinText = MakeText(canvasObj.transform, "Coins", "COINS  0", new Vector2(0.88f, 0.92f), 30);

        GameObject panel = new GameObject("Game Over Panel", typeof(RectTransform), typeof(Image));
        panel.transform.SetParent(canvasObj.transform, false);
        RectTransform pr = panel.GetComponent<RectTransform>();
        pr.anchorMin = new Vector2(0.25f, 0.3f); pr.anchorMax = new Vector2(0.75f, 0.7f);
        pr.offsetMin = Vector2.zero; pr.offsetMax = Vector2.zero;
        panel.GetComponent<Image>().color = new Color(0.02f, 0.03f, 0.08f, 0.9f);
        Text over = MakeText(panel.transform, "GameOverText", "GAME OVER\nPress R to Restart", new Vector2(0.5f, 0.55f), 42);
        over.alignment = TextAnchor.MiddleCenter;
        panel.SetActive(false);
        manager.gameOverPanel = panel;

        GameObject eventObj = new GameObject("EventSystem", typeof(EventSystem), typeof(StandaloneInputModule));
        EditorSceneManager.SaveScene(scene, scenePath);
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
        Debug.Log("Tokyo-free Endless Runner starter scene created at " + scenePath);
    }

    static Material MakeMaterial(Color color)
    {
        Shader shader = Shader.Find("Standard");
        if (shader == null) shader = Shader.Find("Universal Render Pipeline/Lit");
        Material mat = new Material(shader);
        mat.color = color;
        return mat;
    }

    static Text MakeText(Transform parent, string name, string value, Vector2 anchor, int size)
    {
        GameObject obj = new GameObject(name, typeof(RectTransform), typeof(Text));
        obj.transform.SetParent(parent, false);
        RectTransform rt = obj.GetComponent<RectTransform>();
        rt.anchorMin = anchor; rt.anchorMax = anchor;
        rt.pivot = new Vector2(0.5f, 0.5f);
        rt.sizeDelta = new Vector2(500, 90);
        rt.anchoredPosition = Vector2.zero;
        Text text = obj.GetComponent<Text>();
        text.text = value; text.font = Resources.GetBuiltinResource<Font>("LegacyRuntime.ttf");
        text.fontSize = size; text.color = Color.white;
        text.alignment = TextAnchor.MiddleCenter;
        return text;
    }
}
#endif
