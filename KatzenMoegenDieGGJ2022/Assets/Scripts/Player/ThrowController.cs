using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowController : MonoBehaviour
{
    public BallController ball;
    PlayerInput inputActions;
    public Transform ballPositionAnker;
    private void Awake()
    {
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
        
        
    }

    private void RestorePosition()
    {
        ball.ResetToHand(ballPositionAnker);
    }
}
