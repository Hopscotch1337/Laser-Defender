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
    [SerializeField] HighscoreManager highscoreManager;
    [SerializeField] GameObject   highscoreEntries;
    [SerializeField] GameObject highscoreEntryUiPrefab;
    [SerializeField] GameObject optionCanvas;
    [SerializeField] Options options;
    
    private ScoreKeeper scoreKeeper;
    
    
    
    void Awake() 
    {
        scoreKeeper = FindObjectOfType<ScoreKeeper>();

    }



    public void LoadGame()
    {
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
        options.UpdateEnemyStats();
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
       // ActivateAllChildren(highScoreCanvas.transform);
    }

    public void BackToMenu()
    {
        Options options = optionCanvas.GetComponent<Options>();
        options.UpdateEnemyStats();
        optionCanvas.SetActive(false);
        highScoreCanvas.SetActive(false);
        mainMenuCanvas.SetActive(true);
    }

    private void ShowHighscores()
    {
        // Entfernt alle Kindobjekte von highscoreEntries
        for (int i = highscoreEntries.transform.childCount - 1; i >= 0; i--)
        {
            Transform child = highscoreEntries.transform.GetChild(i);
            Destroy(child.gameObject);
            Debug.Log("old highscores destroyed " + i);
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

    // private void ActivateAllChildren(Transform parent)
    // {
    //     foreach (Transform child in parent)
    //     {
    //         child.gameObject.SetActive(true);
    //         Debug.Log("Activating child: " + child.name);
    //         ActivateAllChildren(child); // Rekursiv alle Kinder aktivieren
    //     }
    // }


}
