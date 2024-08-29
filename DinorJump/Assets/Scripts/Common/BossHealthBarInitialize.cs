using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHealthBarInitialize : MonoBehaviour
{
    [SerializeField]
    private Agent Boss;
    public void InitializeHealthBar()
    {
        DinorSingleton.Instance.BossHealthBar.Initialize(Boss.ID.MaxHealth);
        SetHealth(Boss.ID.MaxHealth);
        Boss.GetComponent<Damageable>().OnHealthValueChange.AddListener(SetHealth);
    }
    public void SetHealth(int val)
    {
      
        DinorSingleton.Instance.BossHealthBar.SetValue(val);
    }
    public void OnDestroy()
    {
        DinorSingleton.Instance.BossHealthBar.ToggleBossHealthBar(false);
    }
}
