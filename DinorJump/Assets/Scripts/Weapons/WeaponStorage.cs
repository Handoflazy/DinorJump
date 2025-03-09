using System.Collections.Generic;
using System.Linq;

namespace WeaponSystem
{
    public class WeaponStorage 
    {
        private readonly List<WeaponData> weaponDataList = new();
        private int currentWeaponIndex = -1;
        internal int WeaponCount => weaponDataList.Count;

        public bool AddWeaponData(WeaponData weaponData)
        {
           if(weaponDataList.Contains(weaponData)) return false;
           weaponDataList.Add(weaponData);
            currentWeaponIndex = weaponDataList.Count - 1;
            return true;
        }

        public WeaponData GetCurrentWeapon()
        {
            if (currentWeaponIndex == -1)
            {
                return null;
            }
            return weaponDataList[currentWeaponIndex];
        }

        public List<string> GetPlayerWeaponNames()
        {
            return weaponDataList.Select(weapon=>weapon.name).ToList();
        }

        public WeaponData SwapWeapon()
        {
            if (currentWeaponIndex == -1)
            {
                return null;
            }
            currentWeaponIndex++;
            if(currentWeaponIndex >= weaponDataList.Count)
            {
                currentWeaponIndex = 0;
            }
            return weaponDataList[currentWeaponIndex];
        }

        public void ClearStorage()
        {
            weaponDataList.Clear();
            currentWeaponIndex = -1;
            
        }
        
        public bool IsContainWeapon(WeaponData weaponData) {
            return weaponDataList.Any(w => w.Equals(weaponData));
        }

       
    }
}