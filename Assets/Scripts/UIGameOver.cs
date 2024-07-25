using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIGameOver : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private HighscoreManager highscoreManager;
    [SerializeField] private TextMeshProUGUI newHighscoreText;
    [SerializeField] private TMP_InputField inputNameField;
    [SerializeField] private GameObject saveButton;

    private int currentScore;

    private void Awake()
    {
        HideHighscoreInput();
    }

    private void Start()
    {
        // Retrieve and display current score
        currentScore = (int)ScoreKeeper.instance.GetScore();
        scoreText.text = currentScore.ToString("00000000");

        // Check if the current score is a new high score
        bool isNewHighscore = highscoreManager.IsNewHighscore(currentScore);
        newHighscoreText.gameObject.SetActive(isNewHighscore);
        inputNameField.gameObject.SetActive(isNewHighscore);
        saveButton.SetActive(isNewHighscore);
    }

    public void AddToHighscore()
    {
        string playerName = inputNameField.text;
        if (!string.IsNullOrWhiteSpace(playerName))
        {
            // Add the new high score
            highscoreManager.Add(playerName, currentScore);
            // Hide input fields and button
            HideHighscoreInput();
            // Save the updated high scores
            highscoreManager.Save();
        }
    }

    private void HideHighscoreInput()
    {
        newHighscoreText.gameObject.SetActive(false);
        inputNameField.gameObject.SetActive(false);
        saveButton.SetActive(false);
    }
}