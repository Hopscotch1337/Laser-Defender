using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class ScoreKeeper : MonoBehaviour
{
    [SerializeField] private float currentScore = 0f;
    [SerializeField] private GameObject[] powerUps;
    [SerializeField] private float multip = 1f;

    private int enemyKills = 0;

    public static ScoreKeeper instance;

    private void Awake()
    {
        ManageSingleton();
    }

    private void ManageSingleton()
    {
        if (instance != null)
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
        currentScore += value * multip;
        currentScore = Mathf.Clamp(currentScore, 0, float.MaxValue);
        SpawnPowerUps();
    }

    public void UpdateScoreMultiplier(float multiplier)
    {
        multip = multiplier;
    }

    public void ResetScore()
    {
        currentScore = 0;
        enemyKills = 0;
    }

    private void SpawnPowerUps()
    {
        enemyKills++;
        if (IsMultipleOf(enemyKills, 10))
        {
            int randomIndex = Random.Range(0, powerUps.Length);
            GameObject powerUp = Instantiate(powerUps[randomIndex], transform.position, Quaternion.identity);
            Rigidbody2D rb = powerUp.GetComponent<Rigidbody2D>();

            if (rb != null)
            {
                rb.velocity = transform.up * -4f;
            }
        }
    }

    private bool IsMultipleOf(int number, int divisor)
    {
        if (divisor == 0)
        {
            Debug.LogError("Divisor cannot be zero.");
            return false;
        }
        return number % divisor == 0;
    }
}