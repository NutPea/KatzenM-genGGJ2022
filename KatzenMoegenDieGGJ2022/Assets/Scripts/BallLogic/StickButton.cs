using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class StickButton : MonoBehaviour
{
    Rigidbody rb;
    public UnityEvent activateEvent;
    public UnityEvent deactivateEvent;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Ball"))
        {
            rb = other.GetComponent<Rigidbody>();
            ActivateButton();
            
        }

    }

    public void ActivateButton()
    {
        activateEvent.Invoke();
        rb.GetComponent<BallController>().isStuck = true;
        rb.GetComponent<Collider>().enabled = false;
        rb.velocity = Vector3.zero;
        rb.isKinematic = true;
        rb.useGravity = false;
        rb.transform.position = transform.position + transform.rotation * Vector3.up * 0.1f;
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Ball"))
        {
            DeactivateButton();
        }
    }

    public void DeactivateButton()
    {
        deactivateEvent.Invoke();
        rb.GetComponent<BallController>().isStuck = false;
        rb.GetComponent<Collider>().enabled = true;
        
    }
}
