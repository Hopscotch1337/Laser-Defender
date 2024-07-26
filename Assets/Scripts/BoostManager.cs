using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoostManager : MonoBehaviour
{
    [SerializeField] private GridLayoutGroup gridLayoutGroup;
    private List<Button> buttons;
    private int currentIndex = 0;

    void Start()
    {
        // Alle Buttons im GridLayout finden und in eine Liste speichern
        buttons = new List<Button>(gridLayoutGroup.GetComponentsInChildren<Button>());
        if (buttons.Count > 0)
        {
            HighlightButton(buttons[currentIndex]);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            RotateToNextButton();
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            if (buttons.Count > 0)
            {
                // Funktion des aktuellen Buttons ausführen
                buttons[currentIndex].onClick.Invoke();

                // Button zerstören und aus der Liste entfernen
                Destroy(buttons[currentIndex].gameObject);
                buttons.RemoveAt(currentIndex);

                // Zum nächsten Button rotieren, wenn es noch weitere Buttons gibt
                if (buttons.Count > 0)
                {
                    currentIndex %= buttons.Count;
                    HighlightButton(buttons[currentIndex]);
                }
                else
                {
                    currentIndex = 0; // Zurücksetzen des Index, wenn keine Buttons mehr vorhanden sind
                }
            }
        }
    }

    private void HighlightButton(Button button)
    {
        // Hier kannst du Logik hinzufügen, um den aktuellen Button hervorzuheben
        // Zum Beispiel, indem du seine Farbe änderst
        ColorBlock colorBlock = button.colors;
        colorBlock.normalColor = Color.green; // Hervorhebung durch Gelb
        button.colors = colorBlock;
    }

    private void UnhighlightButton(Button button)
    {
        // Logik zum Entfernen der Hervorhebung
        ColorBlock colorBlock = button.colors;
        colorBlock.normalColor = Color.white; // Standardfarbe ist Weiß
        button.colors = colorBlock;
    }

    private void RotateToNextButton()
    {
        if (buttons.Count > 0)
        {
            UnhighlightButton(buttons[currentIndex]);
            currentIndex = (currentIndex + 1) % buttons.Count;
            HighlightButton(buttons[currentIndex]);
        }
    }
}