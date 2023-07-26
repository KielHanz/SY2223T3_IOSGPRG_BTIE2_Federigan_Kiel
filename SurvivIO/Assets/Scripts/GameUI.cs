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

    public Image _primaryBtnLogo;
    public Image _secondaryBtnLogo;
    
    public Slider _hpSlider;

    public void WeaponSlotColorChange(Color primarySlotColor, Color secondarySlotColor)
    {
       _primaryBtn.GetComponent<Image>().color = primarySlotColor;
       _secondaryBtn.GetComponent<Image>().color = secondarySlotColor;
    }

    public void primaryImageSlot(Sprite primaryImage)
    {
        _primaryBtnLogo.sprite = primaryImage;
    }

    public void secondaryImageSlot(Sprite secondaryImage)
    {
        _secondaryBtnLogo.sprite = secondaryImage;
    }

    public void UpdateAmmoUI()
    {
        _currentAmmoUI.text = "" + GameManager.Instance._player._currentGun.GetComponent<Gun>()._currentAmmo;
        _maxAmmoUI.text = "" + GameManager.Instance._player._currentGun.GetComponent<Gun>()._maxAmmo;
    }
}
