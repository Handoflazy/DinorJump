using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ExitLevelTransition : MonoBehaviour
{
    [SerializeField]
    private string playerTag = "Player";
    [SerializeField]
    private int inputAxisValue = 1;

    private bool playerInRange = false;

    public UnityEvent OnPlayerEnter, OnPlayerExit, OnTransition;

    private bool isUpButtonPress = false;

    private void Start()
    {
        DinorSingleton.Instance.PlayerID.PlayerEvents.OnMoveInput += ReadInput;
    }
    private void OnDisable()
    {
        DinorSingleton.Instance.PlayerID.PlayerEvents.OnMoveInput -= ReadInput;
    }
    private void ReadInput( Vector2 inputVector)
    {
        isUpButtonPress = inputVector.y >= inputAxisValue;
    }
    private void Update()
    {
        if (playerInRange&&isUpButtonPress)
        {
            OnTransition?.Invoke();
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(playerTag))
        {
            playerInRange = true;
            OnPlayerEnter?.Invoke();
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(playerTag))
        {
            playerInRange = false;
            OnPlayerExit?.Invoke();
        }
    }
}
