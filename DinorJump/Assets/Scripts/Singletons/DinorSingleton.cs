using UnityEngine;
using UnityEngine.SceneManagement;
using Scene = UnityEngine.SceneManagement.Scene;

public class DinorSingleton : MonoBehaviour
{
    public static DinorSingleton Instance { get; private set; }
    [field: SerializeField]
    public PlayerID PlayerID;

    public PlayerUI PlayerUI { get; private set; }

    public BossHealthBar BossHealthBar { get; private set; }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnLoadedScene;
    }
    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnLoadedScene;
    }
    private void OnLoadedScene(Scene scene, LoadSceneMode mode)
    {
        if(SceneManager.GetActiveScene().buildIndex == 0)
        {
            if(FindObjectOfType<AgentInputs>().gameObject)
                Destroy(FindObjectOfType<AgentInputs>().gameObject);
            Destroy(gameObject);
        }
    }
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        Instance = this;
        PlayerUI = GetComponentInChildren<PlayerUI>();
        BossHealthBar = GetComponentInChildren<BossHealthBar>();
        DontDestroyOnLoad(Instance);



    }
}
