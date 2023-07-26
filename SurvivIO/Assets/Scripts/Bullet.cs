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
        Unit enemy = collision.gameObject.GetComponent<Unit>();
        Player player = collision.gameObject.GetComponent<Player>();
        Gun gunParent = transform.parent.gameObject.GetComponent<Gun>();

        if (health != null)
        {
            health.TakeDamage(gunParent._damage);

            if (player == null)
            {
                enemy.ManageEnemyHealth();
            }
            else
            {
                GameUI.Instance._hpSlider.value = (float)health.CurrentHealth / (float)health.MaxHealth;
            }
        }
        Destroy(gameObject);
    }
}
