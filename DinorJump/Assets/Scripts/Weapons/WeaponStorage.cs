using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace WeaponSystem
{
    public class WeaponStorage 
    {
        private List<WeaponData> WeaponDataList = new List<WeaponData>();
        private int currentWeaponIndex = -1;
        internal int WeaponCount { get=>WeaponDataList.Count; }

        internal bool AddWeaponData(WeaponData weaponData)
        {
           if(WeaponDataList.Contains(weaponData)) return false;
           WeaponDataList.Add(weaponData);
            currentWeaponIndex = WeaponDataList.Count - 1;
            return true;
        }

        internal WeaponData GetCurrentWeapon()
        {
            if (currentWeaponIndex == -1)
            {
                return null;
            }
            return WeaponDataList[currentWeaponIndex];
        }

        internal List<string> GetPlayerWeaponNames()
        {
            return WeaponDataList.Select(Weapon=>Weapon.name).ToList();
        }

        internal WeaponData SwapWeapon()
        {
            if (currentWeaponIndex == -1)
            {
                return null;
            }
            currentWeaponIndex++;
            if(currentWeaponIndex >= WeaponDataList.Count)
            {
                currentWeaponIndex = 0;
            }
            return WeaponDataList[currentWeaponIndex];
        }

        public void ClearStorage()
        {
            WeaponDataList.Clear();
            currentWeaponIndex = -1;
        }

       
    }
}