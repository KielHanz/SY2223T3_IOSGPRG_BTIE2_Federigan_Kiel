using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pistol : Gun
{
    private void Update()
    {
        Reload();
    }
    public override void Shoot()
    {
        base.Shoot();
        Debug.Log("Single Shot");
    }
}
