using SVS.Level;
using SVS.PlayerAgent;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using WeaponSystem;

public class Player : AgentSystem, ISaveData
{
    [SerializeField]
    private WeaponManager weaponManager;
    [SerializeField] 
    private AgentWeaponManager agentWeaponManager;
    protected override void Awake()
    {
        weaponManager = FindObjectOfType<WeaponManager>();
        agentWeaponManager = GetComponentInChildren<AgentWeaponManager>();
    }
    public void LoadData()
    {
        List<string> weaponName = SaveSystem.LoadWeapons();

        if (weaponName != null)
        {
            foreach (string name in weaponName)
            {
                // print("Loading weapon: " + weaponName);
                var weapon = weaponManager.GetWeaponWithName(name);
                agentWeaponManager.AddWeaponData(weapon);
            }

        }
        else
        {
            print("No Weapon To load");
            agentWeaponManager.ClearAllWeapon();
        }

    }

    public void SaveData()
    {
        List<string> weaponNames = agentWeaponManager.GetPlayerWeaponNames();
        if(weaponNames != null &&weaponNames.Count>0)
        {
            print("Save" + weaponNames[0].ToString());
            SaveSystem.SaveWeapons(weaponNames);
        }
    }
}