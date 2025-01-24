using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class HighScoreUI : MonoBehaviour
{
    [SerializeField] private Transform scoreContainer;
    [SerializeField] private GameObject scoreEntryPrefab;
    [SerializeField] private Button backButton;
    [SerializeField] private ScrollRect scrollRect;

    void Start()
    {
        PopulateHighScores();
        backButton.onClick.AddListener(() => SceneManager.LoadScene(0));

        if (scrollRect != null)
        {
            var contentRT = scoreContainer.GetComponent<RectTransform>();
            var viewportRT = scrollRect.viewport.GetComponent<RectTransform>();
            contentRT.sizeDelta = new Vector2(viewportRT.rect.width, contentRT.sizeDelta.y);
        }
    }

    void PopulateHighScores()
    {
        var highScores = DataManager.Instance.HighScores;
        for (int i = 0; i < highScores.Count; i++)
        {
            GameObject entry = Instantiate(scoreEntryPrefab, scoreContainer);
            TextMeshProUGUI[] texts = entry.GetComponentsInChildren<TextMeshProUGUI>();
            if (texts.Length >= 2)
            {
                texts[0].text = $"{i + 1}. {highScores[i].playerName}";  
                texts[1].text = $"{highScores[i].score}";                
            }
        }
    }
}

