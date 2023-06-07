using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dash : MonoBehaviour
{
    [SerializeField] private GameObject wallObject;
    private WallMovement wall;

    public float dashGauge;

    private float dashTimer;

    public bool isDash;

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
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began && dashGauge == 1 && Input.GetTouch(0).phase != TouchPhase.Moved)
        {
            isDash = true;

        }

        if (isDash)
        {
            if (dashTimer > 0)
            {
                wall.scrollSpeed = 4f;
                
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
