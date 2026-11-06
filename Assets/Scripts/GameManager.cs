
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public Text scoreText;
    public Text coinText;
    public GameObject gameOverPanel;

    private float score;
    private int coins;

    public bool IsPlaying { get; private set; } = true;

    void Awake()
    {
        Instance = this;
        Time.timeScale = 1f;
    }

    void Update()
    {
        // Check restart even after game over
        if (!IsPlaying)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                Restart();
            }

            return;
        }

        score += Time.deltaTime * 10f;

        if (scoreText != null)
        {
            scoreText.text = "SCORE  " + Mathf.FloorToInt(score);
        }
    }

    public void AddCoin()
    {
        coins++;

        if (coinText != null)
        {
            coinText.text = "COINS  " + coins;
        }
    }

    public void EndGame()
    {
        if (!IsPlaying) return;

        IsPlaying = false;

        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(true);
        }

        Time.timeScale = 0f;
    }

    public void Restart()
    {
        Time.timeScale = 1f;

        string sceneName = SceneManager.GetActiveScene().name;

        SceneManager.LoadScene(sceneName);
    }
}
