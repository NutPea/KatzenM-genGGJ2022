using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.VFX;
public class BallController : MonoBehaviour
{   
    public float speed;
    public float maxspeed;
    Rigidbody rb;
    int bounces = 0;
    public int maxBounceCount = 2;
    public AnimationCurve squashCurve;
    public bool isStuck;
    Collider col;
    private bool bounceCooldown;
    private bool isInHand = true;
    public UnityEvent returnEvent;

    public MeshRenderer ballLit1;
    public MeshRenderer ballLit2;
    public MeshRenderer sphereOutside;
    public VisualEffect ballIdleEffect;

    float minValue = 0.1f;
    int clipKey = -1;

    private void Awake()
    {
        
        returnEvent = new UnityEvent();
    }
    private void Start()
    {
        if (clipKey == -1)
            clipKey = Shader.PropertyToID("_Clip");
        rb = GetComponent<Rigidbody>();
        
        rb.useGravity = false;
        col = GetComponent<Collider>();
        
    }

    public void Throw(Vector3 direction)
    {
        if(isInHand)
        {
            rb.velocity = direction * speed;
            rb.isKinematic = false;
            col.enabled = true;
            isStuck = false;
            transform.parent = null;
            isInHand = false;
        }
        
    }

    public void ResetToHand(Transform handPosition)
    {
        if(rb == null)
            rb = GetComponent<Rigidbody>();
        if (col == null)
            col = GetComponent<Collider>();
        rb.isKinematic = true;
        transform.position = handPosition.position;
        transform.parent = handPosition;
        bounces = 0;
        rb.velocity = Vector3.zero;
        rb.useGravity = false;
        isStuck = true;
        col.enabled = false;
        isInHand = true;
        returnEvent.Invoke();
    }

    private void FixedUpdate()
    {
       

        if(maxspeed < rb.velocity.magnitude)
        {
            rb.velocity = maxspeed * rb.velocity.normalized;
        }
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (isStuck || bounceCooldown)
            return;
        bounces++;
        bounceCooldown = true;
        StartCoroutine(BounceCooldown());
        if (bounces < maxBounceCount && collision.gameObject.CompareTag("BouncePad"))
        {
            
            foreach (var item in collision.contacts)
            {
                Debug.DrawRay(item.point, item.normal * 100, Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f), 10f);
                
                transform.rotation = Quaternion.LookRotation(item.normal);
                StartCoroutine(Squash());
                rb.velocity = Vector3.zero;
                StartCoroutine(AccelerateTo(speed, 0.15f, item.normal));
            }
        }
        else
        {
            rb.useGravity = true;
            foreach (var item in collision.contacts)
            {
                Debug.DrawRay(item.point, item.normal * 100, Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f), 10f);
                //rb.velocity = item.normal * speed;
                transform.rotation = Quaternion.LookRotation(item.normal);
                StartCoroutine(Squash());
                //rb.velocity = Vector3.zero;
                //StartCoroutine(AccelerateTo(speed, 0.25f, item.normal));
            }
            
            
             
        }
        
    }

    public void SetDissolve(float alpha)
    {
        ballIdleEffect.SetFloat(clipKey, alpha);
        ballLit1.material.SetFloat(clipKey, Mathf.Lerp(1, -0.1f, alpha));
        ballLit2.material.SetFloat(clipKey, Mathf.Lerp(1, -0.1f, alpha));
        sphereOutside.material.SetFloat(clipKey, alpha);
    }

    

    IEnumerator AccelerateTo(float speed, float time, Vector3 direction)
    {
        while(rb.velocity.magnitude < speed)
        {
            yield return new WaitForEndOfFrame();
            rb.velocity += direction * speed * (Time.deltaTime / time);
            
        }
    }
    
    IEnumerator BounceCooldown()
    {
        yield return new WaitForSeconds(0.1f);
        bounceCooldown = false;
    }

    IEnumerator Squash()
    {
        float time = 0;
        while(time < 1)
        {
            yield return new WaitForEndOfFrame();
            time += Time.deltaTime * 8;
            Vector3 squash = new Vector3(1 / squashCurve.Evaluate(time), 1 / squashCurve.Evaluate(time), 1 * squashCurve.Evaluate(time));
            transform.localScale = squash ;
        }
    }

    
}
