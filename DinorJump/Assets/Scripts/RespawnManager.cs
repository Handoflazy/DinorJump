using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnManager : MonoBehaviour
{
    List<RespawnPoint> respawnPoints = new List<RespawnPoint>();
    [ReadOnlyInspector]
    public RespawnPoint currentRespawnPoints;
    [SerializeField]
    private PlayerID playerID;
    private void Awake()
    {
        foreach (Transform item in transform)
        {
            respawnPoints.Add(item.GetComponent<RespawnPoint>());
            item.GetComponent<RespawnPoint>().OnSpawnPointActivated += UpdateRespawnPoint;
        }
        currentRespawnPoints = respawnPoints[0];
    }

    private void OnEnable()
    {
        playerID.PlayerEvents.OnRespawnRequired += Respawn;
    }
    private void OnDisable()
    {
        playerID.PlayerEvents.OnRespawnRequired -= Respawn;
    }
    public void UpdateRespawnPoint(RespawnPoint newRespawnPoint)
    {
        currentRespawnPoints.DisableRespawnPoint();
        currentRespawnPoints = newRespawnPoint;
    }
    public void Respawn(GameObject objectToRespawn)
    {
        currentRespawnPoints.SetPlayerGO(objectToRespawn);
        currentRespawnPoints.RespawnPlayer();
        if(objectToRespawn == null) 
        return;
    }

    public void RespawnAt(RespawnPoint spawnPoint, GameObject playerGo)
    {
        spawnPoint.SetPlayerGO(playerGo);
        Respawn(playerGo);
    }

    public void ResetAllSpawnPoints()
    {
        foreach (var item in respawnPoints)
        {
            item.ResetRespawnPoint();
        }
        currentRespawnPoints = respawnPoints[0];
    }
}

