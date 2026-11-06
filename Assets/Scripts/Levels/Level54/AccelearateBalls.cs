using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AccelearateBalls : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    private float dirX, dirY, _moveSpeed = 5f;
    private void Update()
    {
        dirY = Input.acceleration.y * _moveSpeed;
        dirX = Input.acceleration.x * _moveSpeed;
    }
    
    private void FixedUpdate()
    {
        rb.velocity = new Vector2(dirX, dirY);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (gameObject.name == "circle_1")
        {
            if(other.name == "GameObject")
                FindObjectOfType<GameController>().RestartLevel();
        }
    }
}
