using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class WeaponElementUI : MonoBehaviour
{
    [SerializeField]
    private Image weaponImage;
    [SerializeField]
    private GameObject weaponSwapTip;

    public UnityEvent SwapWeaponEvent, ToggleWeaponTipUI;
    private void Start()
    {
        if ((weaponImage == null))
        {
            weaponImage = GetComponentInChildren<Image>();
        }
        weaponImage.enabled = false;
        weaponSwapTip.SetActive(false);
    }
    public void UpdateWeaponImage(Sprite sprite)
    {
        if ((weaponImage.sprite == sprite))
        {
            return;
        }
        if (sprite != null)
        {
            weaponImage.enabled = true;
            weaponImage.sprite = sprite;
            SwapWeaponEvent?.Invoke();
        }
    }
    public void ToggleWeaponTip(bool val)
    {
        weaponSwapTip.SetActive(val);
        if(val)
        ToggleWeaponTipUI?.Invoke();
    }
}
