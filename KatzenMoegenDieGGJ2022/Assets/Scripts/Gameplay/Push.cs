using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Push : MonoBehaviour
{
    PlayerInput inputActions;
    ThrowController tc;
    bool hasBox = false;
    public Transform kistenAnker;
    Rigidbody Box;
    BoxCollider boxBoxCollider;
    Camera cam;
    
    private void Awake()
    {
        inputActions = new PlayerInput();
        inputActions.Keyboard.Push.performed += ctx => PushInput();
    }

    private void Start()
    {
        tc = FindObjectOfType<ThrowController>();
        cam = Camera.main;
    }

    void PushInput()
    {
        if(hasBox)
        {
            Drop();
            hasBox = false;
        }
        else
        {
            
            PickUp();
        }
    }

    void PickUp()
    {
        RaycastHit rch;
        
        if(Physics.Raycast(cam.transform.position, cam.transform.forward, out rch, 2.0f) && rch.transform.CompareTag("Kiste"))
        {
            hasBox = true;
            tc.boxInHand = true;
            rch.transform.parent = kistenAnker;
            rch.transform.position = kistenAnker.position;
            rch.rigidbody.isKinematic = true;
            Box = rch.rigidbody;
            boxBoxCollider = rch.transform.GetComponent<BoxCollider>();
            boxBoxCollider.enabled = false;
        }
    }

    void Drop()
    {
        Box.isKinematic = false;
        Box.transform.parent = null;
        tc.boxInHand = false;
        boxBoxCollider.enabled = true;
        Box = null;
    }

    private void OnEnable()
    {
        inputActions.Enable();
    }

    private void OnDisable()
    {
        inputActions.Disable();
    }
}
