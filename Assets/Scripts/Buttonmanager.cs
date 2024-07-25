using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Buttonmanager : MonoBehaviour
{
    [SerializeField] private Button button;

    void Start()
    {
        AssignButtons();
    }

    void OnEnable()
    {
        AssignButtons();
    }

    private void AssignButtons()
    {
        // Überprüfe, ob die Instanz von LevelManager vorhanden ist
        if (LevelManager.instance != null)
        {
            // Entferne alle vorhandenen Listener vom Button
            button.onClick.RemoveAllListeners();

            // Füge den passenden Listener basierend auf dem Namen des Buttons hinzu
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
                    Debug.LogWarning($"No method assigned for button: {button.name}");
                    break;
            }
        }
        else
        {
            // Fehlerprotokollierung, wenn die LevelManager-Instanz nicht gefunden wird
            Debug.LogError("LevelManager Singleton not found!");
        }
    }
}
