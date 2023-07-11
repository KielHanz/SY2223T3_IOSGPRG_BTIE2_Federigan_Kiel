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
        else if (weaponSlot == WeaponSlot.Secondary)
        {
            _player.SetCurrentGun(_secondaryWeapon);
        }
    }

    public void ChangeToPrimaryWeapon()
    {
        _player.SetCurrentGun(_primaryWeapon);

    }

    public void ChangeToSecondaryWeapon()
    {
        _player.SetCurrentGun(_secondaryWeapon);
    }

    public void AddAmmo(Weapon weapon, int amount)
    {
        for (int i = 0; i < ammos.Length; i++)
        {
            if ((int)weapon == i)
            {
                ammos[(int)weapon]._gunAmmoCarry += amount;
                ammos[(int)weapon]._gunAmmoCarry = Mathf.Min(ammos[(int)weapon]._gunAmmoCarry, ammos[(int)weapon]._gunAmmoMaxCarry);

            }
        }

        GameUI.Instance.pistolAmmoCarryUI.text = ammos[0]._gunAmmoCarry + " / 90";
        GameUI.Instance.shotgunAmmoCarryUI.text = ammos[1]._gunAmmoCarry + " / 60";
        GameUI.Instance.automaticRifleAmmoCarryUI.text = ammos[2]._gunAmmoCarry + " / 120";
    }
}
