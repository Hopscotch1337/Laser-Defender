using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DamageDealer : MonoBehaviour
{
    [SerializeField] private int damage = 10;

    // Gibt den aktuellen Schaden zurück und gibt eine Debug-Nachricht aus
    public int GetDamage()
    {
        Debug.Log($"{damage} damage done");
        return damage;
    }

    // Zerstört das GameObject, an das dieses Skript angehängt ist
    public void Hit()
    {
        Destroy(gameObject);
    }

    // Erhöht den Schaden um den angegebenen Wert und gibt den neuen Schaden aus
    public void AddDamage(int additionalDamage)
    {
        damage += additionalDamage;
        Debug.Log($"New damage value: {damage}");
    }
}