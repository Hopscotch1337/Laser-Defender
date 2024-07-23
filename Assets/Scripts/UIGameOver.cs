using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIGameOver : MonoBehaviour
{
    int score;
    ScoreKeeper scoreKeeper;
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] HighscoreManager highscoreManager;
    [SerializeField] TextMeshProUGUI newHighscore;
    [SerializeField] TMP_InputField inputName;
    [SerializeField] GameObject saveButton;
    


    void Awake() 
    {
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
        newHighscore.gameObject.SetActive(false);
        inputName.gameObject.SetActive(false);
        saveButton.gameObject.SetActive(false);
    }
    void Start()
    {
        scoreText.text = scoreKeeper.GetScore().ToString("00000000");
        bool isNewHighscore = highscoreManager.IsNewHighscore((int)scoreKeeper.GetScore());
        newHighscore.gameObject.SetActive(isNewHighscore);
        inputName.gameObject.SetActive(isNewHighscore);
        saveButton.gameObject.SetActive(isNewHighscore);
    }

    public void AddToHighscore()
    {
        string playerName = inputName.text;
        if (!string.IsNullOrWhiteSpace(playerName))
        {
            highscoreManager.Add(playerName, (int)scoreKeeper.GetScore());
            newHighscore.gameObject.SetActive(false);
            inputName.gameObject.SetActive(false);
            saveButton.gameObject.SetActive(false);
            highscoreManager.Save();

        }
    }

}
