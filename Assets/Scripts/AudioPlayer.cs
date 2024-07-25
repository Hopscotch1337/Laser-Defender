using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayer : MonoBehaviour

{
    [Header("Shooting")]
    [SerializeField] AudioClip shootingClip;
    [SerializeField, Range(0f, 1f)] float shootingVolume = 1f;

    [Header("Hit")]
    [SerializeField] AudioClip hitClip;
    [SerializeField] AudioClip shieldClip;
    [SerializeField, Range(0f, 1f)] float hitVolume = 1f;

    [Header("Destruction")]
    [SerializeField] AudioClip destroyClip;
    [SerializeField, Range(0f, 1f)] float destroyVolume = 1f;

    [Header("Music")]
    [SerializeField] AudioClip[] audioClips;
    private AudioSource audioSource;

    void Awake()
    {
        if (audioClips.Length > 0)
        {
            audioSource = GetComponent<AudioSource>();
            if (audioSource != null)
            {
                int randomIndex = Random.Range(0, audioClips.Length);
                audioSource.clip = audioClips[randomIndex];
                audioSource.Play();
            }
            else
            {
                Debug.LogWarning("AudioSource component missing on this GameObject.");
            }
        }
        else
        {
            Debug.LogWarning("No audio clips assigned to play on Awake.");
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
        PlayClip(shieldClip, 0.4f); // Hardcoded volume
    }

    public void PlayDestroyClip()
    {
        PlayClip(destroyClip, destroyVolume);
    }

    private void PlayClip(AudioClip clip, float volume)
    {
        if (clip != null)
        {
            AudioSource.PlayClipAtPoint(clip, Camera.main.transform.position, volume);
        }
        else
        {
            Debug.LogWarning("Attempted to play a null AudioClip.");
        }
    }
}