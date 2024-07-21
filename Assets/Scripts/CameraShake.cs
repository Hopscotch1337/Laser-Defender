using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{

[SerializeField] float shakeDuration = 1f;
[SerializeField] float shackeMagnitude = 5f;

Vector3 initialPosition;
    void Start()
    {
        initialPosition = transform.position;
    }

    public void Play()
    {

        StartCoroutine(Shake());
    }

    IEnumerator Shake()
    {
        float elapsedTime = shakeDuration;
        while (elapsedTime > 0)
        {
            transform.position = initialPosition + (Vector3) Random.insideUnitCircle * shackeMagnitude;
            elapsedTime -= Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        
        transform.position = initialPosition;
    }
}