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
    private void Awake()
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
        if(sprite!=null && !gameObject.activeInHierarchy)
        {
            gameObject.SetActive(true);
        }
        if ((weaponImage.sprite == sprite&& sprite))
        {
            weaponImage.enabled = true;
            return;
        }
        if (sprite != null)
        {
            weaponImage.enabled = true;
            weaponImage.sprite = sprite;
            SwapWeaponEvent?.Invoke();
        }
        else
        {
            weaponImage.enabled = false;
            gameObject.SetActive(false);
        }
    }
    public void ToggleWeaponTip(bool val)
    {
        if (!weaponSwapTip)
            return;
        weaponSwapTip.SetActive(val);
        if(val)
            ToggleWeaponTipUI?.Invoke();
    }
}
