using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Transform player;
    public int playerLives;
    [HideInInspector] public bool isDead;
    [HideInInspector] public bool enemyInRange;

    [SerializeField]private float gravityToWall;
    private Rigidbody2D rb;
    private bool wrongDirection;
    [SerializeField] private Slash slashDir;
    private Enemy enemy;
    private Powerup powerUp;
    private Dash dash;
    private CircleCollider2D circleCollider;

    void Start()
    {
        isDead = false;
        wrongDirection = false;
        rb = GetComponent<Rigidbody2D>();
        powerUp = GetComponent<Powerup>();
        dash = GetComponent<Dash>();
        circleCollider= GetComponent<CircleCollider2D>();
    }

    void Update()
    {
        Debug.Log(enemyInRange);
        if (isDead)
            return;

        if (playerLives <0)
        {
            playerLives = 0;
        }

        if (wrongDirection)
        {
            playerLives--;
            wrongDirection = false;
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
                dash.increaseDashPoints();
            }

            if (enemy.deathDirection != slashDir.slashDirection && slashDir.slashDirection != 0)
            {
                wrongDirection = true;
                slashDir.slashDirection = 0;
            }
        }
    }

    void FixedUpdate()
    {
        rb.velocity = Vector2.right * gravityToWall * Time.deltaTime;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        enemy = other.GetComponent<Enemy>();
        if (enemy != null)
        {
            enemyInRange = true;

            slashDir.slashDirection = 0;

            enemy.secondArrow.SetActive(true);
            enemy.arrow.SetActive(false);
        }
    }
}





