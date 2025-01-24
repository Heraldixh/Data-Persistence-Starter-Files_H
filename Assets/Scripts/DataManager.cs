using System.Linq;
using UnityEngine;
using System.IO;
using System.Collections.Generic;


public class DataManager : MonoBehaviour
{
    public static DataManager Instance { get; private set; }
    public string PlayerName { get; private set; }
    public string BestPlayerName { get; private set; }
    public int BestScore { get; private set; }
    private List<PlayerScore> highScores = new List<PlayerScore>();
    public IReadOnlyList<PlayerScore> HighScores => highScores;

    [System.Serializable]
    public class PlayerScore
    {
        public string playerName;
        public int score;
    }

    [System.Serializable]
    class SaveData
    {
        public string bestPlayerName;
        public int bestScore;
        public List<PlayerScore> highScores = new List<PlayerScore>();
    }

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
        LoadHighScore();
    }

    public void SetPlayerName(string name)
    {
        PlayerName = name;
    }

    public void UpdateHighScore(int score)
    {
        if (score > BestScore)
        {
            BestScore = score;
            BestPlayerName = PlayerName;
            SaveHighScore();
        }
    }

    public void AddScore(int score)
    {
        highScores.Add(new PlayerScore { playerName = PlayerName, score = score });
        highScores = highScores.OrderByDescending(x => x.score).ToList();

        if (highScores.Count > 10)
        {
            highScores = highScores.Take(10).ToList();
        }

        UpdateHighScore(score);
        SaveHighScore();
    }

    public void SaveHighScore()
    {
        SaveData data = new SaveData
        {
            bestPlayerName = BestPlayerName,
            bestScore = BestScore,
            highScores = highScores
        };
        string json = JsonUtility.ToJson(data);
        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }

    public void LoadHighScore()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);
            BestPlayerName = data.bestPlayerName;
            BestScore = data.bestScore;
            highScores = data.highScores ?? new List<PlayerScore>();
        }
    }
}