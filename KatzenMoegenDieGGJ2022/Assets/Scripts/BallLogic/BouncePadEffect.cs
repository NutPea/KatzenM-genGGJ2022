using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class BouncePadEffect : MonoBehaviour
{
    VisualEffect vfx;
    private void Start()
    {
        vfx = GetComponentInChildren<VisualEffect>();
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.CompareTag("Ball"))
        {
            vfx.Play();
        }
    }
}
