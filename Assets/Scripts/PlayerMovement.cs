using System;
using System.Collections;
using System.Collections.Generic;
using System.Security;
using UnityEngine;
using UnityEngine.InputSystem;



public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float playerMoveSpeed = 5f;
    [SerializeField] private float paddingLeft;
    [SerializeField] private float paddingRight;
    [SerializeField] private float paddingUp;
    [SerializeField] private float paddingDown;
    [SerializeField] private Joystick joystick;

    private Vector2 rawInput;
    private Vector2 minBounds;
    private Vector2 maxBounds;
    private Shooter shooter;

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
        HandleMovement();
    }

    private void InitBounds()
    {
        Camera mainCamera = Camera.main;
        minBounds = mainCamera.ViewportToWorldPoint(new Vector2(0, 0));
        maxBounds = mainCamera.ViewportToWorldPoint(new Vector2(1, 1));
    }

    private void HandleMovement()
    {
        Vector2 delta = Vector2.zero;

        if (Touchscreen.current != null && Touchscreen.current.primaryTouch.press.isPressed)
        {
            delta = joystick.Direction * playerMoveSpeed * Time.deltaTime;
        }
        else if (Keyboard.current.anyKey.isPressed)
        {
            delta = rawInput * playerMoveSpeed * Time.deltaTime;
        }

        Vector2 newPos = new Vector2
        {
            x = Mathf.Clamp(transform.position.x + delta.x, minBounds.x + paddingLeft, maxBounds.x - paddingRight),
            y = Mathf.Clamp(transform.position.y + delta.y, minBounds.y + paddingDown, maxBounds.y - paddingUp)
        };

        transform.position = newPos;
    }

    public void OnMove(InputValue value)
    {
        rawInput = value.Get<Vector2>();
    }

    public void AddPlayerMovement(float moveSpeedModifier)
    {
        playerMoveSpeed += moveSpeedModifier;
    }
}
 
