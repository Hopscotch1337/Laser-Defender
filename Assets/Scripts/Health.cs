using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] float health = 50f;
    [SerializeField] int shield = 0;
    [SerializeField] int scoreToAdd = 100;
    [SerializeField] ParticleSystem hitEffect;
    [SerializeField] ParticleSystem destroyEffect;
    CameraShake cameraShake;
    [SerializeField] bool isPlayer;
    AudioPlayer audioPlayer;
    ScoreKeeper scoreKeeper;
    LevelManager levelManager;
    EnemyAttributes enemyAttributes;

    [SerializeField] float enemyHealthfixed;

    void Awake()
    {
        cameraShake = Camera.main.GetComponent<CameraShake>();
        audioPlayer = FindObjectOfType<AudioPlayer>();
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
        levelManager = FindObjectOfType<LevelManager>();
        enemyAttributes = FindAnyObjectByType<EnemyAttributes>();  
    }
    void Start()
    {
        UpdateAttributes();
    }

    void UpdateAttributes()
    {
        if (!isPlayer && enemyAttributes != null)
        {
            health = Mathf.RoundToInt(health * enemyAttributes.GetHealthMultiplier());
        }
        
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.tag != "PowerUps")
        {
            DamageDealer damageDealer = other.GetComponent<DamageDealer>();
            if(damageDealer != null)
            {
                Takedamage(damageDealer.GetDamage());
                damageDealer.Hit();
            }
        }
        else if (other.tag == "PowerUps" && isPlayer)
        {
            PowerUps powerUps = other.GetComponent<PowerUps>();
            if (powerUps != null)
            {
                powerUps.GetPowerUp(this.gameObject);
                powerUps.PowerUpGathered();
            }
        }
        
    }

    public float GetHealth()
    {
        if (isPlayer)
        {
            return health;
        }
        else
        {
            return enemyHealthfixed;
        }
        
    }
    public int GetShield()
    {
        return shield;
    }
    public void AddHealth(float addHealth)
    {
        health += addHealth;
    }
     public void AddEnemyHealth(float addHealth)
    {
        health = addHealth;
    }

    public void AddShield(int addShield)
    {
        shield += addShield;
    }



    void Takedamage(int damage)
    {  
        if(shield < 1)
        {
            shield = 0;
            health -= damage;
            PlayHitEffect();
            ShakeCamra();
            if (health <1)
            {
                if(!isPlayer && scoreKeeper != null)
                {
                    scoreKeeper.AddScore(scoreToAdd);
                }
                else
                {
                    levelManager.LoadGameOver();
                }
                Destroy(gameObject);
            }
            
        }
        else
        {
        shield -= damage;
        ShakeCamra();
        audioPlayer.PlayShieldClip();
        }
    }

    void PlayHitEffect()
    {
        ParticleSystem effect;
        
        if (health > 1) 
        {
            effect = hitEffect;
            audioPlayer.PlayHitClip();
        }
        else            
        {
            effect = destroyEffect;
            audioPlayer.PlayDestroyClip();
        }
        
        ParticleSystem instance = Instantiate(effect, transform.position, Quaternion.identity);
        Destroy(instance.gameObject, instance.main.duration + instance.main.startLifetime.constantMax);
    }

    void ShakeCamra()
    {
        if(isPlayer && cameraShake != null)
        {
            cameraShake.Play();
        }
    }

    
}
