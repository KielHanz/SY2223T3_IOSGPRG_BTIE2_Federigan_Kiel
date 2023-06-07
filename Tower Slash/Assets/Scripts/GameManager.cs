using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject playerObject;
    private Player player;
    void Start()
    {
        Time.timeScale = 1;
        player = playerObject.GetComponent<Player>();

    }
    void Update()
    {
        if(player.playerLives <= 0)
        {
            player.isDead = true;
            Time.timeScale = 0;
        }
    }

    public void Retry()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

    }

}
