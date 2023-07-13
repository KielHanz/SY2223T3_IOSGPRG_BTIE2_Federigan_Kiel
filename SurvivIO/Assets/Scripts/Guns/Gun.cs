using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;
using static UnityEditor.PlayerSettings;

public class Gun : MonoBehaviour
{
    public int _maxAmmo;
    public int _damage;
    public Weapon _weaponType;
    public Transform nozzle;

    [SerializeField]protected float _fireRate;
    [SerializeField]protected float _spread;
    [SerializeField] protected GameObject bullet;
    protected Gun gun;
    protected Gun prevGun;

    public int _currentAmmo;
    [SerializeField]protected int maxClip;

    private bool isEmpty;

    private void Start()
    {
        if (gun != null)
        {
            gun._currentAmmo = maxClip;
            GameUI.Instance._currentAmmoUI.text = "" + gun._currentAmmo;
        }
    }

    public virtual void Shoot()
    {

        Debug.Log("Base Gun Shooting");

        //temporary
        gun = GameManager.Instance._inventory._gun;
      

        if (gun != null)
        {
            Instantiate(bullet, gun.nozzle.transform.position, gun.transform.rotation);
        }
        gun._currentAmmo--;

        Reload();

        GameUI.Instance._currentAmmoUI.text = "" + gun._currentAmmo;
        GameUI.Instance._maxAmmoUI.text = "" + _maxAmmo;
    }

    public virtual void Reload()
    {

        gun = GameManager.Instance._inventory._gun;
        if (gun._currentAmmo <= 0 && _maxAmmo <= 0)
        {
            gun._currentAmmo = 0;
            isEmpty = true;
        }
        else if (gun._currentAmmo > 0 || _maxAmmo > 0)
        {
            isEmpty = false;
        }

        if (isEmpty)
        {
            return;
        }
  
        if (gun._currentAmmo == 0)
        {
            Debug.Log("reloading");
            if (_maxAmmo > maxClip)
            {
                _maxAmmo -= maxClip;
            }

            if (_maxAmmo > 0 || gun._currentAmmo > 0)
            {
                gun._currentAmmo = maxClip;
            }
            if (_maxAmmo < maxClip)
            {
                gun._currentAmmo = _maxAmmo;
                _maxAmmo = 0;

            }
            GameUI.Instance._currentAmmoUI.text = "" + gun._currentAmmo;
            GameUI.Instance._maxAmmoUI.text = "" + _maxAmmo;
        }

    }
}
