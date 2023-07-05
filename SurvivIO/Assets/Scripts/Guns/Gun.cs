using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] private int _damage;
    [SerializeField] private int _maxAmmo;
    [SerializeField] private float _fireRate;
    [SerializeField] private float _spread;

    private int _currentAmmo;

    public virtual void Shoot()
    {
        Debug.Log("Base Gun Shooting");
    }

    //private void Reload()
    //{

    //}
}
