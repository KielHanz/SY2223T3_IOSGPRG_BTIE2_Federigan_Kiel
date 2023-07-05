using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Health))]
public class Unit : MonoBehaviour
{
    [SerializeField] private Health _health;

    [SerializeField] protected string _name;

    [SerializeField] protected float _speed;

    [SerializeField] protected Gun _currentGun;

    [SerializeField] private GameObject _enemyHealthBar;
    [SerializeField] private Slider _enemyHpSlider;

    public void Initialize(string name, int maxHealth, float speed)
    {
        _name = name;
        gameObject.name = _name;

        _health = gameObject.GetComponent<Health>();
        _health.Initialize(maxHealth);

        _speed = speed;

        Debug.Log($"{name} has been initialized");
    }

    public virtual void Shoot()
    {
        Debug.Log($"{_name} is shooting");
        _currentGun.Shoot();
    }

    protected void HealthBar()
    {
        GameObject healthBar = Instantiate(_enemyHealthBar, transform.position + new Vector3(0, 1, 0), Quaternion.identity, transform);
        _enemyHpSlider = healthBar.transform.GetChild(0).GetComponentInChildren<Slider>();
        Debug.Log(_enemyHpSlider);
    }

    protected void ManageHealth()
    {
        _enemyHpSlider.value = (float)_health.CurrentHealth / (float)_health.MaxHealth;
    }
}
