using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedEnemy : Unit
{
    private void Start()
    {
        targetDestination = transform.position;
        InsantiateEnemyHealthBar();
        _currentGun = _guns[Random.Range(0, _guns.Count)].GetComponent<Gun>();
        GameObject equippedGun = Instantiate(_currentGun.gameObject, transform.position + new Vector3(0.5f, 0.0f, 0.0f), transform.rotation);
        equippedGun.transform.SetParent(transform.GetChild(0));
        equippedGun.transform.rotation = Quaternion.Euler(0, 0,-90);
    }

    private void FixedUpdate()
    {
        if (!isWithinRange)
        {
            Patrol();
        }
        else if (unitList != null)
        {
            if (Vector2.Distance(unitList[0].transform.position, this.transform.position) >= 5f)
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
