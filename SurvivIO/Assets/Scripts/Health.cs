using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public int MaxHealth
    {
        get => _maxHealth;
    }

    public int CurrentHealth
    {
        get => _currentHealth;
    }

    [SerializeField] private int _maxHealth;
    [SerializeField] private int _currentHealth;

    public Action _onDeath;

    private void OnEnable()
    {
        _onDeath += OnDeath;
    }

    private void OnDisable()
    {
        _onDeath -= OnDeath;
    }

    public void Initialize(int maxHealth)
    {
        _maxHealth = maxHealth;
        _currentHealth = maxHealth;
    }
    public void TakeDamage(int damage)
    {
        _currentHealth -= damage;
        _currentHealth = Mathf.Max(_currentHealth, 0);

        if (_currentHealth <= 0)
        {
            _onDeath?.Invoke();
            Destroy(gameObject);
        }
    }

    public void AddHealth(int amount)
    {
        _currentHealth += amount;
        _currentHealth = Mathf.Min(_currentHealth, _maxHealth);
    }

    private void OnDeath()
    {
        Debug.Log($"{gameObject.name} is Dead!");
    }
}
