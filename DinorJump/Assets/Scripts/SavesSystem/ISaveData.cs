using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SVS.Level
{
    public interface ISaveData
    {
        void SaveData();
        void LoadData();
    }
}