using UnityEngine;
using System.Collections.Generic;

public class WorldSpawner : MonoBehaviour
{
    public Transform player;
    public float segmentLength = 30f;
    public int segmentsAhead = 5;
    public float laneDistance = 2.5f;
    private readonly List<GameObject> spawned = new List<GameObject>();
    private int nextSegment;

    void Start()
    {
        for (int i = 0; i < segmentsAhead; i++) SpawnSegment(i);
        nextSegment = segmentsAhead;
    }

    void Update()
    {
        if (!player) return;
        while (nextSegment * segmentLength < player.position.z + segmentsAhead * segmentLength)
            SpawnSegment(nextSegment++);
        for (int i = spawned.Count - 1; i >= 0; i--)
        {
            if (spawned[i] && spawned[i].transform.position.z + segmentLength < player.position.z - 15f)
            {
                Destroy(spawned[i]);
                spawned.RemoveAt(i);
            }
        }
    }

    void SpawnSegment(int index)
    {
        GameObject root = new GameObject("Track Segment " + index);
        root.transform.position = new Vector3(0, 0, index * segmentLength);
        spawned.Add(root);

        GameObject road = GameObject.CreatePrimitive(PrimitiveType.Cube);
        road.name = "Road";
        road.transform.SetParent(root.transform);
        road.transform.localPosition = new Vector3(0, -0.25f, segmentLength * 0.5f);
        road.transform.localScale = new Vector3(8.5f, 0.5f, segmentLength);
        road.GetComponent<Renderer>().material.color = new Color(0.12f, 0.14f, 0.19f);

        for (int side = -1; side <= 1; side += 2)
        {
            GameObject rail = GameObject.CreatePrimitive(PrimitiveType.Cube);
            rail.transform.SetParent(root.transform);
            rail.transform.localPosition = new Vector3(side * 4.35f, 0.2f, segmentLength * 0.5f);
            rail.transform.localScale = new Vector3(0.25f, 0.5f, segmentLength);
            rail.GetComponent<Renderer>().material.color = new Color(0.1f, 0.75f, 0.95f);
        }

        if (index < 2) return;
        for (int row = 0; row < 3; row++)
        {
            int lane = Random.Range(0, 3);
            float z = 7f + row * 8f;
            if (Random.value < 0.75f) CreateObstacle(root.transform, (lane - 1) * laneDistance, z);
            int coinLane = Random.Range(0, 3);
            for (int c = 0; c < 4; c++) CreateCoin(root.transform, (coinLane - 1) * laneDistance, z + 2f + c * 1.5f);
        }
    }

    void CreateObstacle(Transform parent, float x, float z)
    {
        GameObject obj = GameObject.CreatePrimitive(PrimitiveType.Cube);
        obj.name = "Barrier";
        obj.transform.SetParent(parent);
        obj.transform.localPosition = new Vector3(x, 0.8f, z);
        obj.transform.localScale = new Vector3(1.5f, 1.6f, 0.8f);
        obj.GetComponent<Renderer>().material.color = new Color(1f, 0.25f, 0.2f);
        Collider col = obj.GetComponent<Collider>();
        col.isTrigger = true;
        obj.AddComponent<Obstacle>();
    }

    void CreateCoin(Transform parent, float x, float z)
    {
        GameObject obj = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        obj.name = "Coin";
        obj.transform.SetParent(parent);
        obj.transform.localPosition = new Vector3(x, 1.1f, z);
        obj.transform.localScale = Vector3.one * 0.55f;
        obj.GetComponent<Renderer>().material.color = new Color(1f, 0.8f, 0.05f);
        obj.GetComponent<Collider>().isTrigger = true;
        obj.AddComponent<Coin>();
    }
}
