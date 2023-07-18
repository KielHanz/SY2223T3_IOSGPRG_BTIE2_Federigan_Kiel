using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Health))]
public class Unit : MonoBehaviour
{

    public Gun _currentGun;

    [SerializeField] private Health _health;

    [SerializeField] protected string _name;
    [SerializeField] protected float _speed;

    [SerializeField] private GameObject _enemyHealthBar;
    [SerializeField] private Slider _enemyHpSlider;
    private GameObject _healthBar;

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

    public virtual void Reload()
    {
        _currentGun.Reload();
    }

    protected void HealthBar()
    {
        _healthBar = Instantiate(_enemyHealthBar, transform.position + new Vector3(0, 1, 0), Quaternion.identity, transform);
        _healthBar.SetActive(false);
        _enemyHpSlider = _healthBar.transform.GetChild(0).GetComponentInChildren<Slider>();
        Debug.Log(_enemyHpSlider);
    }

    public void ManageHealth()
    {
        _healthBar.SetActive(true);
        _enemyHpSlider.value = (float)_health.CurrentHealth / (float)_health.MaxHealth;
    }
}
