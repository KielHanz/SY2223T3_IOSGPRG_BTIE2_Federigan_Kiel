using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Player : MonoBehaviour
{

    public int playerLives = 3;
    [HideInInspector] public bool isDead;
    [HideInInspector] public bool enemyInRange;
    public float gravityToWall;
    [HideInInspector] public Rigidbody2D rb;
    public bool wrongDirection;
    public Slash slashDir;
    [HideInInspector] public Enemy enemy;
    [HideInInspector] public WallMovement wall;
    [HideInInspector] public Powerup powerUp;
    [HideInInspector] public Dash dash;
    [HideInInspector] public CircleCollider2D circleCollider;
    public float dashPointsIncrement;

    private void Start()
    {
        isDead = false;
        dashPointsIncrement = 0.05f;
        wrongDirection = false;
        rb = GetComponent<Rigidbody2D>();
        powerUp = GetComponent<Powerup>();
        dash = GetComponent<Dash>();
        slashDir = GetComponent<Slash>();
        slashDir.slashDirection = 0;
        circleCollider= GetComponent<CircleCollider2D>();

    }

    private void Update()
    {
        if (isDead)
            return;

        if (playerLives <= 0)
        {
            playerLives = 0;
        }

        if (dash.isDash)
        {
            circleCollider.radius = 15;
        }
        else
        {
            circleCollider.radius = 6;
        }

        if (enemy != null)
        {

            if (wrongDirection)
            {
                playerLives--;
                wrongDirection = false;
            }

            if (slashDir.slashDirection != 0 && enemy.deathDirection != slashDir.slashDirection && slashDir.slashDirection != 0.05f)
            {
                wrongDirection = true;
                slashDir.slashDirection = 0;
            }

            if (enemy.deathDirection == slashDir.slashDirection)
            {
                enemy.enemyHp = 0;
            }


            if (dash.isDash)
            {
                enemy.speed = Time.unscaledTime;
            }

            if (enemy.enemyHp == 0)
            {
                enemyInRange = false;
                powerUp.PowerupChance();
                increaseDashGauge(dashPointsIncrement);
                GameUI.score += Random.Range(15, 30);
            }

        }
        else
        {
            wrongDirection = false;
        }
    }

    private void FixedUpdate()
    {
        rb.velocity = Vector2.right * gravityToWall * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        enemy = other.GetComponent<Enemy>();
        if (enemy != null)
        {
            Debug.Log("enemy in range");
            enemyInRange = true;

            slashDir.slashDirection = 0.05f;

            enemy.secondArrow.SetActive(true);
            enemy.arrow.SetActive(false);
        }
        wall = other.GetComponent<WallMovement>();
    }

    private void increaseDashGauge(float value)
    {
        dash.dashGauge += value;
    }
}






