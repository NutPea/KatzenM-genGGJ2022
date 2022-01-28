using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    public Transform handPosition;
    public float speed;
    public float maxspeed;
    Rigidbody rb;
    int bounces = 0;
    public int maxBounceCount = 2;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.velocity = Vector3.left;
        rb.useGravity = false;
        
    }

    void ResetToHand()
    {
        transform.position = handPosition.position;
        bounces = 0;
        rb.velocity = Vector3.left;
        rb.useGravity = false;
    }

    private void FixedUpdate()
    {
        float currentSpeed = rb.velocity.magnitude;
        if(currentSpeed <= 0)
        {
            StartCoroutine(StartReset());
        }

        if(maxspeed < rb.velocity.magnitude)
        {
            rb.velocity = maxspeed * rb.velocity.normalized;
        }
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        bounces++;
        if (bounces < maxBounceCount)
        {
            foreach (var item in collision.contacts)
            {
                Debug.DrawRay(item.point, item.normal * 100, Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f), 10f);
                rb.velocity = item.normal * speed;
                
            }
        }
        else
        {
            rb.useGravity = true;
            if (bounces > 8)
            {
                StartCoroutine(StartReset());

            }
             
        }
        
    }

    IEnumerator StartReset()
    {
        yield return new WaitForSeconds(1);
        ResetToHand();
    }
}
