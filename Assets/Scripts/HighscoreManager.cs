using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine.SocialPlatforms.Impl;

public class HighscoreManager : MonoBehaviour
{
    [Serializable]
    public class HighscoreContainer
    {
        public HighscoreEntry[] highscores;
    }

    [Serializable]
    public class HighscoreEntry
    {
        public string Name;
        public int Score;
    }

    private const string FileName = "highscore.json";
    private const int MaxHighscores = 8;
    private string HighscoreFilePath => Path.Combine(Application.persistentDataPath, FileName);
    private List<HighscoreEntry> highscore = new List<HighscoreEntry>();
    private static HighscoreManager instance;

    private void Awake() 
    {
        Load();
    }


    
    private void OnDestroy() 
    {
        Save();
    }

    public void Save()
    {
        var highscoreContainer = new HighscoreContainer { highscores = highscore.ToArray() };
        string json = JsonUtility.ToJson(highscoreContainer);
        File.WriteAllText(HighscoreFilePath, json);
    }

    private void Load()
    {
        if (!File.Exists(HighscoreFilePath))
            return;

        string json = File.ReadAllText(HighscoreFilePath);
        var highscoreContainer = JsonUtility.FromJson<HighscoreContainer>(json);

        if (highscoreContainer?.highscores != null)
        {
            highscore = new List<HighscoreEntry>(highscoreContainer.highscores);
        }
    }

    private void Add(HighscoreEntry entry)
    {
        highscore.Add(entry);
        highscore = highscore.OrderByDescending(e => e.Score).Take(MaxHighscores).ToList();
    }
    
    public bool IsNewHighscore(int score)
    {
        if (score <= 0)
        {
            return false;
        }

        if (highscore.Count < MaxHighscores)
        {
            return true;
        }

        return score > highscore.Last().Score;
    }
    
    public void Add(string playerName, int score)
    {
        if (!IsNewHighscore(score))
        {
            return;
        }

        var entry = new HighscoreEntry { Name = playerName, Score = score };
        Add(entry);
    }

    public List<HighscoreEntry> List()
    {
        return new List<HighscoreEntry>(highscore);
    }
}