
using DesignPatterns.State;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public PlayerID ID;
    [field:SerializeField]
    public MovementDataSO MovementData { get; private set; }
    public GroundedDetector groundedDetector;
    public StateMachine playerStateMachine;
    private static Dictionary<int, GameObject> instances = new Dictionary<int, GameObject>();

    
    public int instanceID;
    private void Awake()
    {


        if (instances.ContainsKey(instanceID))
        {
            var existing = instances[instanceID];
            if (existing != null)
            {
                if (ReferenceEquals(gameObject, existing))
                {
                    return;
                }
                Destroy(gameObject);
                return;
            }
        }

        instances[instanceID] = gameObject;
        playerStateMachine = new StateMachine(this);
        groundedDetector = GetComponentInChildren<GroundedDetector>();
        DontDestroyOnLoad(gameObject);
    }
    private void Start()
    {
        playerStateMachine.Initialize(playerStateMachine.idleState);

    }

    private void Update()
    {
        // update the current State
        playerStateMachine.Update();
    }
    private void FixedUpdate()
    {
        groundedDetector.CheckGrounded();
        playerStateMachine.FixedUpdate();
    }
}

