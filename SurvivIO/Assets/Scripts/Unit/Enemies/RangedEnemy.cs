using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedEnemy : Unit
{
    private void Start()
    {
        HealthBar();
    }

    private void Update()
    {
        ManageHealth();
    }
}
