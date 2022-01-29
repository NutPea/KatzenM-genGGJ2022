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

    public string Tag = "Player";

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(Tag))
        {
            onEnterTriggerEvent.Invoke();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag(Tag))
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
        if (other.gameObject.CompareTag(Tag))
        {
            onExitTriggerEvent.Invoke();
        }
    }
}
