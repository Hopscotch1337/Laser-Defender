using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System;

public class LevelManager : MonoBehaviour
{
    [SerializeField] float sceneLoadDelay = 1f;
    [SerializeField] GameObject highscoreEntryUiPrefab;


    GameObject optionCanvas;
    GameObject highScoreCanvas;
    GameObject mainMenuCanvas;

    GameObject highscoreEntries;
    HighscoreManager highscoreManager;

    public static LevelManager instance;

    void Awake() 
    {
        ManageSingleton();
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
    void Start()
    {
        optionCanvas = FindDeactivatedGameObjectByName("OptionCanvas");
        highScoreCanvas = FindDeactivatedGameObjectByName("HighScoreCanvas");
        mainMenuCanvas = FindDeactivatedGameObjectByName("MenuCanvas");
        highscoreManager = FindObjectOfType<HighscoreManager>();
        highscoreEntries = highScoreCanvas.transform.Find("HighscoreEntries").gameObject;
    }

    GameObject FindDeactivatedGameObjectByName(string name)
    {
        Transform[] allTransforms = Resources.FindObjectsOfTypeAll<Transform>();
        foreach (Transform t in allTransforms)
        {
            if (t.gameObject.name == name)
            {
                return t.gameObject;
            }
        }
        return null;
    }

    public void LoadGame()
    {
        Options.instance.UpdateEnemyStats();
        ScoreKeeper.instance.ResetScore();
        SceneManager.LoadScene(1);
    }

    public void LoadGameOver()
    {
        EnemyAttributes.instance.ResetHealthMultiplier();
        StartCoroutine(WaitAndLoad(2, sceneLoadDelay));
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene(0);
    }
    
    public void QuitGame()
    {
        Application.Quit();
    }

    private IEnumerator WaitAndLoad(int sceneIndex, float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(sceneIndex);
    }

    public void OpenOptions()
    {
        optionCanvas = FindDeactivatedGameObjectByName("OptionCanvas");
        highScoreCanvas = FindDeactivatedGameObjectByName("HighScoreCanvas");
        mainMenuCanvas = FindDeactivatedGameObjectByName("MenuCanvas"); 
        optionCanvas.transform.GetChild(0).gameObject.SetActive(true);
        optionCanvas.transform.GetChild(1).gameObject.SetActive(true);
        highScoreCanvas.SetActive(false);
        mainMenuCanvas.SetActive(false);
        
    }

    public void OpenHighscore()
    {
        highscoreManager = FindObjectOfType<HighscoreManager>();
        highScoreCanvas = FindDeactivatedGameObjectByName("HighScoreCanvas"); 
        optionCanvas = FindDeactivatedGameObjectByName("OptionCanvas");
        mainMenuCanvas = FindDeactivatedGameObjectByName("MenuCanvas"); 
        optionCanvas.transform.GetChild(0).gameObject.SetActive(false);
        optionCanvas.transform.GetChild(1).gameObject.SetActive(false);
        highScoreCanvas.SetActive(true);
        mainMenuCanvas.SetActive(false);
        ShowHighscores();
       // ActivateAllChildren(highScoreCanvas.transform);
    }

    public void BackToMenu()
    {
        Options.instance.UpdateEnemyStats();
        optionCanvas.transform.GetChild(0).gameObject.SetActive(false);
        optionCanvas.transform.GetChild(1).gameObject.SetActive(false);
        highScoreCanvas.SetActive(false);
        mainMenuCanvas.SetActive(true);
    }

    private void ShowHighscores()
    {
        highscoreEntries = highScoreCanvas.transform.Find("HighscoreEntries").gameObject;
        // Entfernt alle Kindobjekte von highscoreEntries
        for (int i = highscoreEntries.transform.childCount - 1; i >= 0; i--)
        {
            Transform child = highscoreEntries.transform.GetChild(i);
            Destroy(child.gameObject);
            Debug.Log("old highscores destroyed on list entry " + i);
        }
        
        var highscores = highscoreManager.List();
        foreach (var highscore in highscores)
        {
            var entry = Instantiate(highscoreEntryUiPrefab, highscoreEntries.transform);
            SetHighscoreEntryDetails(entry, highscore);
            Debug.Log("Highscore entry created: " + highscore.Name + " - " + highscore.Score);
        }
    }

    private void SetHighscoreEntryDetails(GameObject entry, HighscoreManager.HighscoreEntry highscore)
    {
        // Assuming your prefab has Text components for displaying the name and score
        var nameText = entry.transform.Find("NameText").GetComponent<TMP_Text>();
        var scoreText = entry.transform.Find("ScoreText").GetComponent<TMP_Text>();

        if (nameText != null && scoreText != null)
        {
            nameText.text = highscore.Name;
            scoreText.text = highscore.Score.ToString();
        }
        else
        {
            Debug.LogError("NameText or ScoreText component missing in highscore entry prefab.");
        }
    }
}
