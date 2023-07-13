using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameUI : Singleton<GameUI>
{
    public List<TextMeshProUGUI> _gunAmmoCarryUIs;
    public TextMeshProUGUI _pistolAmmoCarryUI;
    public TextMeshProUGUI _shotgunAmmoCarryUI;
    public TextMeshProUGUI _automaticRifleAmmoCarryUI;

    public TextMeshProUGUI _currentAmmoUI;
    public TextMeshProUGUI _maxAmmoUI;
    public Image _primaryBtn;
    public Image _secondaryBtn;
    public Slider _hpSlider;

}
