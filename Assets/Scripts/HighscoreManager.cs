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
        public HighscoreEntry [] highscores;
   }


   [Serializable]
   public class HighscoreEntry
   {
    public string Name;
    public int Score;
   }

    private const string FileName ="highscore.json";
    private const int MaxHighscores = 8;
    private string HighscoreFilePath => Path.Combine(Application.persistentDataPath, FileName);
    private List <HighscoreEntry> _highscore = new List<HighscoreEntry>();
    static HighscoreManager instance;

   void Awake() 
    {
        ManageSingleton();
        Load();
    }
        void ManageSingleton()
    {
        if(instance != null)
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
    
    void OnDestroy() 
    {
        Save();
    }

    private void Save()
    {
        var highscoreContainer = new HighscoreContainer()
        {
            highscores = _highscore.ToArray()
        };
    
        string json = JsonUtility.ToJson(highscoreContainer);
        File.WriteAllText(HighscoreFilePath, json);
    }

    private void Load()
    {
        if(!File.Exists(HighscoreFilePath))
        {
            return;
        }

        string json = File.ReadAllText(HighscoreFilePath);
        var highscoreContainer = JsonUtility.FromJson<HighscoreContainer>(json);

        if (highscoreContainer == null || highscoreContainer.highscores == null)
        {
            return;
        }

        _highscore = new List<HighscoreEntry>(highscoreContainer.highscores);
    }

    private void Add(HighscoreEntry entry)
    {
        _highscore.Add(entry);
        _highscore = _highscore.OrderByDescending(e => e.Score).Take(MaxHighscores).ToList();
    }
    
    public bool IsNewHighscore(int score)
    {
        if (score <= 0)
        {
            return false;
        }

        if (_highscore.Count < MaxHighscores)
        {
            return true;
        }

        return score > _highscore.Last().Score;
    }
    
    public void Add(string playerName, int score)
    {
        if (!IsNewHighscore(score))
        {
            return;
        }

        var entry = new HighscoreEntry()
        {
            Name = playerName,
            Score = score
        };

        Add(entry);
    }

    public List<HighscoreEntry> List()
    {
        return _highscore;
    }
}
