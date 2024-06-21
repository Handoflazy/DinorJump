using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MovementData", menuName = "Agent/AgentID")]
public class PlayerID : ScriptableObject
{
    public string playerName;
    public PlayerEvents playerEvents;
}
