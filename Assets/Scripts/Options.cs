using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class Options : MonoBehaviour
{
    [SerializeField] private float enemyHealth;
    [SerializeField] private float timeBetweenWaves;
    [SerializeField] private float enemyFireRate;
    [SerializeField] private float timeBetweenEnemy;

    [SerializeField] private List<GameObject> enemyPrefabs;
    [SerializeField] private TMP_Dropdown healthDropdown;
    [SerializeField] private TMP_Dropdown fireRateDropdown;
    [SerializeField] private TMP_Dropdown waveRateDropdown;
    [SerializeField] private TMP_Dropdown spawnRateDropdown;
    [SerializeField] private TextMeshProUGUI scoreMultiplierText;

    private float scoreMultiplierHealth = 1f;
    private float scoreMultiplierWave = 1f;
    private float scoreMultiplierSpawn = 1f;
    private float scoreMultiplierFireRate = 1f;

    public static Options instance;

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

    public void DropdownValueChangedHealth(TMP_Dropdown dropdown)
    {
        switch (dropdown.value)
        {
            case 0:
                enemyHealth = 0.8f;
                scoreMultiplierHealth = 0.6f;
                break;
            case 1:
                enemyHealth = 1f;
                scoreMultiplierHealth = 1f;
                break;
            case 2:
                enemyHealth = 1.3f;
                scoreMultiplierHealth = 1.4f;
                break;
            default:
                enemyHealth = 1f;
                scoreMultiplierHealth = 1f;
                break;
        }
        Debug.Log("Enemy health ready to updated to: " + enemyHealth);
    }

    public void DropdownValueChangedFireRate(TMP_Dropdown dropdown)
    {
        switch (dropdown.value)
        {
            case 0:
                enemyFireRate = 1.2f;
                scoreMultiplierFireRate = 0.6f;
                break;
            case 1:
                enemyFireRate = 1f;
                scoreMultiplierFireRate = 1f;
                break;
            case 2:
                enemyFireRate = 0.8f;
                scoreMultiplierFireRate = 1.4f;
                break;
            default:
                enemyFireRate = 1f;
                scoreMultiplierFireRate = 1f;
                break;
        }
        Debug.Log("Enemy firerate ready to updated to: " + enemyFireRate);
    }

    public void DropdownValueChangedSpawnTime(TMP_Dropdown dropdown)
    {
        switch (dropdown.value)
        {
            case 0:
                timeBetweenWaves = 2f;
                scoreMultiplierWave = 0.8f;
                break;
            case 1:
                timeBetweenWaves = 1f;
                scoreMultiplierWave = 1f;
                break;
            case 2:
                timeBetweenWaves = 0;
                scoreMultiplierWave = 1.2f;
                break;
            default:
                timeBetweenWaves = 1f;
                scoreMultiplierWave = 1f;
                break;
        }
        Debug.Log("Wave spawn time ready to update to: " + timeBetweenWaves);
    }

    public void DropdownValueChangedTimeBetweenEnemys(TMP_Dropdown dropdown)
    {
        switch (dropdown.value)
        {
            case 0:
                timeBetweenEnemy = 1.3f;
                scoreMultiplierSpawn = 0.8f;
                break;
            case 1:
                timeBetweenEnemy = 1f;
                scoreMultiplierSpawn = 1f;
                break;
            case 2:
                timeBetweenEnemy = 0.8f;
                scoreMultiplierSpawn = 1.3f;
                break;
            default:
                timeBetweenEnemy = 1f;
                scoreMultiplierSpawn = 1f;
                break;
        }
        Debug.Log("Enemy spawn time ready to update to: " + timeBetweenEnemy);
    }

    public void UpdateEnemyStats()
    {
        UpdateEnemyHealth();
        UpdateEnemyFireRate();
        UpdateEnemySpawner();
        UpdateScoreMultiplier();
    }

    private void UpdateEnemyHealth()
    {
        for(int i = 0; i < enemyPrefabs.Count; i++)
        {
            Health healthScript = enemyPrefabs[i].GetComponent<Health>();
            float currentHealth = healthScript.GetHealth();
            float additionalHealth = currentHealth * enemyHealth;
            healthScript.AddEnemyHealth(additionalHealth);
            Debug.Log("--prefab" + i + " health updated " + additionalHealth);
        }
    }

    private void UpdateEnemyFireRate()
    {
        for(int i = 0; i < enemyPrefabs.Count; i++)
        {
            Shooter shooterScript = enemyPrefabs[i].GetComponent<Shooter>();
            float currentAttackspeed = shooterScript.GetEnemyAttackSpeed();
            float additionalAttackspeed = currentAttackspeed * enemyFireRate;
            shooterScript.AddEnemyAttackSpeed(additionalAttackspeed);
            Debug.Log("--prefab" + i +" attackspeed updated" + additionalAttackspeed);
        }
    }

    private void UpdateEnemySpawner()
    {
        EnemySpawner enemySpawner = FindObjectOfType<EnemySpawner>();
        enemySpawner.ChangeTimeBetweenWaves(timeBetweenWaves);
        Debug.Log("--Wave spawn time updated to " + timeBetweenWaves);

        enemySpawner.ChangeTimeBetweenEnemies(timeBetweenEnemy);
        Debug.Log("--Enemy spawn time updated to " + timeBetweenEnemy);
    }

    private void UpdateScoreMultiplier()
    {
        float totalMultiplier = scoreMultiplierFireRate * scoreMultiplierHealth * scoreMultiplierWave * scoreMultiplierSpawn;
        float roundedMultiplier = (float)Math.Round(totalMultiplier, 2);
        ScoreKeeper.instance.UpdateScoreMultiplier(roundedMultiplier);
        scoreMultiplierText.text = "Score x" + roundedMultiplier;
        Debug.Log("--Score multiplier updated to " + roundedMultiplier);
    }
}