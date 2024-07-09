using System.Collections;
using System.Collections.Generic;
using UI;
using UnityEngine;

public class PlayerUI : MonoBehaviour
{
    private HealthUI healthUI;
    private PointUI pointUI;
    private WeaponElementUI weaponElementUI;
    private void Awake()
    {
        healthUI = GetComponentInChildren<HealthUI>();
        pointUI = GetComponentInChildren<PointUI>();
        weaponElementUI = GetComponentInChildren<WeaponElementUI>();
    }
    private void OnEnable()
    {
        DinorSingleton.Instance.PlayerID.playerEvents.OnInitializeMaxHealth += InitializeMaxHealth;
        DinorSingleton.Instance.PlayerID.playerEvents.OnHealthValueChange += SetHealth;
        DinorSingleton.Instance.PlayerID.playerEvents.OnWeaponSwap += UpdateWeaponSprite;
        DinorSingleton.Instance.PlayerID.playerEvents.OnMulipleWeapons +=()=> weaponElementUI.ToggleWeaponTip(true);

    }
    private void OnDisable()
    {
        DinorSingleton.Instance.PlayerID.playerEvents.OnInitializeMaxHealth -= InitializeMaxHealth;
        DinorSingleton.Instance.PlayerID.playerEvents.OnHealthValueChange -= SetHealth;
        DinorSingleton.Instance.PlayerID.playerEvents.OnWeaponSwap -= UpdateWeaponSprite;
        DinorSingleton.Instance.PlayerID.playerEvents.OnMulipleWeapons -= () => weaponElementUI.ToggleWeaponTip(false);
    }
    public void InitializeMaxHealth(int maxHealth)
    {
        healthUI.Initialize(maxHealth);
    }
    public void SetHealth(int currentHealth)
    {
        healthUI.SetHealth(currentHealth);
    }
    public void SetPoints(int currentPoints)
    {
        pointUI.SetPoints(currentPoints);
    }

    public void UpdateWeaponSprite(Sprite sprite)
    {
        weaponElementUI.UpdateWeaponImage(sprite);
    }
}
