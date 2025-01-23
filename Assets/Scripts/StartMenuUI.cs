using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartMenuUI : MonoBehaviour
{
    [Header("UI References")]
    public InputField playerNameInput;  // or TMP_InputField if using TextMeshPro
    public Button startButton;

    private void Start()
    {
        // Add a listener for the Start button
        startButton.onClick.AddListener(OnStartButtonClicked);
    }

    private void OnStartButtonClicked()
    {
        // 1. Get the player’s name from the input field
        string playerName = playerNameInput.text;

        // 2. Store it in a global manager or a static variable
        GameManager.Instance.PlayerName = playerName;

        // 3. Load the main game scene
        SceneManager.LoadScene("main");
    }
}