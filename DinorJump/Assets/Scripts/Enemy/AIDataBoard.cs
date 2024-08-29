using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;

namespace SVS.AI
{
    public class AIDataBoard : MonoBehaviour
    {
        public List<AIDataTypes> dataTypes;
        Dictionary<AIDataTypes, bool> aiBoard = new Dictionary<AIDataTypes, bool>();

        private void Start()
        {
            HashSet<AIDataTypes> noDuplicates = new HashSet<AIDataTypes>(dataTypes);
            foreach (var item in noDuplicates)
            {
                aiBoard.Add(item,false);
            }
        }
        public bool CheckBoard(AIDataTypes aIDataType)
        {
            if (CheckParameter(aIDataType) == false)
            {
                throw new Exception("No " + aIDataType.ToString() + " in the AI board for " + gameObject.name);
            }
            return aiBoard[aIDataType];
        }
        public void SetBoard(AIDataTypes aIDataType, bool val)
        {
            if (CheckParameter(aIDataType) == false)
            {
                throw new Exception("No " + aIDataType.ToString() + " in the AI board for " + gameObject.name);
            }
            aiBoard[aIDataType] = val;
        }
        bool CheckParameter(AIDataTypes aIDataType)
        {
            return aiBoard.ContainsKey(aIDataType);
        }
    }

    

    public enum AIDataTypes
    {
        Waiting,
        PlayerDetected,
        Arrived,
        InMeleeRange,
        PathBlocked
    }
}