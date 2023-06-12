using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dash : MonoBehaviour
{
    public float dashGauge;
    public bool isDash;

    private WallMovement wall;
    [SerializeField] private GameObject wallObject;
    private float dashTimer;

    void Start()
    {
        dashTimer = 3f;
        dashGauge = 0;

        wall = wallObject.GetComponent<WallMovement>(); 
    }

    void Update()
    {
        if (dashGauge >= 1)
        {
            dashGauge = 1;
        }

        if (isDash)
        {
            if (dashTimer > 0)
            {
                wall.scrollSpeed = Time.unscaledTime;
                
                dashTimer -= Time.deltaTime;
            }
            if (dashTimer <= 0)
            {
                dashGauge = 0;
                wall.scrollSpeed = 2.25f;
                dashTimer = 3f;
                isDash = false;
            }
        }
    }

    public void increaseDashPoints()
    {
        dashGauge += 0.1f;
    }
}
