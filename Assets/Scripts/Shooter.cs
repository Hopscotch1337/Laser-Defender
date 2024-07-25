using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Unity.VisualScripting;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    [Header("General")]
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private float projectileSpeed = 10f;
    [SerializeField] private float projectileLiveTime = 5f;
    [SerializeField] private float playerFireRate;

    [Header("AI")]
    [SerializeField] private float fireRateModifier = 0f;
    [SerializeField] private float minFireRate = 0.2f;
    [SerializeField] private bool useAI;
    [SerializeField] private float enemyFireRate;
    [SerializeField] private float enemyFixedFireRate;

    private bool autoFire = true; // Android Version
    private Coroutine fireCoroutine;
    private AudioPlayer audioPlayer;

    private void Awake()
    {
        audioPlayer = FindObjectOfType<AudioPlayer>();
    }

    public void AddAttackSpeed(float addAttackSpeed)
    {
        playerFireRate *= addAttackSpeed;
        Debug.Log($"Player fire rate updated to: {playerFireRate}");
    }

    public void AddEnemyAttackSpeed(float addAttackSpeed)
    {
        enemyFireRate = addAttackSpeed;
        Debug.Log($"Enemy fire rate updated to: {enemyFireRate}");
    }

    public float GetEnemyAttackSpeed()
    {
        return enemyFixedFireRate;
    }

    private void Update()
    {
        Fire();
    }

    private void Fire()
    {
        if (autoFire && fireCoroutine == null)
        {
            fireCoroutine = StartCoroutine(FireContinuously());
        }
        else if (!autoFire && fireCoroutine != null)
        {
            StopCoroutine(fireCoroutine);
            fireCoroutine = null;
        }
    }

    private IEnumerator FireContinuously()
    {
        while (true)
        {
            FireProjectile();
            float fireRate = DetermineFireRate();
            yield return new WaitForSeconds(fireRate);
        }
    }

    private void FireProjectile()
    {
        GameObject projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
        audioPlayer.PlayShootingClip();

        Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.velocity = transform.up * projectileSpeed;
        }

        Destroy(projectile, projectileLiveTime);
    }

    private float DetermineFireRate()
    {
        float randomFireRate = useAI
            ? Random.Range(enemyFireRate - fireRateModifier, enemyFireRate + fireRateModifier)
            : playerFireRate;

        return Mathf.Clamp(randomFireRate, minFireRate, float.MaxValue);
    }
}
