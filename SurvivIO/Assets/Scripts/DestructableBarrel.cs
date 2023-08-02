using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructableBarrel : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Bullet bullet = collision.gameObject.GetComponent<Bullet>();
        if (bullet != null)
        {
            transform.localScale -= new Vector3(0.05f,0.05f,0.05f);
        }

        if (transform.localScale.x <= 0.25f)
        {
            Spawner spawner = Spawner.Instance;

            SpawnLoot(Random.Range(1,4), spawner._gunPrefab, spawner._ammoPrefab, spawner._healthKitPrefab);
            Destroy(gameObject);
        }
    }

    private void SpawnLoot(int count, List<GameObject> gunLootPrefab, List<GameObject> ammoLootPrefab, GameObject healthKit)
    {
        for (int i = 0; i < count; i++)
        {
            float randomValue = Random.Range(1f, 100f);

            if (randomValue <= 10f)
            {
                Instantiate(gunLootPrefab[Random.Range(0, gunLootPrefab.Count)], transform.position, Quaternion.identity);

            }
            else if (randomValue > 40f)
            {
                Instantiate(ammoLootPrefab[Random.Range(0, ammoLootPrefab.Count)], transform.position, Quaternion.identity);
            }
            else
            {
                Instantiate(healthKit, transform.position, Quaternion.identity);
            }
        }
    }
}
