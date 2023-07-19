using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEnemy : Unit
{
    private void Start()
    {
        targetDestination = transform.position;
        InsantiateEnemyHealthBar();
    }

    private void FixedUpdate()
    {
        if (!isWithinRange())
        {
            Patrol();
        }
        else
        {
            if (Vector2.Distance(GameManager.Instance._player.transform.position, this.transform.position) >= 2f)
            {
                transform.position = Vector2.MoveTowards(transform.position, GameManager.Instance._player.transform.position, _speed * Time.deltaTime);
            }
            transform.right = GameManager.Instance._player.transform.position - transform.position;
        }
    }
}
