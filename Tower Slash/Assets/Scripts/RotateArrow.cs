using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateArrow : MonoBehaviour
{
    private float rotateTimer = 0.5f;

    void Update()
    {
      if (rotateTimer > 0 )
        {
            rotateTimer -= Time.deltaTime;
        }
      if (rotateTimer <= 0)
        {
            this.transform.eulerAngles += new Vector3(0, 0, 90);
            if (this.transform.eulerAngles.z > 270)
            {
                this.transform.eulerAngles = new Vector3(0, 0, 0);
                Debug.Log("yes");
            }

            Debug.Log("euler: " + this.transform.eulerAngles.z);
            rotateTimer = 0.2f;
        }
    }
}
