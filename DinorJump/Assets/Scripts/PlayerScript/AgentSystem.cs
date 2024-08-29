using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentSystem : MonoBehaviour
{
    protected Agent agent;
    protected virtual void Awake()
    {
        agent = transform.root.GetComponent<Agent>();;
    }
}
