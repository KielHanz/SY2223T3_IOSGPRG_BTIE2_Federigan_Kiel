using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankPlayer : Player
{

    private void Start()
    {
        playerLives = 5;
        dashPointsIncrement = 0.05f;
        isDead = false;
        wrongDirection = false;
        rb = GetComponent<Rigidbody2D>();
        powerUp = GetComponent<Powerup>();
        dash = GetComponent<Dash>();
        slashDir = GetComponent<Slash>();
        circleCollider = GetComponent<CircleCollider2D>();
    }
}
