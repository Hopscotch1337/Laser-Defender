using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("Wave Settings")]
    [SerializeField] private List<WaveConfigSO> waveConfigs;
    [SerializeField] private float timeBetweenWaves = 1f;
    [SerializeField] private float timeBetweenEnemyMultiplier = 1f;

    [Header("UI Elements")]
    [SerializeField] private TextMeshProUGUI levelDisplay;
    [SerializeField] private TextMeshProUGUI waveDisplay;

    [Header("Game Settings")]
    [SerializeField] private bool isLooping = true;
    [SerializeField] private bool isUI;

    private WaveConfigSO currentWave;
    private int waveCount = 0;
    private int levelCount = 0;

    private void Start()
    {
        // Aktualisiert die Feindeigenschaften und startet die Wellen-Coroutine
        Options.instance.UpdateEnemyStats();
        StartCoroutine(GetSpawnWaves());
    }

    private IEnumerator GetSpawnWaves()
    {
        do
        {
            // Aktualisiert die Level-Anzeige, wenn nicht im UI-Modus
            if (!isUI)
            {
                levelCount++;
                levelDisplay.text = $"Level: {levelCount}";
            }

            // Durchläuft jede Welle und spawnt die Feinde
            for (int waveIndex = 0; waveIndex < waveConfigs.Count; waveIndex++)
            {
                if (!isUI)
                {
                    waveCount++;
                    waveDisplay.text = $"Wave: {waveCount}";
                }

                currentWave = waveConfigs[waveIndex];
                for (int i = 0; i < currentWave.GetEnemyCount(); i++)
                {
                    Instantiate(
                        currentWave.GetEnemyPrefab(i),
                        currentWave.GetStartWaypoint().position,
                        Quaternion.Euler(0, 0, 180),
                        transform
                    );

                    yield return new WaitForSeconds(currentWave.GetRandomSpawnTime() * timeBetweenEnemyMultiplier);
                }

                yield return new WaitForSeconds(timeBetweenWaves);
            }

            // Erhöht den Gesundheitsmultiplikator, wenn nicht im UI-Modus
            if (!isUI)
            {
                EnemyAttributes.instance.IncreaseHealthMultiplier(1.2f);
            }
        }
        while (isLooping);
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

    public void ChangeTimeBetweenEnemies(float newEnemyTimeMultiplier)
    {
        timeBetweenEnemyMultiplier = newEnemyTimeMultiplier;
    }
}
