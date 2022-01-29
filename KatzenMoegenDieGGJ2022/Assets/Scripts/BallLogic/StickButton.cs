using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.VFX;

public class StickButton : MonoBehaviour
{
    Rigidbody rb;
    public UnityEvent activateEvent;
    public UnityEvent deactivateEvent;
    private VisualEffect vfx;
    private BallController ball;
    bool isActive;

    private void Start()
    {
        vfx = GetComponentInChildren<VisualEffect>();
        ball = FindObjectOfType<BallController>();
        ball.returnEvent.AddListener(DeactivateButton);

    }

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
        vfx.enabled = true;
        rb.GetComponent<BallController>().isStuck = true;
        rb.GetComponent<Collider>().enabled = false;
        rb.velocity = Vector3.zero;
        rb.isKinematic = true;
        rb.useGravity = false;
        rb.transform.position = transform.position + transform.rotation * Vector3.up * 0.1f;
        isActive = true;
    }

    

    public void DeactivateButton()
    {
        if(isActive)
        {
            isActive = false;
            vfx.enabled = false;
            deactivateEvent.Invoke();
            rb.GetComponent<BallController>().isStuck = false;
            rb.GetComponent<Collider>().enabled = true;
        }       
    }
}
