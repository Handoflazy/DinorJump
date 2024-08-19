using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WeaponSystem;

public class GiveAgentWeapon : MonoBehaviour
{
   public List<WeaponData> weapons = new List<WeaponData>();

    private void Start()
    {
        Agent player = GetComponent<Agent>();
        foreach (var item in weapons)
        {
            player.agentWeapon.AddWeaponData(item);
        }
    }
}
