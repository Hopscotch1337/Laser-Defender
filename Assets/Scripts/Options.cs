using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Options : MonoBehaviour
{
    [SerializeField] float enemyHealth = 1f;
    [SerializeField] float timeBetweenWaves = 0f; 
    [SerializeField] float enemyFireRate = 1f;

    [SerializeField] List<GameObject> enemyPrefabs;
    [SerializeField] private TMP_Dropdown healthDropdown;
    [SerializeField] private TMP_Dropdown fireRateDropdown;
    [SerializeField] private TMP_Dropdown spawnRateDropdown;
    [SerializeField] ScoreKeeper scoreKeeper;

    float scoreMultiplierHealth = 1f;
    float scoreMultiplierSpawn = 1f;
    float scoreMultiplierFireRate = 1f;

    static Options instance;

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


 public void DropdownValueChangedHealth(TMP_Dropdown dropdown)
    {
        switch (dropdown.value)
        {
            case 0:
                enemyHealth = 0.7f;
                scoreMultiplierHealth = 0.5f;
                break;
            case 1:
                enemyHealth = 1f;
                scoreMultiplierHealth = 1f;
                break;
            case 2:
                enemyHealth = 1.3f;
                scoreMultiplierHealth = 1.5f;
                break;
            default:
                enemyHealth = 1f;
                scoreMultiplierHealth = 0.5f;
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
                scoreMultiplierFireRate = 0.5f;
                break;
            case 1:
                enemyFireRate = 1f;
                scoreMultiplierFireRate = 1f;
                break;
            case 2:
                enemyFireRate = 0.8f;
                scoreMultiplierFireRate = 1.5f;
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
                scoreMultiplierSpawn = 0.8f;
                break;
            case 1:
                timeBetweenWaves = 1f;
                scoreMultiplierSpawn = 1f;
                break;
            case 2:
                timeBetweenWaves = 0;
                scoreMultiplierSpawn = 1.2f;
                break;
            default:
                timeBetweenWaves = 1f;
                scoreMultiplierSpawn = 1f;
                break;
        }
        Debug.Log("Enemy spawn time ready to update to: " + timeBetweenWaves);
    }

    public void UpdateEnemyStats()
    {
        for(int i = 0; i < enemyPrefabs.Count; i++)
        {
            Health healthScript = enemyPrefabs[i].GetComponent<Health>();
            float currentHealth = healthScript.GetHealth();
            float additionalHealth = currentHealth * enemyHealth;
            healthScript.AddEnemyHealth(additionalHealth);
            Debug.Log("--prefab" + i + " health updated " + additionalHealth);
        }
         for(int i = 0; i < enemyPrefabs.Count; i++)
        {
            Shooter shooterScript = enemyPrefabs[i].GetComponent<Shooter>();
            float currentAttackspeed = shooterScript.GetEnemyAttackspeed();
            float additionalAttackspeed = currentAttackspeed * enemyFireRate;
            shooterScript.AddEnemyAttackSpeed(additionalAttackspeed);
            Debug.Log("--prefab" + i +" attackspeed updated" + additionalAttackspeed);
        }
        EnemySpawner enemySpawner = FindObjectOfType<EnemySpawner>();
        enemySpawner.ChangeTimeBetweenWaves(timeBetweenWaves);
        Debug.Log("--enemy spawn time updated to " + timeBetweenWaves);

        ScoreKeeper scoreKeeper = FindObjectOfType<ScoreKeeper>();
        scoreKeeper.UpdateScoreMultiplier(scoreMultiplierFireRate * scoreMultiplierHealth * scoreMultiplierSpawn);
    }

    
}
