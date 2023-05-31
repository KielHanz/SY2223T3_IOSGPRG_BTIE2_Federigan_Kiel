using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    public float deathDirection;

    [HideInInspector]
    public float enemyHp;


    [SerializeField]
    List<GameObject> arrows;

    SpriteRenderer render;
    // Start is called before the first frame update
    void Start()
    {

  

        int randomIdx = Random.Range(0, arrows.Count);

        Quaternion arrowRotation = transform.rotation;
        switch (deathDirection)
        {
            case 1:
                arrowRotation = Quaternion.Euler(0, 0, -90);
                break;
            case 2:
                arrowRotation = Quaternion.Euler(0, 0, 90);
                break;
            case 3:
                arrowRotation = Quaternion.Euler(0, 0, 0);
                break;
            case 4:
                arrowRotation = Quaternion.Euler(0,0,180);
                break;
        }
        if (arrows[randomIdx] == arrows[1])
        {
            render = arrows[1].GetComponent<SpriteRenderer>();
            render.flipX = true;
        }


            GameObject arrow = Instantiate(arrows[randomIdx], transform.position + new Vector3(-1, -1, 0), arrowRotation);
            arrow.transform.SetParent(transform, true);
    }

    // Update is called once per frame
    void Update()
    {


        if (enemyHp <= 0)
        {
            Destroy(this.gameObject);
        }
    }
}
