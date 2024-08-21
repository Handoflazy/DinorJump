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
        playerID.playerEvents.OnInitializeMaxHealth += InitializeMaxHealth;
        playerID.playerEvents.OnHealthValueChange += SetHealth;
        playerID.playerEvents.OnWeaponSwap += UpdateWeaponSprite;
        playerID.playerEvents.OnMulipleWeapons +=()=> weaponElementUI.ToggleWeaponTip(true);
        playerID.playerEvents.OnPointsValueChange += SetPoints;
        playerID.playerEvents.OnToggleMenu += ToggleInGameMenu;


    }
    private void OnDisable()
    {
        playerID.playerEvents.OnInitializeMaxHealth -= InitializeMaxHealth;
        playerID.playerEvents.OnHealthValueChange -= SetHealth;
        playerID.playerEvents.OnWeaponSwap -= UpdateWeaponSprite;
        playerID.playerEvents.OnMulipleWeapons -= () => weaponElementUI.ToggleWeaponTip(false);
        playerID.playerEvents.OnPointsValueChange -= SetPoints;
        playerID.playerEvents.OnToggleMenu -= ToggleInGameMenu;
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

    private void ToggleInGameMenu()
    {
        menuPanel.SetActive(!menuPanel.activeSelf);
    }

}
