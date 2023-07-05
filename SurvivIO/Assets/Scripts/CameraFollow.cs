using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private GameObject target;
    private void Update()
    {
        transform.position = new Vector3(target.transform.position.x, target.transform.position.y, -10);

    }
}