using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : Singleton<Spawner>
{
    [SerializeField] private GameObject _meleeEnemyPrefab;
    [SerializeField] private GameObject _rangedEnemyPrefab;
    [SerializeField] private GameObject _bossEnemyPrefab;

    [SerializeField] private List<GameObject> _ammoPrefab;
    [SerializeField] private List<GameObject> _gunPrefab;
    [SerializeField] private GameObject _lootParent;
    private int spawnEnemyTries;
    private int spawnLootTries;

    public List<Unit> _enemies;
    
    private void Start()
    {
        SpawnEnemies(5, _meleeEnemyPrefab, "Brian melee", 100, 5);
        SpawnEnemies(3,_rangedEnemyPrefab, "Brian Ranged", 75, 7);
        SpawnEnemies(1, _bossEnemyPrefab, "Brian Boss", 1000, 3);
        SpawnLootable(10, _gunPrefab);
        SpawnLootable(100, _ammoPrefab);

    }

    private void SpawnEnemies(int count, GameObject prefab, string name, int maxHealth, float speed)
    {
        if (spawnEnemyTries > 10)
        {
            return;
        }
        spawnEnemyTries++;
        float _randomX;
        float _randomY;
        Vector3 _randomPosition;

        for (int i = 0; i < count; i++)
        {
            _randomX = Random.Range(-80, 80);
            _randomY = Random.Range(-40, 40);
            _randomPosition = new Vector3(_randomX, _randomY, 0);
            GameObject enemyGO = null;

            Collider2D[] collider = Physics2D.OverlapCircleAll(_randomPosition, 1f);

            if (collider.Length <= 1 && collider.Length == 0)
            {
                enemyGO = Instantiate(prefab, _randomPosition, Quaternion.identity);
                enemyGO.transform.parent = transform;

                Unit unit = enemyGO.GetComponent<Unit>();
                _enemies.Add(unit);

                unit.Initialize(name, maxHealth, speed);
            }
            if (collider.Length > 1)
            {
                Destroy(enemyGO);
                SpawnEnemies(count, prefab, name, maxHealth, speed);
            }
        }
    }

    private void SpawnLootable(int count, List<GameObject> gameObject)
    {
        if (spawnLootTries > 10)
        {
            return;
        }
        spawnLootTries++;
        float randomX;
        float randomY;

        Vector3 randomPosition;

        for (int i = 0; i < count; i++)
        {
            randomX = Random.Range(-80, 80);
            randomY = Random.Range(-40, 40);

            randomPosition = new Vector3(randomX, randomY, 0);

            Collider2D[] collider = Physics2D.OverlapCircleAll(randomPosition, 1f);
            GameObject lootPref = null;
            
            if (collider.Length <= 1 && collider.Length == 0)
            {
                lootPref = Instantiate(gameObject[Random.Range(0, gameObject.Count)], randomPosition, Quaternion.identity);
                lootPref.transform.parent = _lootParent.transform;
            }
            if (collider.Length > 1)
            {
                Destroy(lootPref);
                SpawnLootable(count, gameObject);
            }

        }
    }
}
