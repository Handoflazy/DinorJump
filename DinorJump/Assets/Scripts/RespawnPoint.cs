using System;
using UnityEngine;

public class RespawnPoint : MonoBehaviour
{
    [SerializeField]
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
