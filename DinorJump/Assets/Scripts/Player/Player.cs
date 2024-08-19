using SVS.Level;
using SVS.PlayerAgent;
using System.Collections.Generic;
using UnityEngine;
using WeaponSystem;

public class Player : MonoBehaviour, ISaveData
{
    private WeaponManager weaponManager;
    [SerializeField]
    private AgentWeaponManager agentWeaponManager;
    private void Awake()
    {
        weaponManager = FindObjectOfType<WeaponManager>();
        agentWeaponManager = GetComponentInChildren<AgentWeaponManager>();
    }
    public void LoadData()
    {
        print("hm");
        List<string> weaponName = SaveSystem.LoadWeapons();
        if(weaponName !=null )
        {
            foreach( string name in weaponName )
            {
                print("Loading weapon: " + weaponName);
                var weapon = weaponManager.GetWeaponWithName(name);
                agentWeaponManager.AddWeaponData(weapon); 
            }
            return;
        }

            print("No Weapon To load");

    }

    public void SaveData()
    {
        List<string> weaponNames = agentWeaponManager.GetPlayerWeaponNames();
        if(weaponNames != null &&weaponNames.Count>0)
        {
            print(weaponNames[0].ToString());
            SaveSystem.SaveWeapons(weaponNames);
        }
    }
}