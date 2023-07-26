using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private bool lootOnly;
    [SerializeField] private int numberOfLoot;
    [SerializeField] private int numberOfEnemies;

    [SerializeField] private Vector3 size;

    [SerializeField] private List<GameObject> _enemyPrefab;
    [SerializeField] private List<GameObject> _ammoPrefab;
    [SerializeField] private List<GameObject> _gunPrefab;

    [SerializeField] private GameObject _lootParent;

    public List<Unit> _enemies;
    
    private void Start()
    {
        SpawnLootable(numberOfLoot, _gunPrefab, _ammoPrefab);
        if (!lootOnly)
        {
            SpawnEnemies(numberOfEnemies, _enemyPrefab[0], "Brian Enemy", 100, 7);
            SpawnEnemies(1, _enemyPrefab[1], "Brian Boss", 1000, 3);
        }
    }

    private void SpawnEnemies(int count, GameObject prefab, string name, int maxHealth, float speed)
    {
        if (lootOnly)
        {
            return;
        }

        for (int i = 0; i < count; i++)
        {
            Vector3 randPos = RandomSpawn();

            GameObject enemyGO = Instantiate(prefab, randPos, Quaternion.identity);
            enemyGO.transform.parent = transform;

            Unit unit = enemyGO.GetComponent<Unit>();
            _enemies.Add(unit);

            unit.Initialize(name, maxHealth, speed);
        }
    }

    private void SpawnLootable(int count, List<GameObject> gunLootPrefab, List<GameObject> ammoLootPrefab)
    {
        for (int i = 0; i < count; i++)
        {
            float randomValue = Random.Range(0f, 100f);
            Vector3 randPos = RandomSpawn();

            if (randomValue <= 30f)
            {
                GameObject gunlootPref = Instantiate(gunLootPrefab[Random.Range(0, gunLootPrefab.Count)], randPos, Quaternion.identity);
                gunlootPref.transform.parent = _lootParent.transform;
            }
            else
            {
                GameObject ammolootPref = Instantiate(ammoLootPrefab[Random.Range(0, ammoLootPrefab.Count)], randPos, Quaternion.identity);
                ammolootPref.transform.parent = _lootParent.transform;
            }
        }
    }

    private Vector3 RandomSpawn()
    {
        float randomX = Random.Range(-size.x / 2, size.x / 2);
        float randomY = Random.Range(-size.y / 2, size.y / 2);

        Vector3 randomPosition = this.gameObject.transform.position + new Vector3(randomX, randomY, 0);

        return randomPosition;
    }
}
