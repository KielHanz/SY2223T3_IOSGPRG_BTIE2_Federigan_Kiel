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
        if (!isWithinRange())
        {
            Patrol();
        }
        else
        {
            if (Vector2.Distance(GameManager.Instance._player.transform.position, this.transform.position) >= 4f)
            {
                transform.position = Vector2.MoveTowards(transform.position, GameManager.Instance._player.transform.position, _speed * Time.deltaTime);
            }
            transform.right = GameManager.Instance._player.transform.position - transform.position;
        }
    }
}
