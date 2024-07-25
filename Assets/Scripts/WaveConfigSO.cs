using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "New Wave Config", menuName = "Wave Config")]
public class WaveConfigSO : ScriptableObject 
{
    [Header("Wave Settings")]
    [SerializeField] private List<GameObject> enemyPrefabs;
    [SerializeField] private Transform pathPrefab;
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float timeBetweenEnemySpawns = 1f;
    [SerializeField] private float spawnTimeVariance = 0.2f;
    [SerializeField] private float minimumSpawnTime = 0.2f;

    // Gets the starting waypoint from the path prefab
    public Transform GetStartWaypoint()
    {
        return pathPrefab.GetChild(0);
    }

    // Retrieves all waypoints from the path prefab
    public List<Transform> GetWaypoints()
    {
        List<Transform> waypoints = new List<Transform>();

        foreach (Transform child in pathPrefab)
        {
            waypoints.Add(child);
        }

        return waypoints;
    }

    // Returns the move speed for enemies
    public float GetMoveSpeed()
    {
        return moveSpeed;
    }

    // Returns the number of enemy prefabs
    public int GetEnemyCount()
    {
        return enemyPrefabs.Count;
    }

    // Retrieves the enemy prefab at a specific index
    public GameObject GetEnemyPrefab(int index)
    {
        if (index >= 0 && index < enemyPrefabs.Count)
        {
            return enemyPrefabs[index];
        }
        else
        {
            Debug.LogError("Index out of bounds: " + index);
            return null;
        }
    }

    // Calculates a random spawn time within the specified variance
    public float GetRandomSpawnTime()
    {
        float spawnTime = Random.Range(timeBetweenEnemySpawns - spawnTimeVariance, timeBetweenEnemySpawns + spawnTimeVariance);
        return Mathf.Clamp(spawnTime, minimumSpawnTime, float.MaxValue);
    }
}
