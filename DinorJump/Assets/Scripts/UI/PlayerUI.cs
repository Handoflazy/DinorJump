using System.Collections;
using System.Collections.Generic;
using UI;
using UnityEngine;

public class PlayerUI : MonoBehaviour
{
    private HealthUI healthUI;
    private PointUI pointUI;
    private WeaponElementUI weaponElementUI;
    [SerializeField]
    private PlayerID playerID;

    public GameObject menuPanel;
    private void Awake()
    {
        healthUI = GetComponentInChildren<HealthUI>();
        pointUI = GetComponentInChildren<PointUI>();
        weaponElementUI = GetComponentInChildren<WeaponElementUI>();
    }
    private void OnEnable()
    {
        playerID.PlayerEvents.OnInitializeMaxHealth += InitializeMaxHealth;
        playerID.PlayerEvents.OnHealthValueChange += SetHealth;
        playerID.PlayerEvents.OnWeaponSwap += UpdateWeaponSprite;
        playerID.PlayerEvents.OnMulipleWeapons +=(bool val)=> weaponElementUI.ToggleWeaponTip(val);
        playerID.PlayerEvents.OnPointsValueChange += SetPoints;
        playerID.PlayerEvents.OnToggleMenu += ToggleInGameMenu;


    }
    private void OnDisable()
    {
        playerID.PlayerEvents.OnInitializeMaxHealth -= InitializeMaxHealth;
        playerID.PlayerEvents.OnHealthValueChange -= SetHealth;
        playerID.PlayerEvents.OnWeaponSwap -= UpdateWeaponSprite;
        playerID.PlayerEvents.OnMulipleWeapons -= (bool val) => weaponElementUI.ToggleWeaponTip(val);
        playerID.PlayerEvents.OnPointsValueChange -= SetPoints;
        playerID.PlayerEvents.OnToggleMenu -= ToggleInGameMenu;
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

    private void ToggleInGameMenu(bool val)
    {
        menuPanel.SetActive(val);
    }

}
