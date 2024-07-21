using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    [Header("Shooting")]
    [SerializeField] AudioClip shootingClip;
    [SerializeField] [Range(0f, 1f)] float shootingVolume = 1f;

    [Header("Hit")]
    [SerializeField] AudioClip hitClip;
    [SerializeField] AudioClip shieldClip;
    [SerializeField] [Range(0f, 1f)] float hitVolume = 1f;
    
    [Header("Destruction")]
    [SerializeField] AudioClip destroyClip;
    [SerializeField] [Range(0f, 1f)] float destroyVolume = 1f;

    [Header("Musik")]
    [SerializeField] AudioClip [] audioClips;
    AudioSource audioSource;

    void Awake()
    {
        if (audioClips != null)
        {
            audioSource = GetComponent<AudioSource>();
            int randomIndex = Random.Range(0, audioClips.Length);
            audioSource.clip = audioClips[randomIndex]; 
            audioSource.Play();
        }
        
    }

    public void PlayShootingClip()
    {
        PlayClip(shootingClip, shootingVolume);
    }

    public void PlayHitClip()
    {
        PlayClip(hitClip, hitVolume);
    }
       public void PlayShieldClip()
    {
        PlayClip(shieldClip, 0.4f);
    }

    public void PlayDestroyClip()
    {
        PlayClip(destroyClip, destroyVolume);
    }

    void PlayClip(AudioClip clip, float volume)
    {
        if(clip != null)
        {
            AudioSource.PlayClipAtPoint(clip,Camera.main.transform.position, volume);
        }
        
    }
}

