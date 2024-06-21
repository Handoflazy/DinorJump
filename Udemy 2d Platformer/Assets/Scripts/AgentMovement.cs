using Cinemachine.Utility;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentMovement : PlayerSystem
{

    protected Rigidbody2D RB;
    [field: SerializeField]
    MovementDataSO MovementData { get; set; }

    public bool IsFacingRight { get; private set; }

    protected float currentSpeed = 0;
    protected Vector2 oldMovementInput = Vector2.zero;
    protected Vector2 newMovementInput = Vector2.zero;
    protected float speedLimitActor = 1f;

    private bool isGround = true;
    private bool isJumping;
    private bool isClimbing;




    #region BEGIN SCRIPT
    protected override void Awake()
    {
        base.Awake();
        boxCollider2D = transform.root.GetComponent<BoxCollider2D>();
        RB = transform.root.GetComponent<Rigidbody2D>();

    }
    private void OnEnable()
    {
        player.ID.playerEvents.OnMove += HandleMove;
        player.ID.playerEvents.OnJumpPressed += HandleJump;
    }
    private void OnDisable()
    {
        player.ID.playerEvents.OnMove -= HandleMove;
        player.ID.playerEvents.OnJumpPressed -= HandleJump;
    }

    private void Start()
    {
        SetGravityScale(MovementData.gravityScale);
        IsFacingRight = true;
    }
    #endregion
    private void HandleJump()
    {
        if (isGround == false||isClimbing)
        {
            return;
        }
        isJumping = true;
        Jump();
    }

    public void CompleteJump()
    {
        isGround = true;
        isJumping = false;
    }

    public virtual void HandleMove(Vector2 movementInput)
    {
        newMovementInput = movementInput.normalized;
    }
    protected virtual void FixedUpdate()
    {
        MoveAgent(newMovementInput * speedLimitActor);
    }

    #region HANDLE METHOD
    protected virtual void MoveAgent(Vector2 Input)
    {
        if (isClimbing)
        {

        }
        else
        {
            if (isJumping)
            {
                return;
            }
            if (Mathf.Abs(Input.x) > 0 && currentSpeed >= 0)
            {
                oldMovementInput = Input;
                currentSpeed += MovementData.runAcceleration * MovementData.runMaxSpeed * Time.deltaTime;

            }
            else
            {
                currentSpeed -= MovementData.runDeccelAmount * MovementData.runMaxSpeed * Time.deltaTime;

            }

            currentSpeed = Mathf.Clamp(currentSpeed, 0, MovementData.runMaxSpeed);

            RB.velocity = new Vector2(oldMovementInput.x * currentSpeed, RB.velocity.y);

        }
    }


    protected void Jump()
    {
        float force = MovementData.jumpForce;
        if (RB.velocity.y < 0)
            force -= RB.velocity.y;

        RB.AddForce(Vector2.up * force, ForceMode2D.Impulse);
    }
    #endregion


    #region GENERAL METHODS
    public void SetGravityScale(float scale)
    {
        RB.gravityScale = scale;
    }
    #endregion
}
