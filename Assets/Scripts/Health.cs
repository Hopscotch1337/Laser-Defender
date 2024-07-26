using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Health : MonoBehaviour
{
    [Header("General")]
    [SerializeField] private float health = 50f;
    [SerializeField] private int shield = 0;
    [SerializeField] private int scoreToAdd = 100;
    [SerializeField] private ParticleSystem hitEffect;
    [SerializeField] private ParticleSystem destroyEffect;

    [Header("Player - Enemy")]
    [SerializeField] private bool isPlayer;
    [SerializeField] private float enemyHealthFixed;
    [SerializeField] private TextMeshPro healthBar;

    private CameraShake cameraShake;
    private AudioPlayer audioPlayer;
    private ScoreKeeper scoreKeeper;
    private LevelManager levelManager;
    private EnemyAttributes enemyAttributes;

    private void Awake()
    {
        cameraShake = Camera.main.GetComponent<CameraShake>();
        audioPlayer = FindObjectOfType<AudioPlayer>();
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
        levelManager = FindObjectOfType<LevelManager>();
        enemyAttributes = FindAnyObjectByType<EnemyAttributes>();
    }

    private void Start()
    {
        UpdateAttributes();
        if (!isPlayer)
        {
            healthBar.text = health.ToString();
        }
    }

    private void UpdateAttributes()
    {
        if (!isPlayer && enemyAttributes != null)
        {
            health = Mathf.RoundToInt(health * enemyAttributes.GetHealthMultiplier());
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "PowerUps" && isPlayer)
        {
            HandlePowerUp(other);
        }
        else if (other.tag != "PowerUps")
        {
            HandleDamage(other);
        }
    }

    private void HandlePowerUp(Collider2D other)
    {
        PowerUps powerUps = other.GetComponent<PowerUps>();
        if (powerUps != null)
        {
            powerUps.GetPowerUp(gameObject);
            powerUps.PowerUpGathered();
        }
    }

    private void HandleDamage(Collider2D other)
    {
        DamageDealer damageDealer = other.GetComponent<DamageDealer>();
        if (damageDealer != null)
        {
            TakeDamage(damageDealer.GetDamage());
            damageDealer.Hit();
        }
    }

    public float GetHealth()
    {
        return isPlayer ? health : enemyHealthFixed;
    }

    public int GetShield()
    {
        return shield;
    }

    public void AddHealth(float addHealth)
    {
        health = Mathf.Clamp(health + addHealth, 0, 100);
    }

    public void AddShield(int addShield)
    {
        shield = Mathf.Clamp(shield + addShield, 0, 50);
    }

    public void AddEnemyHealth(float addHealth)
    {
        health = addHealth;
    }

    private void TakeDamage(int damage)
    {
        if (shield > 0)
        {
            shield -= damage;
            audioPlayer.PlayShieldClip();
            shield = shield < 0 ? 0 : shield;
        }
        else
        {
            health -= damage;
            PlayHitEffect();
            ShakeCamera();

            if (health <= 0)
            {
                HandleDeath();
            }
            else if (!isPlayer)
            {
                healthBar.text = health.ToString();
            }
        }
    }

    private void HandleDeath()
    {
        if (!isPlayer)
        {
            scoreKeeper?.AddScore(scoreToAdd);
        }
        else
        {
            levelManager.LoadGameOver();
        }
        Destroy(gameObject);
    }

    private void PlayHitEffect()
    {
        ParticleSystem effect = health > 1 ? hitEffect : destroyEffect;
        if (health > 1)
        {
            audioPlayer.PlayHitClip();
        }
        else
        {
            audioPlayer.PlayDestroyClip();
        }

        ParticleSystem instance = Instantiate(effect, transform.position, Quaternion.identity);
        Destroy(instance.gameObject, instance.main.duration + instance.main.startLifetime.constantMax);
    }

    private void ShakeCamera()
    {
        if (isPlayer && cameraShake != null)
        {
            cameraShake.Play();
        }
    }
}