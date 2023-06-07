using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI playerLivesUI;

    [SerializeField] private Slider dashGaugeSlider;
    [SerializeField] private Image dashGaugeFill;

    [SerializeField] private GameObject playerObject;

    [SerializeField] private GameObject deathMenu;


    private Player player;
    private Dash dash;
    void Start()
    {
        deathMenu.SetActive(false);
        player = playerObject.GetComponent<Player>();
        dash = playerObject.GetComponent<Dash>();
    }

    // Update is called once per frame
    void Update()
    {
        playerLivesUI.text = "Lives: " + player.playerLives;

        dashGaugeSlider.value = dash.dashGauge;

        if (player.isDead)
        {
            deathMenu.SetActive(true);
        }
    }
}
