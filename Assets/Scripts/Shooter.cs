using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    [Header("General")]
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] float projectileSpeed = 10f;
    [SerializeField] float projectileLiveTime = 5f;
    [SerializeField] float enemyFireRate = 0.5f;
    [SerializeField] float playerFireRate;


    [Header("AI")]
    [SerializeField] float fireRateModifikator = 0f;
    [SerializeField] float minFireRate = 0.2f;
    [SerializeField] bool useAI;

    [HideInInspector] public bool isFiring;

    Coroutine fireCoroutine;
    AudioPlayer audioPlayer;
    

    void Awake()
    {
        audioPlayer = FindObjectOfType<AudioPlayer>();
    }
    void Start()
    {
        
        if (useAI)
        {
            isFiring = true;
        }
    }
    public void AddAttackSpeed(float addAttackSpeed)
    {
        playerFireRate = playerFireRate - addAttackSpeed;
        Debug.Log(playerFireRate);
    }



    void Update()
    {
        Fire();
    }

    void Fire()
    {
        if (isFiring && fireCoroutine == null)
        {
            fireCoroutine = StartCoroutine(FireContinuosly());
        }
        else if (!isFiring && fireCoroutine != null)
        {
            StopCoroutine(fireCoroutine);
            fireCoroutine = null;
        }
        
    }

    IEnumerator FireContinuosly()
    {
        while (true)
        {
        GameObject p = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
        audioPlayer.PlayShootingClip();
        Rigidbody2D rigidbody2D = p.GetComponent<Rigidbody2D>();
        if (rigidbody2D != null)
        {
            rigidbody2D.velocity = transform.up * projectileSpeed;
        }
        Destroy(p, projectileLiveTime);
        float randomFireRate;
        if(!useAI)
        {
            randomFireRate = Random.Range (playerFireRate - fireRateModifikator, playerFireRate + fireRateModifikator);
        }
        else
        {
            randomFireRate = Random.Range (enemyFireRate - fireRateModifikator, enemyFireRate + fireRateModifikator);
        }
        
        float fireRate = Mathf.Clamp(randomFireRate,minFireRate,float.MaxValue); 
        yield return new WaitForSeconds(fireRate);

        }
    }
}
