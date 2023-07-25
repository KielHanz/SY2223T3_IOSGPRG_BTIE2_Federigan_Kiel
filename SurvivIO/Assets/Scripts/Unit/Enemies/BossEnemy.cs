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
        if (!isWithinRange)
        {
            Patrol();
        }
        else if (unitList != null)
        {
            if (Vector2.Distance(unitList[0].transform.position, this.transform.position) >= 4f)
            {
                transform.position = Vector2.MoveTowards(transform.position, unitList[0].transform.position, _speed * Time.deltaTime);
            }
            transform.right = unitList[0].transform.position - transform.position;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        unit = collision.GetComponent<Unit>();

        if (unit != null)
        {
            unitList.Add(unit);
            isWithinRange = true;
            Debug.Log("Unit added to unitList");
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
