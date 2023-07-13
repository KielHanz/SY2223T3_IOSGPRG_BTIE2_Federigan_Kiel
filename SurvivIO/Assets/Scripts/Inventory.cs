using System.Collections;
using System.Collections.Generic;
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
    private int _currAmmoHolder;
    [HideInInspector]public GameObject _currentGunHolder;
    [HideInInspector]public Gun _gun;

    public Ammos[] ammos;

    private int _healthKits;

    public Gun _primaryWeapon;
    public Gun _secondaryWeapon;
    
    public bool _isPrimary;
    public bool _isSecondary;

    private void Start()
    {
        _player = GameManager.Instance._player;
        GameUI.Instance._primaryBtn.GetComponent<Image>().color = new Color(0, 0, 0, 1f);
        GameUI.Instance._secondaryBtn.GetComponent<Image>().color = new Color(0, 0, 0, 0.5f);
    }

    private void ShowGun(Gun gun)
    {
        if (_gun != null)
        {
            _currAmmoHolder = _gun._currentAmmo;
        }
        Destroy(_currentGunHolder);
        _currentGunHolder = Instantiate(gun.gameObject, _player.transform);
        _gun = _currentGunHolder.GetComponent<Gun>();

        GameUI.Instance._currentAmmoUI.text = "" + _gun._currentAmmo;
    }

    private void UpdateAmmo()
    {
        for (int i = 0; i < ammos.Length; i++)
        {
            if ((int)_player._currentGun._weaponType == i)
            {
                _player._currentGun.GetComponent<Gun>()._maxAmmo = ammos[i]._gunAmmoCarry;
                GameUI.Instance._maxAmmoUI.text = "" + _player._currentGun.GetComponent<Gun>()._maxAmmo;

            }
            
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log(_player._currentGun._currentAmmo);
        }
        GameUI.Instance._maxAmmoUI.text = "" + GameManager.Instance._player._currentGun.GetComponent<Gun>()._maxAmmo;

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

    public void PickUpWeapon(Gun gun, GameObject gunObj)
    {
        if (_primaryWeapon == null || (_primaryWeapon != null && _secondaryWeapon != null && _isPrimary))
        {
            _primaryWeapon = gun;
            _player.SetCurrentGun(gun);
            _isPrimary = true;
            _isSecondary = false;

            ShowGun(_primaryWeapon);
            UpdateAmmo();


            GameUI.Instance._currentAmmoUI.text = "" + _gun._currentAmmo;
        }
        else if (_primaryWeapon != null && _secondaryWeapon == null || (_primaryWeapon != null && _secondaryWeapon != null && _isSecondary))
        {
            _secondaryWeapon = gun;
            _player.SetCurrentGun(gun);
            _isPrimary = false;
            _isSecondary = true;

            GameUI.Instance._primaryBtn.GetComponent<Image>().color = new Color(0, 0, 0, 0.5f);
            GameUI.Instance._secondaryBtn.GetComponent<Image>().color = new Color(0, 0, 0, 1f);

            ShowGun(_secondaryWeapon);
            UpdateAmmo();

            GameUI.Instance._currentAmmoUI.text = "" + _gun._currentAmmo;
        }


    }

    public void ChangeToPrimaryWeapon()
    {
        if (_primaryWeapon == null)
        {
            return;
        }
        _primaryWeapon._currentAmmo = _currAmmoHolder;

        if (!_isPrimary)
        {

            ShowGun(_primaryWeapon);
        }

        _player.SetCurrentGun(_primaryWeapon);
        _isPrimary = true;
        _isSecondary = false;

        GameUI.Instance._currentAmmoUI.text = "" + _gun._currentAmmo;
        GameUI.Instance._primaryBtn.GetComponent<Image>().color = new Color(0, 0, 0, 1f);
        GameUI.Instance._secondaryBtn.GetComponent<Image>().color = new Color(0, 0, 0, 0.5f);
        UpdateAmmo();
    }

    public void ChangeToSecondaryWeapon()
    {
        if (_secondaryWeapon == null)
        {
            return;
        }
        _secondaryWeapon._currentAmmo = _currAmmoHolder;

        if (!_isSecondary)
        {

            ShowGun(_secondaryWeapon);

        }

        _player.SetCurrentGun(_secondaryWeapon);
        _isPrimary = false;
        _isSecondary = true;

        GameUI.Instance._currentAmmoUI.text = "" + _gun._currentAmmo;
        GameUI.Instance._primaryBtn.GetComponent<Image>().color = new Color(0, 0, 0, 0.5f);
        GameUI.Instance._secondaryBtn.GetComponent<Image>().color = new Color(0, 0, 0, 1f);
        UpdateAmmo();
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

    public void DeductAmmo(Weapon weapon, int amount)
    {
        for (int i = 0; i < ammos.Length; i++)
        {
            if ((int)weapon == i)
            {
                ammos[(int)weapon]._gunAmmoCarry -= amount;
                ammos[(int)weapon]._gunAmmoCarry = Mathf.Max(ammos[(int)weapon]._gunAmmoCarry, 0);
                GameUI.Instance._gunAmmoCarryUIs[i].text = ammos[i]._gunAmmoCarry + "";
            }
        }
    }    
}
