using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyAttributes : MonoBehaviour
{
    [SerializeField] private float healthMultiplier = 1f;

    public static EnemyAttributes instance;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    // Erhöht den Gesundheitsmultiplikator um den angegebenen Betrag
    public void IncreaseHealthMultiplier(float amount)
    {
        healthMultiplier *= amount;
    }

    // Gibt den aktuellen Gesundheitsmultiplikator zurück
    public float GetHealthMultiplier()
    {
        return healthMultiplier;
    }

    // Setzt den Gesundheitsmultiplikator auf den Standardwert zurück
    public void ResetHealthMultiplier()
    {
        healthMultiplier = 1f;
    }
}