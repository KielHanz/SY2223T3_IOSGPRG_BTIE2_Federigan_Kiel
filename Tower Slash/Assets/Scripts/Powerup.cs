using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{
    public int randomValue;

    [SerializeField] private int powerupChance;
    [SerializeField] private GameObject playerObject;
    private Player player;    

    void Start()
    {
        player = playerObject.GetComponent<Player>();
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
