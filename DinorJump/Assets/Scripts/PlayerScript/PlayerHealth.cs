using SVS.Level;

public class PlayerHealth : Damageable, ISaveData
{
    private Agent agent;
    private void Awake()
    {
        agent = transform.root.GetComponent<Agent>();
    }
    // Start is called before the first frame update
    private void OnEnable()
    {
        OnHealthValueChange.AddListener((int health) => agent.ID.PlayerEvents.OnHealthValueChange?.Invoke(health));
    }
    private void OnDisable()
    {
        OnHealthValueChange.RemoveAllListeners();
    }
    protected override void Start()
    {
        base.Start();
        Initialize(maxHealth);
    }
    public override void Initialize(int health)
    {
        agent.ID.PlayerEvents.OnInitializeMaxHealth?.Invoke(health);
        base.Initialize(health);
        
    }
    public void OutOfLife()
    {
        agent.AgentDied();
    }
    public void SaveData()
    {
        SaveSystem.SaveCurrentHealth(currentHealth);
    }

    public void LoadData()
    {
        CurrentHealth = SaveSystem.LoadHealth();
       
    }
    
}
