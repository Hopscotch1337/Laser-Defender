using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyAttributes : MonoBehaviour
{
    [SerializeField] float healthMultiplier = 1f;
    //public float shieldMultiplier = 1f;
    static EnemyAttributes instance;

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

    public void IncreaseHealthMultiplier(float amount)
    {
        healthMultiplier *= amount;
    }
    public float GetHealthMultiplier()
    {
        return healthMultiplier;
    }


}
