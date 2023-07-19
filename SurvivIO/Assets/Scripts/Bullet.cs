using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float bulletSpeed = 10f;
    private Rigidbody2D bulletRigidbody;

    private void Start()
    {
        bulletRigidbody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        bulletRigidbody.velocity = transform.up * bulletSpeed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Health health = collision.gameObject.GetComponent<Health>();
        Bullet bullet = collision.gameObject.GetComponent<Bullet>();
        Unit enemy = collision.gameObject.GetComponent<Unit>();
        if (health != null)
        {
            health.TakeDamage(5);
            enemy.ManageHealth();

            Destroy(gameObject);
        }
        else if (bullet == null) 
        {
            Destroy(gameObject);
        }
    }
}
