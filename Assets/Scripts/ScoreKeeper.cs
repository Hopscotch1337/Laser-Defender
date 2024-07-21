using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class ScoreKeeper : MonoBehaviour
{
    [SerializeField] int currentScore = 0;
    [SerializeField] GameObject[] powerUps;
    static ScoreKeeper instance;
    int enemyKills =0;


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
    public int GetScore()
    {
        return currentScore;
    }

    public void AddScore(int value)
    {
       currentScore += value;
       enemyKills += 1;
       Mathf.Clamp (currentScore, 0, int.MaxValue);
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
