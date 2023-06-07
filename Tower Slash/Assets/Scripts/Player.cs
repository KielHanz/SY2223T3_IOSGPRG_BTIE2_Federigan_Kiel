using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Player : MonoBehaviour
{
    [HideInInspector] public bool isDead;
    [SerializeField]private float gravityToWall;
    private Rigidbody2D rb;
    public Transform player;

    public float playerRange;

    public int playerLives;

    private bool wrongDirection;

    [SerializeField] private Slash slashDir;

    private Enemy enemy;
    private Powerup powerUp;

    private Dash dash;

    void Start()
    {
        isDead = false;
        wrongDirection = false;
        rb = GetComponent<Rigidbody2D>();
        powerUp = GetComponent<Powerup>();
        dash = GetComponent<Dash>();
 

    }
    void Update()
    {
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


        if (enemy != null)
        {
            if (enemy.deathDirection == slashDir.slashDirection)
            {
                enemy.enemyHp = 0;
            }
            if (enemy.enemyHp == 0)
            {
                powerUp.PowerupChance();
                dash.increaseDashPoints();

            }

            if (enemy.deathDirection != slashDir.slashDirection && slashDir.slashDirection != 0)
            {
                wrongDirection = true;
                slashDir.slashDirection = 0;
            }


            if (enemy.isDead)
            {
                slashDir.slashDirection = 0;
            }
        }

        Debug.Log(slashDir.slashDirection);
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
            slashDir.slashDirection = 0;

            enemy.secondArrow.SetActive(true);
            enemy.arrow.SetActive(false);
            Debug.Log("Enemy in range");

        }
    }
}





