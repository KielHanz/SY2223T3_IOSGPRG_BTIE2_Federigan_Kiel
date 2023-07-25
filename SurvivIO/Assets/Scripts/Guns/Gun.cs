using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Gun : MonoBehaviour
{
    public int _maxAmmo;
    public int _damage;
    public int _currentAmmo;

    [SerializeField] private float _reloadSpeed;
    [SerializeField] private int _numberOfBullets;
    [SerializeField] protected float _fireRate;
    [SerializeField] protected float _spreadDegree;
    [SerializeField] protected int maxClip;

    public Transform nozzle;
    [SerializeField] protected GameObject bullet;
    public Sprite _logo;

    public Weapon _weaponType;
    public WeaponSlot _weaponSlotType;

    private bool isClipEmpty;
    private float _reloadTimer;
    protected float _fireTimer;
    protected Gun gun;
    protected GameObject gunObj;

    private void Awake()
    {
        _reloadTimer = _reloadSpeed;
    }

    private void Update()
    {

        if (_currentAmmo <= 0)
        {
            isClipEmpty = true;
        }

        if (_currentAmmo <= 0 && _maxAmmo > 0)
        {
            Reload();
        }

        if (!GameManager.Instance._player._button.buttonHeld && _fireRate <= 0)
        {
            _fireTimer = 0;
        }

        if (_fireTimer > 0)
        {
            _fireTimer -= Time.deltaTime;
        }
    }

    public virtual void Shoot()
    {
        if (isClipEmpty)
        {
            return;
        }

        if (_fireTimer <= 0 || _fireTimer == _fireRate)
        {
            gunObj = GameManager.Instance._player._currentGun.gameObject;

            if (gunObj != null)
            {
                for (int i = 0; i < _numberOfBullets; i++)
                {
                    float halfSpread = _spreadDegree / 2.0f;
                    float randomOffset = Random.Range(-halfSpread, halfSpread);
                    Instantiate(bullet, gunObj.GetComponent<Gun>().nozzle.transform.position, gunObj.transform.rotation * Quaternion.Euler(0, 0, randomOffset));
                }
                _currentAmmo--;
                GameUI.Instance._currentAmmoUI.text = "" + _currentAmmo;
            }
            _fireTimer = _fireRate;
        }
    }

    public virtual void Reload()
    {
        if (_reloadTimer > 0)
        {
            _reloadTimer -= Time.deltaTime;
        }

        if (_reloadTimer <= 0)
        {
            _maxAmmo -= maxClip - _currentAmmo;

            if (_maxAmmo < _currentAmmo && _currentAmmo == 0)
            {
                _currentAmmo = _maxAmmo + maxClip;
            }
            else
            {
                _currentAmmo = maxClip;
            }

            if (_maxAmmo < 0)
            {
                _maxAmmo = 0;
            }

            _reloadTimer = _reloadSpeed;
            isClipEmpty = false;
        }

        GameManager.Instance._inventory.ammos[(int)_weaponType]._gunAmmoCarry = _maxAmmo;
        GameUI.Instance._gunAmmoCarryUIs[(int)_weaponType].text = GameManager.Instance._inventory.ammos[(int)_weaponType]._gunAmmoCarry + "";
        GameUI.Instance._currentAmmoUI.text = "" + _currentAmmo;
        GameUI.Instance._maxAmmoUI.text = "" + _maxAmmo;
    }
}
