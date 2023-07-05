using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    [SerializeField] private Player _player;
    public Ammos[] ammos;

    private int _healthKits;

    private Gun _primaryWeapon;
    private Gun _secondaryWeapon;

    public void ChangeWeapon(WeaponSlot weaponSlot)
    {
        if (weaponSlot == WeaponSlot.Primary)
        {
            _player.SetCurrentGun(_primaryWeapon);
        }

    }

    public void AddAmmo(Weapon weapon, int amount)
    {
        
        if (weapon == Weapon.Pistol)
        {
            ammos[0]._gunAmmoCarry += amount;

            ammos[0]._gunAmmoCarry = Mathf.Min(ammos[0]._gunAmmoCarry, ammos[0]._gunAmmoMaxCarry);
        }

        if (weapon == Weapon.Shotgun)
        {
            ammos[1]._gunAmmoCarry += amount;

            ammos[1]._gunAmmoCarry = Mathf.Min(ammos[1]._gunAmmoCarry, ammos[1]._gunAmmoMaxCarry);
        }

        if (weapon == Weapon.AutomaticRifle)
        {
            ammos[2]._gunAmmoCarry += amount;

            ammos[2]._gunAmmoCarry = Mathf.Min(ammos[2]._gunAmmoCarry, ammos[2]._gunAmmoMaxCarry);
        }
    }
}
