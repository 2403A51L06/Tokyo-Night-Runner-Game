using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public Text scoreText;
    public Text coinText;
    public GameObject gameOverPanel;
    private float score;
    private int coins;
    public bool IsPlaying { get; private set; } = true;

    void Awake() { Instance = this; }

    void Update()
    {
        if (!IsPlaying) return;
        score += Time.deltaTime * 10f;
        if (scoreText) scoreText.text = "SCORE  " + Mathf.FloorToInt(score);
        if (Input.GetKeyDown(KeyCode.R) && !IsPlaying) Restart();
    }

    public void AddCoin()
    {
        coins++;
        if (coinText) coinText.text = "COINS  " + coins;
    }

    public void EndGame()
    {
        IsPlaying = false;
        if (gameOverPanel) gameOverPanel.SetActive(true);
        Time.timeScale = 0f;
    }

    public void Restart()
    {
        Time.timeScale = 1f;
        UnityEngine.SceneManagement.SceneManager.LoadScene(
            UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex);
    }
}
