using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultPlayer : Player
{
    // Start is called before the first frame update
    void Start()
    {
        playerLives = 3;
        dashPointsIncrement = 0.05f;
        isDead = false;
        wrongDirection = false;
        rb = GetComponent<Rigidbody2D>();
        powerUp = GetComponent<Powerup>();
        dash = GetComponent<Dash>();
        circleCollider = GetComponent<CircleCollider2D>();
    }
}
