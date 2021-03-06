using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class ThrowController : MonoBehaviour
{
    public BallController ball;
    PlayerInput inputActions;
    public Transform ballPositionAnker;
    public Transform mainCam;
    VisualEffect pushEffect;
    public VisualEffect loadEffect;
    public Animator anim;

    public float throwDelay;
    public float catchDelay;
    
    bool hasBallInHand;
    public bool boxInHand;
    bool loadThrow;
    public float loadThrowTimer;
    float currentLoadThrowTimer;

    public Transform innerRotationBall;
    public float innerBallRotationSpeed;

    public float notThrowTimer;
    float currentNotThrowTimer;

    AudioSource audioSource;
    public AudioClip throwClip;
    public AudioClip returnClip;
    public AudioClip chargeUpClip;

    public float secretAnimationPropabiility;
    //Percentage = 1-currentLoadThrowTimer-loadThrowTimer;

    private void Awake()
    {
        pushEffect = ballPositionAnker.GetComponentInChildren<VisualEffect>();
        anim = GetComponentInChildren<Animator>();
        inputActions = new PlayerInput();
        inputActions.Keyboard.Throw.performed += ctx => LoadThrow();
        inputActions.Keyboard.Throw.canceled += ctx => Throw();

        inputActions.Keyboard.ReturnBall.performed += ctx => RestorePosition();
        
        ball.ResetToHand(ballPositionAnker);

        hasBallInHand = true;
        currentLoadThrowTimer = loadThrowTimer;
        currentNotThrowTimer = notThrowTimer;
        audioSource = GetComponent<AudioSource>();
    }


    

    private void OnEnable()
    {
        inputActions.Enable();
    }

    private void OnDisable()
    {
        inputActions.Disable();
    }

    public void LoadThrow()
    {
        loadThrow = true && hasBallInHand;
        if (loadThrow)
        {
            audioSource.Stop();
            audioSource.clip = chargeUpClip;
            audioSource.loop = true;
            audioSource.Play();
        }
            
    }

    public void Throw()
    {
        if (hasBallInHand && !boxInHand)
        {
            audioSource.Stop();
            audioSource.loop = false;
            audioSource.PlayOneShot(throwClip);
            loadThrow = false;
            anim.SetFloat("chargeAmount", 0);
            if(1 - (currentLoadThrowTimer / loadThrowTimer) < 0.1)
            {
                anim.SetTrigger("shotDirect");
            }
            else
            {
                anim.SetTrigger("shot");
            }
            StartCoroutine(ThrowCourotine(throwDelay));
            hasBallInHand = false;
            currentNotThrowTimer = notThrowTimer;
            currentLoadThrowTimer = loadThrowTimer;
        }
        
    }
    private void Update()
    {
        if (loadThrow && currentLoadThrowTimer > 0)
        {
            if(currentLoadThrowTimer < 0)
            {
                currentLoadThrowTimer = 0;
            }
            else
            {
                anim.SetFloat("chargeAmount", 1 - (currentLoadThrowTimer / loadThrowTimer));
                currentLoadThrowTimer -= Time.deltaTime;
            }
        }
        else
        {
             innerRotationBall.transform.Rotate(0, 0, innerBallRotationSpeed  * Time.deltaTime);
        }


        if (hasBallInHand)
        {
            if(currentNotThrowTimer < 0)
            {
                float randomePercentage = Random.Range(0.0f, 1.0f);
                if(randomePercentage > secretAnimationPropabiility)
                {
                    anim.SetTrigger("secret");
                }
                currentNotThrowTimer = notThrowTimer;
            }
            else
            {
                currentNotThrowTimer -= Time.deltaTime;
            }
        }

        float loadStrength;
        if (hasBallInHand)
        {
            loadStrength = 1 - currentLoadThrowTimer / loadThrowTimer;
        }
        else
        {
            loadStrength = 0;
        }
        loadEffect.SetFloat("_loadStrength", loadStrength);

    }

    private void RestorePosition()
    {
        if (!hasBallInHand)
        {
            anim.SetTrigger("catch");
            StartCoroutine(CatchRoutine(catchDelay));
            hasBallInHand = true;
        }
    }

    IEnumerator ThrowCourotine(float time)
    {
       
        yield return new WaitForSeconds(time);
        ball.Throw(mainCam.transform.forward,1-(currentLoadThrowTimer / loadThrowTimer));
        currentLoadThrowTimer = loadThrowTimer;
        pushEffect.Play();
    }

    IEnumerator CatchRoutine(float time)
    {
        audioSource.Stop();
        audioSource.PlayOneShot(returnClip);
        float t = 1;
        while(t > 0)
        {
            t -= Time.deltaTime / time;
            ball.SetDissolve(t);
            yield return new WaitForEndOfFrame();
        }

        ball.ResetToHand(ballPositionAnker);

        while (t < 1)
        {
            t += Time.deltaTime / time;
            ball.SetDissolve(t);
            yield return new WaitForEndOfFrame();
        }
    }
}
