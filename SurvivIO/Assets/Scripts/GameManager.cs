using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public Player _player;
    public Inventory _inventory;

    public void StartGame()
    {
        Health playerHealthScript = _player.gameObject.GetComponent<Health>();
        playerHealthScript._onDeath += GameOver;
    }

    public void GameOver()
    {
        _player = null;
        Debug.Log("Game is Over");
    }
}
