using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{
    [HideInInspector] public int score;

    [SerializeField] private TextMeshProUGUI playerLivesUI;
    [SerializeField] private Slider dashGaugeSlider;
    [SerializeField] private Image dashGaugeFill;
    [SerializeField] private GameObject playerObject;
    [SerializeField] private GameObject deathMenu;
    [SerializeField] private TextMeshProUGUI scoreUI;
    [SerializeField] private Button DashGaugeButton;
    private Player player;
    private Dash dash;

    void Start()
    {
        deathMenu.SetActive(false);
        player = playerObject.GetComponent<Player>();
        dash = playerObject.GetComponent<Dash>();
    }

    void Update()
    {
        playerLivesUI.text = "Lives: " + player.playerLives;
        scoreUI.text = "Score: " + score;

        dashGaugeSlider.value = dash.dashGauge;

        if (player.isDead)
        {
            deathMenu.SetActive(true);
        }
    }

    public void DashGaugePressed()
    {
        if (dash.dashGauge == 1)
        {
            dash.isDash = true;
        }
    }
}
