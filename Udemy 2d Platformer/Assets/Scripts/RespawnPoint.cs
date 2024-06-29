using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class RespawnPoint : MonoBehaviour
{
    [ReadOnlyInspector,SerializeField]
    private GameObject respawnTarget;

    [field: SerializeField]

    public event Action<RespawnPoint> OnSpawnPointActivated;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            respawnTarget = collision.gameObject;
            OnSpawnPointActivated?.Invoke(this);
        }
    }
    public void RespawnPlayer()
    {
        respawnTarget.transform.position = transform.position;
    }


    public void SetPlayerGO(GameObject player)
    {
        respawnTarget = player;
        GetComponent<Collider2D>().enabled = false;
    }
    public void DisableRespawnPoint()
    {
        GetComponent<Collider2D>().enabled = false;
    }
    public void ResetRespawnPoint()
    {
        GetComponent<Collider2D>().enabled = true;
        respawnTarget = null;
    }
}
