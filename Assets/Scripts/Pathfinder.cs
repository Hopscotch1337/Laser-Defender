using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinder : MonoBehaviour
{
    private EnemySpawner enemySpawner;
    private WaveConfigSO waveConfig;
    private List<Transform> waypoints;
    private int waypointIndex = 0;

    private void Awake()
    {
        enemySpawner = FindObjectOfType<EnemySpawner>();
    }

    private void Start()
    {
        InitializePathing();
    }

    private void Update()
    {
        FollowPath();
    }

    private void InitializePathing()
    {
        waveConfig = enemySpawner.GetCurrentWave();
        waypoints = waveConfig.GetWaypoints();
        transform.position = waypoints[waypointIndex].position;
    }

    private void FollowPath()
    {
        if (waypointIndex < waypoints.Count)
        {
            MoveTowardsNextWaypoint();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void MoveTowardsNextWaypoint()
    {
        Vector3 targetPosition = waypoints[waypointIndex].position;
        float delta = waveConfig.GetMoveSpeed() * Time.deltaTime;
        transform.position = Vector2.MoveTowards(transform.position, targetPosition, delta);

        if (transform.position == targetPosition)
        {
            waypointIndex++;
        }
    }
}
