using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public bool isDead;
    [HideInInspector] public GameObject arrow;
    [HideInInspector] public GameObject secondArrow;
    public float deathDirection;
    [HideInInspector] public float enemyHp;
    [SerializeField] public float speed;

    [SerializeField] private List<GameObject> arrows;
    private SpriteRenderer render;
    private Dash dash;

    void Start()
    {
        deathDirection = Random.Range(1, 4);
        int randomIdx = Random.Range(0, arrows.Count);

        Quaternion arrowRotation = transform.rotation;
        switch (deathDirection)
        {
            case 1:
                arrowRotation = Quaternion.Euler(0, 0, -90); //down
                break;
            case 2:
                arrowRotation = Quaternion.Euler(0, 0, 90); // up
                break;
            case 3:
                arrowRotation = Quaternion.Euler(0, 0, 0); // right
                break;
            case 4:
                arrowRotation = Quaternion.Euler(0,0,180); // left
                break;
        }
        if (arrows[randomIdx] == arrows[2])
        {
            secondArrow = Instantiate(arrows[0], transform.position + new Vector3(-1, -1, 0), arrowRotation);
            secondArrow.transform.SetParent(transform, true);
            secondArrow.SetActive(false);
        }
        else if (arrows[randomIdx] == arrows[1])
        {
            render = arrows[1].GetComponent<SpriteRenderer>();
            render.flipX = true;
            arrow = Instantiate(arrows[randomIdx], transform.position + new Vector3(-1, -1, 0), arrowRotation);
            arrow.transform.SetParent(transform, true);

        }
        arrow = Instantiate(arrows[randomIdx], transform.position + new Vector3(-1, -1, 0), arrowRotation);
        arrow.transform.SetParent(transform, true);

    }

    // Update is called once per frame
    void Update()
    {

        if (dash != null)
        {
            if (dash.isDash)
            {
                speed = 20;
            }
            else
            {
                speed = 5;
            }
        }

        transform.Translate(Vector2.down * speed * Time.deltaTime);

        if (enemyHp <= 0)
        {
            isDead = true;

            Destroy(this.gameObject);
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        dash = other.collider.GetComponent<Dash>();
        if (dash != null)
        {
           Destroy(gameObject);
        }

        Player player = other.collider.GetComponent<Player>();
        if (player != null && !dash.isDash)
        {
            player.playerLives--;
        }
       
    }
}
