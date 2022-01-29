using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class StepOnButtonController : MonoBehaviour
{
    public UnityEvent onStepOnButtonEvent;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            onStepOnButtonEvent.Invoke();
        }
    }
}
