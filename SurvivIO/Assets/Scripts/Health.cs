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
        set => _currentHealth = value;
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

    public void Heal()
    {
        Inventory inventory = GameManager.Instance._inventory;

        if (_currentHealth < _maxHealth && inventory._healthKits > 0)
        {
            inventory._isHealing = true;
        }
    }

    private void OnDeath()
    {
        Debug.Log($"{gameObject.name} is Dead!");
    }
}
