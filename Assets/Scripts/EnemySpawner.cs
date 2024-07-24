using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] List<WaveConfigSO> waveConfigs;
    [SerializeField] float timeBetweenWaves = 1f;
    [SerializeField] bool isLooping = true;
    [SerializeField] bool inUI = true;
    WaveConfigSO currentWave;
    EnemyAttributes enemyAttributes;

    

    void Awake() 
    {
        enemyAttributes = FindAnyObjectByType<EnemyAttributes>();
    }

    void Start()
    {
        Options.instance.UpdateEnemyStats();
        StartCoroutine(SpawnWaves());
    }

    IEnumerator SpawnWaves()
    {
        do 
        {
            for (int waveIndex = 0; waveIndex < waveConfigs.Count; waveIndex++)
            {
                currentWave = waveConfigs[waveIndex];
                for(int i = 0; i < currentWave.GetEnemyCount(); i++)
                {
                    Instantiate(currentWave.GetEnemyPrefab(i), currentWave.GetStartWaypoint().position, Quaternion.Euler(0,0,180), transform );
                    yield return new WaitForSeconds(currentWave.GetRandomSpawnTime());
                }
                yield return new WaitForSeconds(timeBetweenWaves);
            
                if (waveIndex == waveConfigs.Count - 1 && !inUI)
                {
                    enemyAttributes.IncreaseHealthMultiplier(1.2f);
                }
            }

        }
        while(isLooping);
        
    } 

    public WaveConfigSO GetCurrentWave()
    {
        return currentWave;
    }

    public float GetTimeBetweenWaves()
    {
        return timeBetweenWaves;
    }
    public void ChangeTimeBetweenWaves(float newSpawnTime)
    {
        timeBetweenWaves = newSpawnTime;
    }
}
