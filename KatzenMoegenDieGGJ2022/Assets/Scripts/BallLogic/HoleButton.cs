using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HoleButton : MonoBehaviour
{
    Rigidbody rb;
    public UnityEvent activateEvent;
    public UnityEvent deactivateEvent;
    private bool isActive;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Ball"))
        {
            rb = other.GetComponent<Rigidbody>();
        }
        
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Ball") && rb.velocity.magnitude <= 0.15f && !isActive)
        {
            ActivateButton();
            isActive = true;
        }
    }

    public void ActivateButton()
    { 
        Debug.Log("Test");
        activateEvent.Invoke();
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Ball"))
        {
            rb = null;
        }
        
    }

    public void DeactivateButton()
    {
        deactivateEvent.Invoke();
    }
}
