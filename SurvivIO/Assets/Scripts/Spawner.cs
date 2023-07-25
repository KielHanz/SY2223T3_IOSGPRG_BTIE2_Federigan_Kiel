using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private List<GameObject> _enemyPrefab;
    [SerializeField] private List<GameObject> _ammoPrefab;
    [SerializeField] private List<GameObject> _gunPrefab;
    [SerializeField] private GameObject _lootParent;

    public List<Unit> _enemies;
    
    private void Start()
    {
        SpawnEnemies(5, _enemyPrefab[0], "Brian melee", 100, 5);
        SpawnEnemies(20, _enemyPrefab[1], "Brian Ranged", 75, 7);
        SpawnEnemies(1, _enemyPrefab[2], "Brian Boss", 1000, 3);

        SpawnLootable(10, _gunPrefab);
        SpawnLootable(100, _ammoPrefab);
    }

    private void SpawnEnemies(int count, GameObject prefab, string name, int maxHealth, float speed)
    {
        for (int i = 0; i < count; i++)
        {
            Vector3 randPos = RandomSpawn(80, 40);

            GameObject enemyGO = Instantiate(prefab, randPos, Quaternion.identity);
            enemyGO.transform.parent = transform;

            Unit unit = enemyGO.GetComponent<Unit>();
            _enemies.Add(unit);

            unit.Initialize(name, maxHealth, speed);
        }
    }

    private void SpawnLootable(int count, List<GameObject> gameObject)
    {
        for (int i = 0; i < count; i++)
        {
            Vector3 randPos = RandomSpawn(80, 40);

            GameObject lootPref = Instantiate(gameObject[Random.Range(0, gameObject.Count)], randPos, Quaternion.identity);
            lootPref.transform.parent = _lootParent.transform;
        }
    }

    private Vector3 RandomSpawn(float _randomX, float _randomY)
    {
        float randomX = Random.Range(-_randomX, _randomX);
        float randomY = Random.Range(-_randomY, _randomY);

        Vector3 randomPosition = new Vector3(randomX, randomY, 0);

        return randomPosition;
    }
}
