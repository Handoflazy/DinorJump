using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SVS.Level
{
    public class SaveSystemManager : MonoBehaviour
    {
        public void ResetSaveData()
        {
            SaveSystem.ResetSaveData();
        }
    }
}