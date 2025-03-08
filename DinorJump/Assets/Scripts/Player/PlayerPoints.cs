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
            Agent.ID.PlayerEvents.OnPointsValueChange?.Invoke(points);
        }
        public void Add(int amount)
        {
            Points+=amount;
            Agent.ID.PlayerEvents.OnPickUpPoints?.Invoke();
            Agent.ID.PlayerEvents.OnPointsValueChange?.Invoke(Points);
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