using UnityEngine;
using UnityEngine.Events;

public class OnTriggerUtil : MonoBehaviour
{
    public UnityEvent OnTriggerEnterEvents, OntriggerExitEvents;

    public LayerMask collisonMask;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((1 << collision.gameObject.layer & collisonMask)!= 0){
            OnTriggerEnterEvents?.Invoke();
        }
    } private void OnTriggerExit2D(Collider2D collision)
    {
        if ((1 << collision.gameObject.layer & collisonMask)!= 0){
            OntriggerExitEvents?.Invoke();
        }
    }
}
