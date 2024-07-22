using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [SerializeField] float sceneLoadDelay = 1f;

    [SerializeField] GameObject mainMenueCanvas;
    [SerializeField] GameObject highScoreCanvas;
    [SerializeField] GameObject optionCanvas;
    ScoreKeeper scoreKeeper;

    void Awake() 
    {
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
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
        Debug.Log("Quitted Game");
        Application.Quit();
    }
    IEnumerator WaitAndLoad(int LoadScene, float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(LoadScene);
    }

    public void OpenOption()
    {
        optionCanvas.gameObject.SetActive(true);
        highScoreCanvas.gameObject.SetActive(false);
        mainMenueCanvas.gameObject.SetActive(false);

    }
    public void OpenHighscore()
    {
        optionCanvas.gameObject.SetActive(false);
        highScoreCanvas.gameObject.SetActive(true);
        mainMenueCanvas.gameObject.SetActive(false);
    }
    public void BackToMenue()
    {
        optionCanvas.gameObject.SetActive(false);
        highScoreCanvas.gameObject.SetActive(false);
        mainMenueCanvas.gameObject.SetActive(true);
    }

}
