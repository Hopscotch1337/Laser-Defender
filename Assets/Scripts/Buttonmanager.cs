using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Buttonmanager : MonoBehaviour
{
    [SerializeField] Button button;

    void Start()
    {
        AssignButtons();
    }

    void OnEnable()
    {
        AssignButtons();
    }
    

    void AssignButtons()
    {
        if (LevelManager.instance != null)
        {
            button.onClick.RemoveAllListeners();
            switch (button.name)
            {
                case "Restart":
                case "StartButton":
                    button.onClick.AddListener(LevelManager.instance.LoadGame);
                    break;
                case "MainMenue":
                    button.onClick.AddListener(LevelManager.instance.LoadMainMenu);
                    break;    
                case "BackButton":
                    button.onClick.AddListener(LevelManager.instance.BackToMenu);
                    break;
                case "Options":
                    button.onClick.AddListener(LevelManager.instance.OpenOptions);
                    break; 
                case "Highscore":
                    button.onClick.AddListener(LevelManager.instance.OpenHighscore);
                    break; 
                case "EndButton":
                    button.onClick.AddListener(LevelManager.instance.QuitGame);
                    break;
                default:
                    Debug.LogWarning("No method assigned for button: " + button.name);
                    break;
            } 
        }
        // else
        // {
        //     Debug.LogError("LevelManager Singleton not found!");
        // }
    }

}
