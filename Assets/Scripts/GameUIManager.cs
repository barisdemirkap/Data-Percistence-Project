using UnityEngine;
using UnityEngine.UI;

public class GameUIManager : MonoBehaviour
{
    [Header("UI References")]
    public Text currentPlayerNameText;
    public Text currentScoreText;
    public Text highScoreText;
    public Text highScoreNameText;

    // This is just a placeholder for demonstration.
    // Replace with however you actually increase or track score in-game.
    private int currentScore = 0;

    private void Start()
    {
        // Display the player's name from GameManager
        currentPlayerNameText.text = "Player: " + GameManager.Instance.PlayerName;

        // Load high score data from GameManager (which uses PlayerPrefs internally)
        highScoreText.text = "High Score: " + GameManager.Instance.GetHighScore().ToString();
        highScoreNameText.text = "By: " + GameManager.Instance.GetHighScorePlayerName();
    }

    private void Update()
    {
        // For demo, increase score constantly; in a real game, you'd do this elsewhere
        currentScore++;
        currentScoreText.text = "Score: " + currentScore.ToString();

        // Check if current score beats the high score
        if (currentScore > GameManager.Instance.GetHighScore())
        {
            GameManager.Instance.SaveHighScore(currentScore, GameManager.Instance.PlayerName);

            // Update UI
            highScoreText.text = "High Score: " + currentScore.ToString();
            highScoreNameText.text = "By: " + GameManager.Instance.PlayerName;
        }
    }
}
