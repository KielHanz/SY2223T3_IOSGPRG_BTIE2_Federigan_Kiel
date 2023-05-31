using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float gravityToWall;
    public Rigidbody2D rb;
    public Transform player;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

    }

    // Update is called once per frame
    void Update()
    {
   


     }

    void FixedUpdate()
    {
        rb.velocity = Vector2.right * gravityToWall * Time.deltaTime;
    }


}
