using UnityEngine;

public class DinorSingleton : MonoBehaviour
{
    public static DinorSingleton Instance { get; private set; }
    [field: SerializeField]
    public PlayerID PlayerID;

    public PlayerUI PlayerUI { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        Instance = this;
        PlayerUI = GetComponentInChildren<PlayerUI>();



        DontDestroyOnLoad(Instance);
    }
}
