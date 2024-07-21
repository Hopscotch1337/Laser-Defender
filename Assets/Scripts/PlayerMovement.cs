using System;
using System.Collections;
using System.Collections.Generic;
using System.Security;
using UnityEngine;
using UnityEngine.InputSystem;



public class PlayerMovement : MonoBehaviour
{
Vector2 rawinput;
Vector2 minBounds;
Vector2 maxBounds;
Shooter shooter;
[SerializeField] float playerMoveSpeed = 5f;
[SerializeField] float paddingleft;
[SerializeField] float paddingright;
[SerializeField] float paddingup;
[SerializeField] float paddingdown;

    void Awake() 
    {
        shooter = GetComponent<Shooter>();
    }
    void Start()
    {   
        InitBondries();
    }

    void Update()
    {
        Movement();
    }

       public void AddPlayerMovement(float moveSpeedModifier)
    {
        playerMoveSpeed += moveSpeedModifier;
    }

    void InitBondries()
    {
        Camera mainCamera = Camera.main;
        minBounds = mainCamera.ViewportToWorldPoint(new Vector2 (0,0));
        maxBounds = mainCamera.ViewportToWorldPoint(new Vector2 (1,1));
    }

    void Movement()
    {
        Vector2 delta = rawinput * playerMoveSpeed * Time.deltaTime;
        Vector2 newPos =new Vector2();
        newPos.x = Mathf.Clamp(transform.position.x + delta.x, minBounds.x + paddingleft, maxBounds.x - paddingright);
        newPos.y = Mathf.Clamp(transform.position.y + delta.y, minBounds.y + paddingdown, maxBounds.y - paddingup);
        transform.position = newPos;

    }
    void OnMove(InputValue value)
    {
        rawinput = value.Get<Vector2>();
    }

        void OnFire(InputValue value)
    {
        if(shooter != null)
        {
            shooter.isFiring = value.isPressed;
        }
    
    }
}

 
