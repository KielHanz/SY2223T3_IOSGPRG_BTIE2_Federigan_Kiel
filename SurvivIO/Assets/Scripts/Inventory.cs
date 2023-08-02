using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]

public struct Ammos
{
    public string _name;
    public Weapon _gunType;
    public int _gunAmmoMaxCarry;
    public int _gunAmmoCarry;
}

public class Inventory : MonoBehaviour
{
    public Ammos[] ammos;
    public bool _isSwitched;
    public bool _isHealing;
    public int _healthKits;
    public int _maxHealthKitCarry;

    private Player _player;

    private Gun _primaryWeapon;
    private Gun _secondaryWeapon;
    private int _prevGunAmmo;

    private float _switchTime;
    private float _switchTimer;
    private float _healTime;
    private float _healTimer;

    public float HealTime
    {
        get => _healTime;
    }

    public float HealTimer
    {
        get => _healTimer;
    }

    private void Start()
    {
        _player = GameManager.Instance._player;
        _switchTime = 1;
        _switchTimer = _switchTime;
        _healTime = 3;
        _healTimer = _healTime;
    }

    private void Update()
    {
        if (_isHealing && !_player.isShooting)
        {
            StartHealTimer();
            GameUI.Instance.UpdateHealTimeBar();
        }
        else if (_player.isShooting)
        {
            _isHealing = false;
            _healTimer = _healTime;
        }

        if (_switchTimer > 0 && _isSwitched)
        {
            _switchTimer -= Time.deltaTime;
        }

        if (_switchTimer <= 0)
        {
            _isSwitched = false;
            _switchTimer = _switchTime;
        }
    }

    public void PickUpWeapon(Gun gun)
    {
        _isSwitched = true;

        if (_primaryWeapon != null && _secondaryWeapon == null)
        {
            _prevGunAmmo = _player._currentGun._currentAmmo;
        }

        if ((_primaryWeapon == null || _primaryWeapon != null) && gun._weaponSlotType == WeaponSlot.Primary)
        {
            _primaryWeapon = gun;
            InstantiateWeapon(_primaryWeapon);

            _player._currentGun._currentAmmo = 0;

            GameUI.Instance.WeaponSlotColorChange(new Color(0, 0, 0, 1f), new Color(0, 0, 0, 0.5f));
            GameUI.Instance.primaryImageSlot(_primaryWeapon._logo);
        }
        else if (_secondaryWeapon == null && gun._weaponSlotType == WeaponSlot.Secondary)
        {
            _secondaryWeapon = gun;
            InstantiateWeapon(_secondaryWeapon);

            _player._currentGun._currentAmmo = 0;

            GameUI.Instance.WeaponSlotColorChange(new Color(0, 0, 0, 0.5f), new Color(0, 0, 0, 1f));
            GameUI.Instance.secondaryImageSlot(_secondaryWeapon._logo);
        }
    }

    public void OnPrimaryClick()
    {
        SwitchWeapon(WeaponSlot.Primary);
    }

    public void OnSecondaryClick()
    {
        SwitchWeapon(WeaponSlot.Secondary);
    }

    public void SwitchWeapon(WeaponSlot weaponSlot)
    {
        if (weaponSlot == WeaponSlot.Primary && _primaryWeapon != null)
        {
            _primaryWeapon._currentAmmo = _prevGunAmmo;
            _prevGunAmmo = _player._currentGun._currentAmmo;

            InstantiateWeapon(_primaryWeapon);

            GameUI.Instance.WeaponSlotColorChange(new Color(0, 0, 0, 1f), new Color(0, 0, 0, 0.5f));
        }
        else if (weaponSlot == WeaponSlot.Secondary && _secondaryWeapon != null)
        {
            _secondaryWeapon._currentAmmo = _prevGunAmmo;
            _prevGunAmmo = _player._currentGun._currentAmmo;

            InstantiateWeapon(_secondaryWeapon);

            GameUI.Instance.WeaponSlotColorChange(new Color(0, 0, 0, 0.5f), new Color(0, 0, 0, 1f));
        }
    }

    public void AddAmmo(Weapon weapon, int amount)
    {
        for (int i = 0; i < ammos.Length; i++)
        {
            if ((int)weapon == i)
            {
                ammos[(int)weapon]._gunAmmoCarry += amount;
                ammos[(int)weapon]._gunAmmoCarry = Mathf.Min(ammos[(int)weapon]._gunAmmoCarry, ammos[(int)weapon]._gunAmmoMaxCarry);
                GameUI.Instance._gunAmmoCarryUIs[i].text = ammos[i]._gunAmmoCarry + "";
            }
        }
        UpdateAmmo();
    }

    public void PickupHealthKit()
    {
        _healthKits++;
        _healthKits = Mathf.Min(_healthKits, _maxHealthKitCarry);
        GameUI.Instance.UpdateHealthKitAmount();

    }

    public void StartHealTimer()
    {
        if (_healTimer > 0)
        {
            _healTimer -= Time.deltaTime;
            Debug.Log(_healTimer);
        }

        if (_healTimer <= 0)
        {
            Health health = _player.GetComponent<Health>();
            health.CurrentHealth += 30;
            health.CurrentHealth = Mathf.Min(health.CurrentHealth, health.MaxHealth);
            _healthKits--;
            GameUI.Instance.UpdatePlayerHealth();
            GameUI.Instance.UpdateHealthKitAmount();

            _healTimer = _healTime;
            _isHealing = false;
        }
    }

    private void UpdateAmmo()
    {
        for (int i = 0; i < ammos.Length; i++)
        {
            if (_player._currentGun != null && (int)_player._currentGun._weaponType == i)
            {
                _player._currentGun._maxAmmo = ammos[i]._gunAmmoCarry;
                GameUI.Instance.UpdateAmmoUI();
            }
        }
    }

    private void InstantiateWeapon(Gun gun)
    {
        if (_player._currentGun != null && _player._currentGun._weaponType != Weapon.None)
        {
            Destroy(_player._currentGun.gameObject);
        }

        GameObject _tempGun = Instantiate(gun.gameObject, _player.transform);
        _tempGun.gameObject.SetActive(true);
        _player.SetCurrentGun(_tempGun.GetComponent<Gun>());
        _player._currentGun.gunObj = this.transform.gameObject;
        UpdateAmmo();
    }
}
