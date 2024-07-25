using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    [SerializeField] private float shakeDuration = 1f;
    [SerializeField] private float shakeMagnitude = 5f;

    private Vector3 initialPosition;

    void Start()
    {
        // Speichert die ursprüngliche Position des Objekts
        initialPosition = transform.position;
    }

    public void Play()
    {
        // Startet den Schüttel-Effekt
        StartCoroutine(ShakeCoroutine());
    }

    private IEnumerator ShakeCoroutine()
    {
        float elapsedTime = shakeDuration;

        while (elapsedTime > 0)
        {
            // Ändert die Position des Objekts innerhalb eines Kreises mit zufälligem Offset
            transform.position = initialPosition + (Vector3)Random.insideUnitCircle * shakeMagnitude;
            
            // Reduziert die verstrichene Zeit und wartet bis zum nächsten Frame
            elapsedTime -= Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        // Stellt die ursprüngliche Position wieder her
        transform.position = initialPosition;
    }
}