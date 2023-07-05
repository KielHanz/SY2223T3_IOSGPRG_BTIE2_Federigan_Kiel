using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemy : Unit
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
