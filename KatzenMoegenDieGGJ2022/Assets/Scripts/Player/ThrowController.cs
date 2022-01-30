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
        ball = FindObjectOfType<BallController>();
        ball.ResetToHand(ballPositionAnker);

        hasBallInHand = true;
        currentLoadThrowTimer = loadThrowTimer;
        currentNotThrowTimer = notThrowTimer;
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
        loadThrow = true;
    }

    public void Throw()
    {
        if (hasBallInHand && !boxInHand)
        {
            loadThrow = false;
            anim.SetTrigger("shot");
            StartCoroutine(ThrowCourotine(throwDelay));
            hasBallInHand = false;
            currentNotThrowTimer = notThrowTimer;
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
