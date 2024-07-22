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
    [SerializeField] GameObject mainMenuCanvas;
    [SerializeField] GameObject highScoreCanvas;
    [SerializeField] GameObject optionCanvas;
    [SerializeField] HighscoreManager highscoreManager;
    [SerializeField] GameObject highscoreEntries;
    [SerializeField] GameObject highscoreEntryUiPrefab;
    
    private ScoreKeeper scoreKeeper;
    static LevelManager instance;

    void Awake() 
    {
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
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

    public void LoadGame()
    {
        scoreKeeper.ResetScore();
        SceneManager.LoadScene(1);
    }

    public void LoadGameOver()
    {
        StartCoroutine(WaitAndLoad(2, sceneLoadDelay));
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene(0);
    }
    
    public void QuitGame()
    {
        Debug.Log("Quitting game...");
        Application.Quit();
    }

    private IEnumerator WaitAndLoad(int sceneIndex, float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(sceneIndex);
    }

    public void OpenOptions()
    {
        optionCanvas.SetActive(true);
        highScoreCanvas.SetActive(false);
        mainMenuCanvas.SetActive(false);
    }

    public void OpenHighscore()
    {
        optionCanvas.SetActive(false);
        highScoreCanvas.SetActive(true);
        mainMenuCanvas.SetActive(false);
        ShowHighscores();
    }

    public void BackToMenu()
    {
        optionCanvas.SetActive(false);
        highScoreCanvas.SetActive(false);
        mainMenuCanvas.SetActive(true);
    }

    private void ShowHighscores()
    {
        // Entfernt alle Kindobjekte von HighscoreEntries
        for (int i = highscoreEntries.transform.childCount - 1; i >= 0; i--)
        {
            Transform child = highscoreEntries.transform.GetChild(i);
            Destroy(child.gameObject);
        }

        // Holt die Highscore-Liste vom HighscoreManager
        List<HighscoreEntry> highscoreEntries = highscoreManager.List();

        // Erstellt UI-Elemente für jeden Highscore-Eintrag
        foreach (HighscoreEntry entry in highscoreList)
        {
            GameObject entryUI = Instantiate(highscoreEntryUiPrefab, highscoreEntries.transform);
            HighscoreEntryUI entryUIComponent = entryUI.GetComponent<HighscoreEntryUI>();

            if (entryUIComponent != null)
            {
                entryUIComponent.SetName(entry.Name);
                entryUIComponent.SetScore(entry.Score);
            }
            else
            {
                Debug.LogError("HighscoreEntryUI component is missing on the prefab.");
            }
        }
    }
}