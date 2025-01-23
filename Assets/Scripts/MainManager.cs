using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainManager : MonoBehaviour
{
    public Brick BrickPrefab;
    public int LineCount = 6;
    public Rigidbody Ball;

    public Text ScoreText;

    // 1. Reference to the HighScore Text UI
    public Text HighScoreText;

    public GameObject GameOverText;

    private bool m_Started = false;
    private int m_Points;

    private bool m_GameOver = false;

    void Start()
    {
        // 2. Show the current best score (from PlayerPrefs via GameManager)
        if (GameManager.Instance != null)
        {
            int bestScore = GameManager.Instance.GetHighScore();
            string bestPlayer = GameManager.Instance.GetHighScorePlayerName();

            // Display "Best Score: PlayerName : Score"
            HighScoreText.text = $"Best Score : {bestPlayer} : {bestScore}";
        }

        // Rest of your existing brick setup
        const float step = 0.6f;
        int perLine = Mathf.FloorToInt(4.0f / step);

        int[] pointCountArray = new[] { 1, 1, 2, 2, 5, 5 };
        for (int i = 0; i < LineCount; ++i)
        {
            for (int x = 0; x < perLine; ++x)
            {
                Vector3 position = new Vector3(-1.5f + step * x, 2.5f + i * 0.3f, 0);
                var brick = Instantiate(BrickPrefab, position, Quaternion.identity);
                brick.PointValue = pointCountArray[i];
                brick.onDestroyed.AddListener(AddPoint);
            }
        }
    }

    private void Update()
    {
        if (!m_Started)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                m_Started = true;
                float randomDirection = Random.Range(-1.0f, 1.0f);
                Vector3 forceDir = new Vector3(randomDirection, 1, 0);
                forceDir.Normalize();

                Ball.transform.SetParent(null);
                Ball.AddForce(forceDir * 2.0f, ForceMode.VelocityChange);
            }
        }
        else if (m_GameOver)
        {
            // Restart on space press
            if (Input.GetKeyDown(KeyCode.Space))
            {
                // (Optional) If you want to save the high score
                // only once the player hits space:
                // GameManager.Instance.SaveHighScore(m_Points, GameManager.Instance.PlayerName);

                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
    }

    void AddPoint(int point)
    {
        m_Points += point;
        ScoreText.text = $"Score : {m_Points}";
    }

    public void GameOver()
    {
        m_GameOver = true;
        GameOverText.SetActive(true);

        // 3. Check if new score is higher than the saved high score
        if (GameManager.Instance != null)
        {
            int currentHighScore = GameManager.Instance.GetHighScore();

            if (m_Points > currentHighScore)
            {
                // 4. Save new high score
                GameManager.Instance.SaveHighScore(m_Points, GameManager.Instance.PlayerName);

                // 5. Update the UI text immediately (optional)
                HighScoreText.text = $"Best Score : {GameManager.Instance.PlayerName} : {m_Points}";
            }
        }
    }
}
