using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{
    [SerializeField] private int powerupChance;
    [SerializeField] private GameObject playerObject;
    private Player player;
    public int randomValue;
    void Start()
    {
        player = playerObject.GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void PowerupChance()
    {
        randomValue = Random.Range(0, 100);
        if (randomValue < powerupChance)
        {
            player.playerLives++;
        }

        Debug.Log("powerup called");
    }
}
