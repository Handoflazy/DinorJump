using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SVS.Level
{
    public static class SaveSystem 
    {
        private const string pointsKey = "Points";
        private const string playerWeaponsKey = "PlayerWeapons";
        private const string playerHealthKey = "PlayerHealth";
        private const string levelKey = "LevelKey";
        private const string saveDataKey = "saveDataKey";
        


        public static void SaveGameData(int levelIndexToSave)
        {
            SaveLevel(levelIndexToSave);
            PlayerPrefs.SetInt(saveDataKey, 1);
        }

        private static void SaveLevel(int levelIndexToSave)
        {
            PlayerPrefs.SetInt(levelKey, levelIndexToSave);
        }

        public static int LoadLevelIndex()
        {
            if (IsSaveDataPreset())
            {
                return PlayerPrefs.GetInt(levelKey);
            }
            return -1;
        }

        private static bool IsSaveDataPreset()
        {
            return PlayerPrefs.GetInt(saveDataKey)==1;
        }
        public static void SaveWeapons(List<string> weaponNames)
        {
            
            
            string data = JsonUtility.ToJson(new PlayerWeapons { playerWeapons = weaponNames}) ;
            PlayerPrefs.SetString(playerWeaponsKey, data);
            Debug.Log("Done Save" +  weaponNames.Count);
        }
        public static List<string> LoadWeapons()
        {
            if (IsSaveDataPreset())
            {
                string data = PlayerPrefs.GetString(playerWeaponsKey);
                if (data.Length > 0)
                {
                    return JsonUtility.FromJson<PlayerWeapons>(data).playerWeapons;
                }
            }
            return null;
        }
        public static void ResetSaveData()
        {
            PlayerPrefs.DeleteKey(pointsKey);
            PlayerPrefs.DeleteKey(playerWeaponsKey);
            PlayerPrefs.DeleteKey(levelKey);
            PlayerPrefs.DeleteKey(saveDataKey);
            PlayerPrefs.DeleteKey(playerHealthKey);
        }
        public static void SavePoints(int amount)
        {
            PlayerPrefs.SetInt(pointsKey,amount);
        }
        public static int LoadPoints()
        {
            return PlayerPrefs.GetInt(pointsKey,0); 
        }
        public static void SaveCurrentHealth(int amount)
        {
            PlayerPrefs.SetInt(playerHealthKey, amount);
        }
        public static int LoadHealth()
        {
            return PlayerPrefs.GetInt(playerHealthKey, 4);
        }
        private struct PlayerWeapons
        {
            public List<string> playerWeapons;
        }
    }
}