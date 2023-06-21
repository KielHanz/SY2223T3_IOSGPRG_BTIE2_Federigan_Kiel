using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashTap : MonoBehaviour
{
    private Vector3 firstPos;
    private Vector3 lastPos;
    private Touch touch;
    private Vector3 touchPosition;
    private float swipeRange = 2;
    private GameManager game;
    private Player player;
    private WallMovement wall;

    private void Start()
    {
        game = GetComponent<GameManager>();
    }

    private void Update()
    {
        if (game.chosen)
        {
            player = game.playerObj.GetComponent<Player>();
            wall = game.wallObj.GetComponent<WallMovement>();
        }

        if (player == null)
        {
            return;
        }

        if (Input.touchCount > 0)
        {
            touch = Input.GetTouch(0);
            touchPosition = Camera.main.ScreenToWorldPoint(touch.position);
            touchPosition.z = 0;
        }

        if (Input.touchCount > 0 && touch.phase == TouchPhase.Began)
        {
            firstPos = touchPosition;
 
        }
        else if (Input.touchCount > 0 && touch.phase == TouchPhase.Ended)
        {
            lastPos = touchPosition;

            float swipeDistance = Vector3.Distance(firstPos, lastPos);
            //only activate if no enemy in player range
            if (swipeDistance <= swipeRange && !player.enemyInRange && !player.isDead)
            {

                StartCoroutine(TapTime(0.05f));
            }

        }
    }
    private IEnumerator TapTime(float time)
    {
        if (player.dash != null)
        {
            player.dash.isDash = true;
        }
      
        GameUI.score += Random.Range(5, 10);
        yield return new WaitForSeconds(time);

        if (player.enemy != null)
        {
            player.enemy.speed = 5;
        }
            wall.scrollSpeed = 2.25f;
            player.dash.dashTimer = 3f;
            player.dash.isDash = false;


    }
}
