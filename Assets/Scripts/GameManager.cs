using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager Instance
    {
        get
        {
            // If an instance doesn’t exist yet, create one
            if (_instance == null)
            {
                // Create a new GameObject to hold the singleton
                GameObject managerObject = new GameObject("GameManager");
                _instance = managerObject.AddComponent<GameManager>();
                DontDestroyOnLoad(managerObject);
            }
            return _instance;
        }
    }

    // The current player's name (set by StartMenuUI)
    public string PlayerName { get; set; }

    private const string HighScoreKey = "HighScore";
    private const string HighScoreNameKey = "HighScoreName";

    // When the manager is initialized
    private void Awake()
    {
        // Make sure only one instance of GameManager exists
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    // Retrieve the saved high score
    public int GetHighScore()
    {
        return PlayerPrefs.GetInt(HighScoreKey, 0);
    }

    // Retrieve the name associated with the high score
    public string GetHighScorePlayerName()
    {
        return PlayerPrefs.GetString(HighScoreNameKey, "None");
    }

    // Save a new high score and the player's name
    public void SaveHighScore(int score, string playerName)
    {
        PlayerPrefs.SetInt(HighScoreKey, score);
        PlayerPrefs.SetString(HighScoreNameKey, playerName);
        PlayerPrefs.Save();
    }
}
