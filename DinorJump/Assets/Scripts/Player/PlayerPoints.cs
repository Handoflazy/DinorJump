using SVS.Level;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace SVS.Player
{
    public class PlayerPoints : AgentSystem, ISaveData
    {
        [ReadOnlyInspector]
        [SerializeField]
        private int points = 0;
        public UnityEvent OnPickUpPoints;
        public int Points { get=>points; private set=>points = value; }
        private void Start()
        {
            agent.ID.playerEvents.OnPointsValueChange?.Invoke(points);
        }
        public void Add(int amount)
        {
            Points+=amount;
            agent.ID.playerEvents.OnPickUpPoints?.Invoke();
            agent.ID.playerEvents.OnPointsValueChange?.Invoke(Points);
            OnPickUpPoints?.Invoke();
        }

        public void SaveData()
        {
            SaveSystem.SavePoints(points);
        }

        public void LoadData()
        {
            points = SaveSystem.LoadPoints();
        }
    }
}