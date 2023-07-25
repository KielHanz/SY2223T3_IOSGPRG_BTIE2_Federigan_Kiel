using System.Collections;
using System.Collections.Generic;
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

    private int _healthKits;

    private Player _player;

    private Gun _primaryWeapon;
    private Gun _secondaryWeapon;
    private int _prevGunAmmo;

    private bool _isPrimary;
    private bool _isSecondary;
    public bool _isSwitched;

    private float _switchTime;
    private float _switchTimer;

    private void Start()
    {
        _player = GameManager.Instance._player;
        GameUI.Instance.WeaponSlotColorChange(new Color(0, 0, 0, 1f), new Color(0, 0, 0, 0.5f));
        _switchTime = 1;
        _switchTimer = _switchTime;
    }

    private void Update()
    {
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

    private void UpdateAmmo()
    {
        for (int i = 0; i < ammos.Length; i++)
        {
            if ((int)_player._currentGun._weaponType == i)
            {
                _player._currentGun._maxAmmo = ammos[i]._gunAmmoCarry;
                GameUI.Instance.UpdateAmmoUI();
            }
        }
    }

    private void InstantiateWeapon(Gun gun)
    {
        if (_player._currentGun._weaponType != Weapon.None)
        {
            Destroy(_player._currentGun.gameObject);
        }
      
        GameObject _tempGun = Instantiate(gun.gameObject, _player.transform);
        _tempGun.gameObject.SetActive(true);
        _player.SetCurrentGun(_tempGun.GetComponent<Gun>());
        UpdateAmmo();
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
            _isPrimary = true;
            _isSecondary = false;

            GameUI.Instance.WeaponSlotColorChange(new Color(0, 0, 0, 1f), new Color(0, 0, 0, 0.5f));
            GameUI.Instance.primaryImageSlot(_primaryWeapon._logo);
        }
        else if (_secondaryWeapon == null && gun._weaponSlotType == WeaponSlot.Secondary)
        {
            _secondaryWeapon = gun;
            InstantiateWeapon(_secondaryWeapon);

            _player._currentGun._currentAmmo = 0;
            _isPrimary = false;
            _isSecondary = true;

            GameUI.Instance.WeaponSlotColorChange(new Color(0, 0, 0, 0.5f), new Color(0, 0, 0, 1f));
            GameUI.Instance.secondaryImageSlot(_secondaryWeapon._logo);
        }
    }

    public void ChangeToPrimaryWeapon()
    {
        if (_primaryWeapon == null)
        {
            return;
        }

        _isSwitched = true;

        if (!_isPrimary)
        {
            _primaryWeapon._currentAmmo = _prevGunAmmo;
            _prevGunAmmo = _player._currentGun._currentAmmo;

            InstantiateWeapon(_primaryWeapon);
        }

        _isPrimary = true;
        _isSecondary = false;

        GameUI.Instance.WeaponSlotColorChange(new Color(0, 0, 0, 1f), new Color(0, 0, 0, 0.5f));

    }

    public void ChangeToSecondaryWeapon()
    {
        if (_secondaryWeapon == null)
        {
            return;
        }

        _isSwitched = true;

        if (!_isSecondary)
        {
            _secondaryWeapon._currentAmmo = _prevGunAmmo;
            _prevGunAmmo = _player._currentGun._currentAmmo;
             InstantiateWeapon(_secondaryWeapon);
        }

        _isPrimary = false;
        _isSecondary = true;

        GameUI.Instance.WeaponSlotColorChange(new Color(0, 0, 0, 0.5f), new Color(0, 0, 0, 1f));
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
}
