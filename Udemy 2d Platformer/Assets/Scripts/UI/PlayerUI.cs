using System.Collections;
using System.Collections.Generic;
using UI;
using UnityEngine;

public class PlayerUI : MonoBehaviour
{
    private HealthUI healthUI;
    private PointUI pointUI;
    private void Awake()
    {
        healthUI = GetComponentInChildren<HealthUI>();
        pointUI = GetComponentInChildren<PointUI>();
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
}
