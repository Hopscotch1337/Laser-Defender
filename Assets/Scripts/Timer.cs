using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI timerText; // Verweise auf das Text-UI-Element, das den Timer anzeigt
    private float elapsedTime;
    private int minutes;
    private int seconds;

    void Update()
    {
        elapsedTime += Time.deltaTime; // Zeit seit dem letzten Frame hinzufügen
        minutes = (int)(elapsedTime / 60); // Ganze Minuten berechnen
        seconds = (int)(elapsedTime % 60); // Übrig gebliebene Sekunden berechnen

        // Aktualisiere den Text des UI-Elements
        timerText.text = string.Format("Time: {00:00}:{01:00}", minutes, seconds);
    }
}