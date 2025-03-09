using UnityEngine;

[CreateAssetMenu(fileName = "MovementData", menuName = "Agent/AgentID")]
public class PlayerID : ScriptableObject
{
    public string playerName;
    public int MaxHealth=4;
    public PlayerEvents PlayerEvents;
    
    private void OnDisable()
    {
        PlayerEvents.ResetAll();
    }
}
