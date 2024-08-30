
using DesignPatterns.States;
using SVS.Pickable;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using WeaponSystem;

public class Agent : MonoBehaviour
{

    #region COMPONENTS
    public PlayerID ID;
    public Rigidbody2D RB2D { get; private set; }
    [field: SerializeField]
    public MovementDataSO Data { get; private set; }
    public GroundedDetector groundedDetector;
    public ClimbingDetector climbingDetector;
    [HideInInspector]
    public StateMachine playerStateMachine;
    public AgentWeaponManager agentWeapon;


    private static Dictionary<int, GameObject> instances = new Dictionary<int, GameObject>();
    #endregion

    #region STATE PARAMETERS
    //Variables control the various actions the player can perform at any time.
    //These are fields which can are public allowing for other sctipts to read them
    //but can only be privately written to.
    public bool IsDeath { get; private set; }
    public bool IsAttacking { get; set; }
    public bool IsFacingRight { get; set; }
    public bool IsJumping { get; set; }
    public bool IsWallJumping { get; set; }
    public bool IsDashing { get; set; }
    public bool IsSliding { get; set; }

    //Timers (also all fields, could be private and a method returning a bool could be used)
    public float LastOnGroundTime { get; set; }
    public float LastOnWallTime { get; set; }
    public float LastOnWallRightTime { get; set; }
    public float LastOnWallLeftTime { get; set; }

    //Jump
    public bool _isJumpCut;
    public bool _isJumpFalling;


    #endregion

    #region INPUT PARAMETERS
    public Vector2 _moveInput { get; set; }

    public float LastPressedJumpTime { get; set; }
    public float LastPressedDashTime { get; set; }
    #endregion

    #region CHECK PARAMETERS
    //Set all of these up in the inspector
    [Header("Checks")]
    [SerializeField] private Transform _groundCheckPoint;
    //Size of groundCheck depends on the size of your character generally you want them slightly small than width (for ground) and height (for the wall check)
    [SerializeField] private Vector2 _groundCheckSize = new Vector2(0.49f, 0.03f);
    [Space(5)]
    [SerializeField] private Transform _frontWallCheckPoint;
    [SerializeField] private Transform _backWallCheckPoint;
    [SerializeField] private Vector2 _wallCheckSize = new Vector2(0.5f, 1f);
    #endregion

    #region EVENTS
    public UnityEvent OnRespawn;

    #endregion
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
        groundedDetector = GetComponentInChildren<GroundedDetector>();
        climbingDetector = GetComponentInChildren<ClimbingDetector>();
        playerStateMachine = GetComponentInChildren<StateMachine>();
        agentWeapon = GetComponentInChildren<AgentWeaponManager>();
        RB2D = GetComponent<Rigidbody2D>();
        if (gameObject.CompareTag("Player"))
            DontDestroyOnLoad(gameObject);
        Data = Data.Clone();
    }
    private void Start()
    {
       
        IsFacingRight = true;
        IsDeath = false;

    }

    private void OnEnable()
    {
        ID.playerEvents.OnMoveInput += CheckDirectionToFace;
    }

    private void Update()
    {
        #region TIMERS
        LastOnGroundTime -= Time.deltaTime;
        LastOnWallTime -= Time.deltaTime;
        LastOnWallRightTime -= Time.deltaTime;
        LastOnWallLeftTime -= Time.deltaTime;

        LastPressedJumpTime -= Time.deltaTime;
        LastPressedDashTime -= Time.deltaTime;
        #endregion

        #region INPUT HANDLER
        //if (_moveInput.x != 0)
        //    CheckDirectionToFace(_moveInput.x > 0);

        #endregion

        #region UDPATE STATE PARAMETERS


        #endregion
        #region COLLISION CHECKS
        if (groundedDetector && groundedDetector.IsGrounded)
        {
            LastOnGroundTime = Data.coyoteTime;
        }
        if(groundedDetector)
              groundedDetector.CheckGrounded();
        //if (groundedDetector.CheckRightWall())
        //{
        //    LastOnWallRightTime = Data.coyoteTime;
        //}
        //if (groundedDetector.CheckLeftWall())
        //{
        //    LastOnWallLeftTime = Data.coyoteTime;
        //}
        //LastOnWallTime = Mathf.Max(LastOnWallLeftTime, LastOnWallRightTime);

        #endregion
    }

    private void CheckDirectionToFace(Vector2 moveVector)
    {
        if(IsAttacking)
        {
            return;
        }
        if (moveVector.x < 0)
            IsFacingRight = false;
        else if (moveVector.x > 0)
            IsFacingRight = true;
    }


    public void AgentDied()
    {

        if (GetComponent<Damageable>().CurrentHealth > 0 && this.CompareTag("Player"))
        {
            Respawn();
            playerStateMachine.CurrentState.Respawn();
            LastPressedJumpTime = 0;
        }
        else
        {
            playerStateMachine.CurrentState.Die();
            if (this.CompareTag("Player"))
            {
                if (!IsDeath)
                    ID.playerEvents.OnToggleMenu?.Invoke(true);
            }
            IsDeath = true;
            
        }
    }

     public void ResetState()
    {
        if(playerStateMachine.CurrentState == playerStateMachine.GetState(StateType.Die))
        {
            playerStateMachine.TransitionTo(playerStateMachine.GetState(StateType.Idle));
        }
        GetComponent<Collider2D>().enabled = true;
        GetComponent<AgentInputs>().enabled = true;
        IsDeath = false;
    }
    public async void Respawn()
    {
        await Task.Delay(500);  
        ID.playerEvents.OnRespawnRequired?.Invoke(this.gameObject);
        OnRespawn?.Invoke();
    }

    public bool CanJump()
    {
        return LastOnGroundTime > 0;
    }
    public void SetGravityScale(float scale)
    {
        RB2D.gravityScale = scale;
    }

    internal void PickUp(WeaponData weaponData)
    {
        if(weaponData != null)
        {
            agentWeapon.AddWeaponData(weaponData);
        }
    }
}

