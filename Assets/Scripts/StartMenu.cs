using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class StartMenu : MonoBehaviour
{
    [SerializeField] private TMP_InputField nameInput;
    [SerializeField] private TextMeshProUGUI bestScoreText;

    private void Start()
    {
        UpdateBestScoreText();
    }

    private void UpdateBestScoreText()
    {
        bestScoreText.text = $"Best Score : {DataManager.Instance.BestPlayerName} : {DataManager.Instance.BestScore}";
    }

    public void StartGame()
    {
        string playerName = nameInput.text;
        if (!string.IsNullOrEmpty(playerName))
        {
            DataManager.Instance.SetPlayerName(playerName);
            SceneManager.LoadScene(1); // Main game scene
        }
    }

    public void ShowHighScores()
    {
        SceneManager.LoadScene(2);
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}