using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DownMovement : MonoBehaviour
{
    [SerializeField] private float speed;

    // Start is called before the first frame update
    void Start()
    {
   
    }

    // Update is called once per frame
    void Update()
    {

        transform.Translate(Vector2.down * speed * Time.deltaTime);
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("WallEnd"))
        {

            Destroy(this.gameObject);
        }
    }
    


}
