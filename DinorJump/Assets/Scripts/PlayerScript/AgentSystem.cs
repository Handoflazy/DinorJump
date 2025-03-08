using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentSystem : MonoBehaviour
{
    protected Agent Agent;
    protected virtual void Awake()
    {
        Agent = transform.root.GetComponent<Agent>();;
    }
}
