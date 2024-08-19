using Vector2 = UnityEngine.Vector2;
using UnityEngine.InputSystem;
using UnityEngine.Events;
using UnityEngine;

public class AgentInputs : AgentSystem, PlayerControls.IMainActions
{
    PlayerControls inputActions;

    [field:SerializeField]
    public Vector2 MovementVector { get;private set; }


    protected override void Awake()
    {
        base.Awake();
        inputActions = new PlayerControls();
    }
    private void Start()
    {
        inputActions.Main.SetCallbacks(this);
        inputActions.Main.Enable();
    }

    private void Update()
    {
        
        agent.ID.playerEvents.OnMoveInput?.Invoke(MovementVector);
    }
    public void OnMovement(InputAction.CallbackContext context)
    {
        this.MovementVector = (context.ReadValue<Vector2>());
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            agent.ID.playerEvents.OnJumpPressed?.Invoke();
         
        }
        else if(context.phase == InputActionPhase.Canceled)
        {
            agent.ID.playerEvents.OnJumpReleased?.Invoke();
        }
      
    }

    public void OnAttack(InputAction.CallbackContext context)
    {
         if(context.phase == InputActionPhase.Performed)
        {
            agent.ID.playerEvents.OnAttackPressed?.Invoke();
        }
    }

    public void OnSwapWeapon(InputAction.CallbackContext context)
    {
        if(context.phase == InputActionPhase.Performed)
        {
            agent.ID.playerEvents.OnWeaponChange?.Invoke();
        }
    }
}
