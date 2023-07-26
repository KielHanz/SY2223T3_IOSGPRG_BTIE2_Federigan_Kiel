using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Player : Unit
{
    [SerializeField] private Joystick _moveJoystick;
    [SerializeField] private Joystick _aimJoystick;
    public ButtonHold _button;
    private Rigidbody2D _rb2D;
    

    private void Start()
    {
        base.Initialize("Hunter", 100, 10);
        _rb2D = gameObject.GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        Movement();
        Aim();
        if (_button.buttonHeld && !GameManager.Instance._inventory._isSwitched)
        {
            Shoot();
            GameUI.Instance._currentAmmoUI.text = "" + _currentGun._currentAmmo;
        }

        if (!_currentGun._isReloading && _currentGun._isReloaded)
        {
            GameManager.Instance._inventory.ammos[(int)_currentGun._weaponType]._gunAmmoCarry = _currentGun._maxAmmo;
            GameUI.Instance._gunAmmoCarryUIs[(int)_currentGun._weaponType].text = GameManager.Instance._inventory.ammos[(int)_currentGun._weaponType]._gunAmmoCarry + "";
            GameUI.Instance._currentAmmoUI.text = "" + _currentGun._currentAmmo;
            GameUI.Instance._maxAmmoUI.text = "" + _currentGun._maxAmmo;
            _currentGun._isReloaded = false;
        }
    }

    public override void Shoot()
    {
        base.Shoot();
        Debug.Log($"{_name} is Shooting");
    }

    public override void Reload()
    {
        base.Reload();
    }

    public void SetCurrentGun(Gun gun)
    {
        _currentGun = gun;

    }

    private void Movement()
    {
        _rb2D.velocity = new Vector3(_moveJoystick.Horizontal * _speed, _moveJoystick.Vertical * _speed, 0);
    }

    private void Aim()
    {
        Vector3 moveVector = (Vector3.up * _aimJoystick.Vertical - Vector3.left * _aimJoystick.Horizontal);

        if (_aimJoystick.Horizontal != 0 || _aimJoystick.Vertical != 0)
        {
            transform.rotation = Quaternion.LookRotation(Vector3.forward, moveVector);
        }
    }
}
