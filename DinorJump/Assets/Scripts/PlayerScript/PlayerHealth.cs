using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : Damageable
{
    private Agent agent;
    private void Awake()
    {
        agent = transform.root.GetComponent<Agent>();
    }
    // Start is called before the first frame update
    private void OnEnable()
    {
        OnHealthValueChange.AddListener((int health) => agent.ID.playerEvents.OnHealthValueChange?.Invoke(health));
    }
    private void OnDisable()
    {
        OnHealthValueChange.RemoveAllListeners();
    }
    private void Start()
    {
        Inititalize(maxHealth);
    }
    public override void Inititalize(int health)
    {
        agent.ID.playerEvents.OnInitializeMaxHealth?.Invoke(health);
        base.Inititalize(health);
        
    }
    public void OutOfLife()
    {
        agent.AgentDied();
    }
    
}
