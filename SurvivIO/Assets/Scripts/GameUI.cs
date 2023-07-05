using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI pistolAmmoCarryUI;
    [SerializeField] private TextMeshProUGUI shotgunAmmoCarryUI;
    [SerializeField] private TextMeshProUGUI automaticRifleAmmoCarryUI;

    [SerializeField] private GameObject player;

    [SerializeField] private Inventory inventory;

    [SerializeField] private Slider hpSlider;

    private Health health;

    private void Start()
    {
        health = player.GetComponent<Health>();

    }

    private void Update()
    {
        hpSlider.value = (float)health.CurrentHealth / (float)health.MaxHealth;

        pistolAmmoCarryUI.text = inventory.ammos[0]._gunAmmoCarry + " / 90";
        shotgunAmmoCarryUI.text = inventory.ammos[1]._gunAmmoCarry + " / 60";
        automaticRifleAmmoCarryUI.text = inventory.ammos[2]._gunAmmoCarry + " / 120";
    }
}
