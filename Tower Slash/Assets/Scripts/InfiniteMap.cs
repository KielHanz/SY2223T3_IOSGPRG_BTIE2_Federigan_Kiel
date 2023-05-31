using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class InfiniteMap : MonoBehaviour
{
    [SerializeField] private GameObject wall;
    [SerializeField] private GameObject wallStart;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void spawnWall()
    {
        Instantiate(wall, wallStart.transform.position, wallStart.transform.rotation);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Wall"))
        {
            spawnWall();
        }

    }

}
