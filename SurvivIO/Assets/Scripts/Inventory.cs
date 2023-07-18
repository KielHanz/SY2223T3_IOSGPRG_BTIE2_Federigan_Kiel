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
    private Player _player;

    public Ammos[] ammos;

    private int _healthKits;

    public Gun _primaryWeapon;
    public Gun _secondaryWeapon;

    public bool _isPrimary;
    public bool _isSecondary;

    public GameObject _tempGun;
    private int _prevGunAmmo;

    private void Start()
    {
        _player = GameManager.Instance._player;
        GameUI.Instance._primaryBtn.GetComponent<Image>().color = new Color(0, 0, 0, 1f);
        GameUI.Instance._secondaryBtn.GetComponent<Image>().color = new Color(0, 0, 0, 0.5f);
    }

    private void UpdateAmmo()
    {
        for (int i = 0; i < ammos.Length; i++)
        {
            if ((int)_player._currentGun._weaponType == i)
            {
                _player._currentGun._maxAmmo = ammos[i]._gunAmmoCarry;
                GameUI.Instance._maxAmmoUI.text = "" + _player._currentGun.GetComponent<Gun>()._maxAmmo;
            }
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log(_player._currentGun._maxAmmo);
        }
    }

    private void InstantiateWeapon(Gun gun)
    {
        Destroy(_tempGun);
        _tempGun = Instantiate(gun.gameObject, _player.transform);
        _tempGun.SetActive(true);
        _player.SetCurrentGun(_tempGun.GetComponent<Gun>());
        UpdateAmmo();
    }

    public void ChangeWeapon(WeaponSlot weaponSlot)
    {
        if (weaponSlot == WeaponSlot.Primary)
        {
            _player.SetCurrentGun(_primaryWeapon);
        }
        else if (weaponSlot == WeaponSlot.Secondary)
        {
            _player.SetCurrentGun(_secondaryWeapon);
        }
    }

    public void PickUpWeapon(Gun gun)
    {
        if (_primaryWeapon == null || (_primaryWeapon != null && _secondaryWeapon != null && _isPrimary))
        {
            _primaryWeapon = gun;
            _player.SetCurrentGun(_primaryWeapon);
            InstantiateWeapon(_primaryWeapon);
            _isPrimary = true;
            _isSecondary = false;

        }
        else if (_primaryWeapon != null && _secondaryWeapon == null || (_primaryWeapon != null && _secondaryWeapon != null && _isSecondary))
        {
            _prevGunAmmo = _tempGun.GetComponent<Gun>()._currentAmmo;
            _secondaryWeapon = gun;
            _player.SetCurrentGun(_secondaryWeapon);
            InstantiateWeapon(_secondaryWeapon);
            _isPrimary = false;
            _isSecondary = true;

            Debug.Log(_tempGun.GetComponent<Gun>()._currentAmmo);

            GameUI.Instance._primaryBtn.GetComponent<Image>().color = new Color(0, 0, 0, 0.5f);
            GameUI.Instance._secondaryBtn.GetComponent<Image>().color = new Color(0, 0, 0, 1f);
        }

        GameUI.Instance._currentAmmoUI.text = "" + _tempGun.GetComponent<Gun>()._currentAmmo;
    }

    public void ChangeToPrimaryWeapon()
    {
        if (_primaryWeapon == null)
        {
            return;
        }

        if (!_isPrimary)
        {
            _primaryWeapon._currentAmmo = _prevGunAmmo;
            _prevGunAmmo = _player._currentGun._currentAmmo;
            //_prevGunAmmo = _tempGun.GetComponent<Gun>()._currentAmmo;
            InstantiateWeapon(_primaryWeapon);

        }

        //_player.SetCurrentGun(_primaryWeapon);
        _isPrimary = true;
        _isSecondary = false;

        GameUI.Instance._primaryBtn.GetComponent<Image>().color = new Color(0, 0, 0, 1f);
        GameUI.Instance._secondaryBtn.GetComponent<Image>().color = new Color(0, 0, 0, 0.5f);
        GameUI.Instance._currentAmmoUI.text = "" + _tempGun.GetComponent<Gun>()._currentAmmo;
    }

    public void ChangeToSecondaryWeapon()
    {
        if (_secondaryWeapon == null)
        {
            return;
        }

        if (!_isSecondary)
        {
            _secondaryWeapon._currentAmmo = _prevGunAmmo;
            _prevGunAmmo = _player._currentGun._currentAmmo;
            //_prevGunAmmo = _tempGun.GetComponent<Gun>()._currentAmmo;
            Debug.Log(_primaryWeapon._currentAmmo);
            InstantiateWeapon(_secondaryWeapon);

        }

        // _player.SetCurrentGun(_secondaryWeapon);
        _isPrimary = false;
        _isSecondary = true;

        GameUI.Instance._primaryBtn.GetComponent<Image>().color = new Color(0, 0, 0, 0.5f);
        GameUI.Instance._secondaryBtn.GetComponent<Image>().color = new Color(0, 0, 0, 1f);
        GameUI.Instance._currentAmmoUI.text = "" + _tempGun.GetComponent<Gun>()._currentAmmo;
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
