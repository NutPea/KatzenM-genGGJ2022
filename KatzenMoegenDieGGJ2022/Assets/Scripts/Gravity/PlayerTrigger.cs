using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class PlayerTrigger : MonoBehaviour
{
    public UnityEvent onEnterTriggerEvent;
    public UnityEvent onExitTriggerEvent;

    public UnityEvent onTriggerColliderChangeEvent;

    bool stayedOnTrigger;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            onEnterTriggerEvent.Invoke();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (!stayedOnTrigger)
            {
                onTriggerColliderChangeEvent.Invoke();
            }
            stayedOnTrigger = true;
        }
        else
        {
            if (stayedOnTrigger)
            {
                onTriggerColliderChangeEvent.Invoke();
            }
            stayedOnTrigger = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            onExitTriggerEvent.Invoke();
        }
    }
}
