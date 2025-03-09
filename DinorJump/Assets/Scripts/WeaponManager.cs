using System.Collections.Generic;
using UnityEngine;
using WeaponSystem;

namespace SVS.PlayerAgent
{
    public class WeaponManager : MonoBehaviour
    {
        public List<WeaponData> weaponList;
        Dictionary<string,WeaponData> weaponDictionary= new Dictionary<string, WeaponData> ();



        private void Awake()
        {
            AddToDictionary(weaponList);
        }

        void AddToDictionary(List<WeaponData> weaponList)
        {
            foreach (WeaponData weapon in weaponList)
            {
                if (weaponDictionary.ContainsKey(weapon.WeaponName))
                    continue;
                weaponDictionary.Add(weapon.WeaponName, weapon);
            }
        }
        public WeaponData GetWeaponWithName(string name)
        {
            if(weaponDictionary.ContainsKey(name))
                return weaponDictionary[name];
            return null;
        }
    }

   
}