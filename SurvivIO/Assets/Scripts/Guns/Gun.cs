using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Gun : MonoBehaviour
{
    public int _maxAmmo;
    public int _damage;
    public Weapon _weaponType;
    public Transform nozzle;

    [SerializeField] protected float _fireRate;
    [SerializeField] protected float _spread;

    [SerializeField] protected GameObject bullet;
    protected Gun gun;

    private GameObject gunObj;

    public int _currentAmmo;
    [SerializeField] protected int maxClip;

    private bool isClipEmpty;
    private float reloadTime = 3;
    private float tempReloadTimer;

    private void Awake()
    {
        tempReloadTimer = 3;
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

    }

    public virtual void Shoot()
    {
        if (isClipEmpty)
        {
            return;
        }

        gunObj = GameManager.Instance._inventory._tempGun;


        if (gunObj != null)
        {
            Instantiate(bullet, gunObj.GetComponent<Gun>().nozzle.transform.position, gunObj.transform.rotation);
            _currentAmmo--;
            GameUI.Instance._currentAmmoUI.text = "" + _currentAmmo;
        }

    }

    public virtual void Reload()
    {
        if (tempReloadTimer > 0)
        {
            tempReloadTimer -= Time.deltaTime;
        }

        if (tempReloadTimer <= 0)
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

            tempReloadTimer = reloadTime;
            isClipEmpty = false;
        }

        GameManager.Instance._inventory.ammos[(int)_weaponType]._gunAmmoCarry = _maxAmmo;
        GameUI.Instance._currentAmmoUI.text = "" + _currentAmmo;
        GameUI.Instance._maxAmmoUI.text = "" + _maxAmmo;

    }
}
