using UnityEngine;
using UnityEngine.Events;

public class ContactTrigger : MonoBehaviour
{
    public UnityEvent OnTriggerEnter;
    public UnityEvent OnTriggerStay;
    public UnityEvent OnTriggerExit;

    public LayerMask triggerLayerMask;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((triggerLayerMask & (1 << collision.gameObject.layer)) != 0)
        {
            OnTriggerEnter?.Invoke();
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if ((triggerLayerMask & (1 << collision.gameObject.layer)) != 0)
        {
            OnTriggerStay?.Invoke();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if ((triggerLayerMask & (1 << collision.gameObject.layer)) != 0)
        {
            OnTriggerExit?.Invoke();
        }
    }
}
