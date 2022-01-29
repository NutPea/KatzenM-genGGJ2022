using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.VFX;
public class BallController : MonoBehaviour
{
    public float defaultSpeed;
    public float extraSpeed;
    public float maxspeed;
    public float minPlayerBounceVelocity;
    public float extraPlayerBounceVelocity;
    public float playerInAirMultiplier;
    Rigidbody rb;
    int bounces = 0;
    public int maxBounceCount = 2;
    private Vector3 startScale;
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

    PlayerController playerController;
    bool hasBouncedOnPlayer;

    float minValue = 0.1f;
    int clipKey = -1;
    float throwPercentage;

    private void Awake()
    {

        startScale = transform.localScale;
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

    public void Throw(Vector3 direction,float percentage)
    {
        if (isInHand)
        {
            rb.velocity = direction * (defaultSpeed + extraSpeed * percentage);
            throwPercentage = percentage;
            rb.isKinematic = false;
            StartCoroutine(ColActivateTimer(0.05f));
            isStuck = false;
            transform.parent = null;
            isInHand = false;
            foreach (Transform t in transform)
            {
                foreach (Transform ct in t)
                {
                    ct.gameObject.layer = 8;
                }
                t.gameObject.layer = 8;
            }
        }

    }

    IEnumerator ColActivateTimer(float time)
    {
        yield return new WaitForSeconds(time);
        col.enabled = true;
    }

    public void ResetToHand(Transform handPosition)
    {
        if (rb == null)
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
        hasBouncedOnPlayer = false;
        transform.localScale = startScale;
        foreach (Transform t in transform)
        {
            foreach (Transform ct in t)
            {
                ct.gameObject.layer = 9;
            }
            t.gameObject.layer = 9;
        }
    }

    private void FixedUpdate()
    {
        if (maxspeed < rb.velocity.magnitude)
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

                transform.forward = item.normal;
                StartCoroutine(Squash());
                rb.velocity = Vector3.zero;
                StartCoroutine(AccelerateTo(defaultSpeed, 0.15f, item.normal));
            }
        }
        else
        {
            rb.useGravity = true;
            foreach (var item in collision.contacts)
            {
                Debug.DrawRay(item.point, item.normal * 100, Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f), 10f);
                //rb.velocity = item.normal * speed;
                //transform.rotation = Quaternion.LookRotation(item.normal);
                StartCoroutine(Squash());
                //rb.velocity = Vector3.zero;
                //StartCoroutine(AccelerateTo(speed, 0.25f, item.normal));
            }
        }

        if (collision.gameObject.CompareTag("Player") && bounces >= 1)
        {
            if (playerController == null)
            {
                playerController = collision.gameObject.GetComponent<PlayerController>();
            }
            playerController.BallVelocityAdd(minPlayerBounceVelocity + extraPlayerBounceVelocity*throwPercentage, playerInAirMultiplier, transform);
            rb.velocity = Vector3.zero;
            rb.AddForce(-transform.up * 5);
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
        while (rb.velocity.magnitude < speed)
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
        while (time < 1)
        {
            yield return new WaitForEndOfFrame();
            time += Time.deltaTime * 8;
            Vector3 squash = new Vector3(1 / squashCurve.Evaluate(time), 1 / squashCurve.Evaluate(time), 1 * squashCurve.Evaluate(time));
            transform.localScale = squash;
        }

    }


}
