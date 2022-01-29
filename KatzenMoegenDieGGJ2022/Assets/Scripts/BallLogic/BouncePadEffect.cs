using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;
using UnityEngine.Events;
public class BouncePadEffect : MonoBehaviour
{
    VisualEffect vfx;
    public UnityEvent events;
    private void Start()
    {
        vfx = GetComponentInChildren<VisualEffect>();
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.CompareTag("Ball"))
        {
            vfx.Play();
            events.Invoke();
        }
    }
}
