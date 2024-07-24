using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class ScoreKeeper : MonoBehaviour
{
    [SerializeField] float currentScore = 0;
    [SerializeField] GameObject[] powerUps;
    int enemyKills =0;
    [SerializeField] float multip = 1f;

    public static ScoreKeeper instance;
    void Awake() 
    {
        ManageSingleton();
    }
    void ManageSingleton()
    {
        if(instance != null)
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
    public float GetScore()
    {
        return currentScore;
    }

    public void AddScore(int value)
    {
       currentScore = currentScore +(value * multip);
       Mathf.Clamp (currentScore, 0, int.MaxValue);
       SpawnPowerUps();
    }

    public void UpdateScoreMultiplier(float multiplier)
    {
        multip = multiplier;
    }

    void SpawnPowerUps()
    {
        enemyKills += 1;
       if (IsMultipleOf(enemyKills, 10))
       {
            int randomIndex = Random.Range(0, powerUps.Length);
            GameObject p = Instantiate(powerUps[randomIndex], transform.position, Quaternion.identity);
            Rigidbody2D rigidbody2D = p.GetComponent<Rigidbody2D>();
            if (rigidbody2D != null)
            {
                rigidbody2D.velocity = transform.up * -4;
            }
       }
    }

    public void ResetScore()
    {
        currentScore = 0;
        enemyKills = 0;
    }

     bool IsMultipleOf(int number, int divisor)
    {
        if (divisor == 0)
        {
            Debug.LogError("Der Divisor darf nicht null sein.");
            return false;
        }
        return number % divisor == 0;
    }


}
