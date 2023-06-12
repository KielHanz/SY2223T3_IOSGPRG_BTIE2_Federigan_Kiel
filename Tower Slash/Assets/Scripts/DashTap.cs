using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashTap : MonoBehaviour
{
    [SerializeField] private GameObject gameObject;
    private Vector3 firstPos;
    private Vector3 lastPos;
    private Touch touch;
    private Vector3 touchPosition;
    private float swipeRange = 2;
    private Player player;
    private GameUI game;

    void Start()
    {
        player = GetComponent<Player>();
        game = gameObject.GetComponent<GameUI>();
    }

    void Update()
    {
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
                game.score += 10;
            }
        }
    }
}
