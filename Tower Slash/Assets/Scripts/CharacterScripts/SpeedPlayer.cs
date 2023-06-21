using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;

public class SpeedPlayer :Player
{

    private void Start()
    {
        wrongDirection = false;
        playerLives = 3;
        dashPointsIncrement = 0.1f;
        isDead = false;
        rb = GetComponent<Rigidbody2D>();
        powerUp = GetComponent<Powerup>();
        dash = GetComponent<Dash>();
        slashDir = GetComponent<Slash>();
        circleCollider = GetComponent<CircleCollider2D>();
    }
}
