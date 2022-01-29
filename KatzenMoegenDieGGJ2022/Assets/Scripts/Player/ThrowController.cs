using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class ThrowController : MonoBehaviour
{
    public BallController ball;
    PlayerInput inputActions;
    public Transform ballPositionAnker;
    VisualEffect pushEffect;
    private void Awake()
    {
        pushEffect = ballPositionAnker.GetComponentInChildren<VisualEffect>();
        inputActions = new PlayerInput();
        inputActions.Keyboard.Throw.performed += ctx => Throw();

        inputActions.Keyboard.ReturnBall.performed += ctx => RestorePosition();
        ball = FindObjectOfType<BallController>();
        ball.transform.position = ballPositionAnker.position;
        ball.transform.parent = ballPositionAnker;
        ball.isStuck = true;
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
        
        ball.Throw(ballPositionAnker.transform.forward);
        pushEffect.Play();
        
    }

    private void RestorePosition()
    {
        ball.ResetToHand(ballPositionAnker);
    }
}
