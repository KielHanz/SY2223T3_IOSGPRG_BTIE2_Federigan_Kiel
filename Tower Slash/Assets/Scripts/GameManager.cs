using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update


    [SerializeField]


    List<GameObject> enemiesList;

    Enemy enemy;

    [SerializeField]
    private Slash slashDir;

    void Start()
    {


    }

    // Update is called once per frame
    void Update()
    {


        checkSwipe();
    }

    void checkSwipe()
    {
        if (enemiesList == null)
            return;

        enemy = enemiesList[0].GetComponent<Enemy>();
        if (enemy.deathDirection == slashDir.slashDirection)
        {
            Debug.Log("Dead");
            enemiesList.Remove(enemiesList[0]);
            enemy.enemyHp = 0;
        
        }
    }


}
