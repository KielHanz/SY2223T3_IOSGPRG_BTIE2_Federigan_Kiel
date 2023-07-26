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
        GameObject equippedGun = Instantiate(_currentGun.gameObject, transform.position + new Vector3(0.5f, 0.0f, 0.0f), Quaternion.Euler(0, 0, -90), transform);
        _currentGun = equippedGun.GetComponent<Gun>();
        _currentGun._isInfiniteAmmo = true;
        _currentGun.gunObj = transform.gameObject; //to get the position of the gun

    }

    private void FixedUpdate()
    {
        MoveTowardsTarget(5f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        unit = collision.GetComponent<Unit>();

        if (unit != null)
        {
            isWithinRange = true;
            targetList.Add(unit);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        unit = other.GetComponent<Unit>();
        targetList.Remove(unit);
    }

    private void OnDestroy()
    {
        targetList.Remove(unit);
    }

    public override void Shoot()
    {
        base.Shoot();
    }
}
