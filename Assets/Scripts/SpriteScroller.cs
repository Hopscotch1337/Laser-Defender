using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteScroller : MonoBehaviour
{
    [SerializeField] private Vector2 scrollSpeed;

    private Vector2 offset;
    private Material material;

    private void Awake()
    {
        // Get the Material component from the SpriteRenderer
        material = GetComponent<SpriteRenderer>().material;
    }

    private void Update()
    {
        // Calculate the offset based on scroll speed and time delta
        offset = scrollSpeed * Time.deltaTime;
        
        // Update the texture offset to create a scrolling effect
        material.mainTextureOffset += offset;
    }
}