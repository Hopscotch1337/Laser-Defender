using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] int health = 50;
    [SerializeField] int shield = 0;
    [SerializeField] int scoreToAdd = 100;
    [SerializeField] ParticleSystem hitEffect;
    [SerializeField] ParticleSystem destroyEffect;
    CameraShake cameraShake;
    [SerializeField] bool applyCameraShake;
    AudioPlayer audioPlayer;
    ScoreKeeper scoreKeeper;
    LevelManager levelManager;

    void Awake()
    {
        cameraShake = Camera.main.GetComponent<CameraShake>();
        audioPlayer = FindObjectOfType<AudioPlayer>();
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
        levelManager = FindObjectOfType<LevelManager>();
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.tag != "PowerUps")
        {
            DamageDealer damageDealer = other.GetComponent<DamageDealer>();
            if(damageDealer != null)
            {
                damageDealer.Hit();
                Takedamage(damageDealer.GetDamage());
            }
        }
        else
        {
            PowerUps powerUps = other.GetComponent<PowerUps>();
            if (powerUps != null)
            {
                powerUps.GetPowerUp(this.gameObject);
                powerUps.PowerUpGathered();
            }
        }
        
    }

    public int GetHealth()
    {
        return health;
    }
    public int GetShield()
    {
        return shield;
    }
    public void AddHealth(int addHealth)
    {
        health += addHealth;
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
                if(!applyCameraShake && scoreKeeper != null)
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
        if(applyCameraShake && cameraShake != null)
        {
            cameraShake.Play();
        }
    }

    
}
