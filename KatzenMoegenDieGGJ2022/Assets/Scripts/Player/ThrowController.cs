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

    private void Awake()
    {
        pushEffect = ballPositionAnker.GetComponentInChildren<VisualEffect>();
        anim = GetComponentInChildren<Animator>();
        inputActions = new PlayerInput();
        inputActions.Keyboard.Throw.performed += ctx => Throw();

        inputActions.Keyboard.ReturnBall.performed += ctx => RestorePosition();
        ball = FindObjectOfType<BallController>();
        ball.transform.position = ballPositionAnker.position;
        ball.transform.parent = ballPositionAnker;
        ball.isStuck = true;

        hasBallInHand = true;
    }

    

    private void OnEnable()
    {
        inputActions.Enable();
    }

    private void OnDisable()
    {
        inputActions.Disable();
    }

    public void Throw()
    {
        if (hasBallInHand)
        {
            anim.SetTrigger("shot");
            StartCoroutine(ThrowCourotine(throwDelay));
            hasBallInHand = false;
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
        ball.Throw(mainCam.transform.forward);
        pushEffect.Play();
    }

    IEnumerator CatchRoutine(float time)
    {
        float t = time;
        while(t > 0)
        {
            t -= Time.deltaTime / time;
            ball.SetDissolve(t);
            yield return new WaitForEndOfFrame();
        }

        ball.ResetToHand(ballPositionAnker);

        while (t < time)
        {
            t += Time.deltaTime / time;
            ball.SetDissolve(t);
            yield return new WaitForEndOfFrame();
            Debug.Log(t);
        }
    }
}
