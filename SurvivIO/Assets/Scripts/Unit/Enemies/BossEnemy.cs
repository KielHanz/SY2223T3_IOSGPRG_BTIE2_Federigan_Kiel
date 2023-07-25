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
        MoveTowardsTarget(4f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        unit = collision.GetComponent<Unit>();

        if (unit != null)
        {
            isWithinRange = true;
            unitList.Add(unit);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        unit = other.GetComponent<Unit>();
        unitList.Remove(unit);
    }

    private void OnDestroy()
    {
        unitList.Remove(unit);
    }
}
