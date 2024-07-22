using System;
using System.Collections;
using System.Collections.Generic;
using System.Security;
using UnityEngine;
using UnityEngine.InputSystem;



public class PlayerMovement : MonoBehaviour
{
 [SerializeField] private float playerMoveSpeed = 5f;
    [SerializeField] private float paddingleft;
    [SerializeField] private float paddingright;
    [SerializeField] private float paddingup;
    [SerializeField] private float paddingdown;

    private Vector2 rawInput;
    private Vector2 minBounds;
    private Vector2 maxBounds;
    private Shooter shooter;
    [SerializeField] Joystick joystick;

    private void Awake()
    {
        shooter = GetComponent<Shooter>();
    }

    private void Start()
    {
        InitBounds();
    }

    private void Update()
    {
        Movement();
    }

    private void InitBounds()
    {
        Camera mainCamera = Camera.main;
        minBounds = mainCamera.ViewportToWorldPoint(new Vector2(0, 0));
        maxBounds = mainCamera.ViewportToWorldPoint(new Vector2(1, 1));
    }

    private void Movement()
    {
         if (Touchscreen.current != null && Touchscreen.current.primaryTouch.press.isPressed)
        {
            Vector2 delta = joystick.Direction * playerMoveSpeed * Time.deltaTime;
            Vector2 newPos = new Vector2
            {
                x = Mathf.Clamp(transform.position.x + delta.x, minBounds.x + paddingleft, maxBounds.x - paddingright),
                y = Mathf.Clamp(transform.position.y + delta.y, minBounds.y + paddingdown, maxBounds.y - paddingup),
            };
            transform.position = newPos;
        }
        // Prüfe auf Tastatureingaben
        else if (Keyboard.current.anyKey.isPressed)
        {
            Vector2 delta = rawInput * playerMoveSpeed * Time.deltaTime;
            Vector2 newPos = new Vector2
            {
                x = Mathf.Clamp(transform.position.x + delta.x, minBounds.x + paddingleft, maxBounds.x - paddingright),
                y = Mathf.Clamp(transform.position.y + delta.y, minBounds.y + paddingdown, maxBounds.y - paddingup),
            };
            transform.position = newPos;
        }
        
        
    }

    public void OnMove(InputValue value)
    {
        rawInput = value.Get<Vector2>();
    }

    public void OnFire(InputValue value)
    {
        if (shooter != null)
        {
            shooter.isFiring = value.isPressed;
        }
    }

    public void AddPlayerMovement(float moveSpeedModifier)
    {
        playerMoveSpeed += moveSpeedModifier;
    }
}

 
