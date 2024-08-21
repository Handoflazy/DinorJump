using SVS.Camera;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;

namespace SVS.Level
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField]
        private CameraManager cameraManager;
        [SerializeField]
        private RespawnManager respawnManager;
        [SerializeField]
        private  Agent player;
        private LevelManagement sceneManagement;

    
        private void Awake()
        {
            if(cameraManager == null) 
                cameraManager = FindObjectOfType<CameraManager>();
            if(respawnManager==null)
                respawnManager = FindObjectOfType<RespawnManager>();    
            if(player == null)
            {
                player = FindObjectOfType<AgentInputs>().GetComponent<Agent>();
            }
            sceneManagement = FindObjectOfType<LevelManagement>();
        }
        
        private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            ReloadEntity();
            if(respawnManager)
             respawnManager.Respawn(player.gameObject);
            if(cameraManager)
            cameraManager.SetCameraTarget(player.transform);
            LoadData();
        }
        private void OnEnable()
        {
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        private void OnDisable()
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
            
        }

        private void Start()
        {
            player.gameObject.SetActive(true);
            respawnManager.Respawn(player.gameObject);
            cameraManager.SetCameraTarget(player.transform);
        }

        private void LoadData()
        {
            IEnumerable<ISaveData> saveDataScript = FindObjectsOfType<MonoBehaviour>().OfType<ISaveData>();
            foreach (ISaveData item in saveDataScript)
            {
                item.LoadData();
            }
           // SceneManager.sceneLoaded -= OnSceneLoaded;
        }

        public void ReloadEntity()
        {
            if (cameraManager == null)
                cameraManager = FindObjectOfType<CameraManager>();
            if (respawnManager == null)
                respawnManager = FindObjectOfType<RespawnManager>();
            if (player == null)
            {
                player = FindObjectOfType<AgentInputs>().GetComponent<Agent>();
            }
            sceneManagement = FindObjectOfType<LevelManagement>();
        }
        public void SaveGameData()
        {
            IEnumerable<ISaveData> saveDataScript = FindObjectsOfType<MonoBehaviour>().OfType<ISaveData>();
            foreach (ISaveData item in saveDataScript)
            {
                item.SaveData();
            }
            SaveSystem.SaveGameData(sceneManagement.GetNextLevelIndex());
        }
    }

}
