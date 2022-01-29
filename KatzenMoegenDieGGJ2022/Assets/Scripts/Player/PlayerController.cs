using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class PlayerController : MonoBehaviour
{

    Transform playerCamera;
    Rigidbody rb;
    PlayerInput inputActions;
    private Vector2 playerInputVector;
    [HideInInspector]public bool upsideDown;

    [Header("//------------------PlayerMovement---------------------//")]
    public float movementSpeed = 1f;
    public float sprintSpeed = 10f;
    bool isSprinting;
    public LayerMask groundLayer;
    public float groundCheckDistance = 1f;
    float currentMovementSpeed;


    [HideInInspector] public bool stopMovementUntilHitGroundAgain;
    [Header("//------------------Camera---------------------//")]
    public float sensetivity = 30;
    float cameraPitch = 0.0f;


    [Header("//------------------CameraMovement---------------------//")]
    public Vector3 cameraMovementVector;
    public float cameraWalkSpeed;
    public float cameraSprintSpeed;
    [Space(10)]
    public float cameraSideMovRotValue = 1;
    public float cameraFrontMovRotValue = 1;

    Vector3 toMoveDownWalkVector;
    Vector3 toMoveUpWalkVector;
    Vector3 cameraStartVector;
    float currentCameraSpeed;

    [Header("//----------------------Jump------------------------//")]
    public float jumpVelocity = 10f;
    public float extraGravity = -3f;
    public float jumpTime = 1f;
    public AnimationCurve jumpFallOf;
    public float coyoteTime = 1f;
    public float jumpBufferTimer = 1f;

    public float currentJumpBufferTimer;
    float currentCoyoteTime;
    [HideInInspector] public bool forceAired;
    bool jumpButtonPressed;
    bool isJumping;
    float velocityY;
    bool isGroundedTrigger;
    float currentJumpTime;
    bool coyoteTimeGroundCheck = true;

    [Header("//------------------CutScene---------------//")]
    public float moveTime;
    bool blockPlayerInput;
    float currentMoveTime;
    public UnityEvent onCutSceneStart;
    public UnityEvent onCutSceneEnd;


    private void Awake()
    {
        inputActions = new PlayerInput();
        inputActions.Keyboard.MouseMovement.performed += ctx => MouseMovement();
        inputActions.Keyboard.Jump.performed += ctx => Jump();

        inputActions.Keyboard.Shift.performed += ctx => OnSprintStart();
        inputActions.Keyboard.Shift.canceled += ctx => OnSprintStop();
    }

    void Start()
    {
        playerCamera = Camera.main.GetComponent<Transform>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        rb = GetComponent<Rigidbody>();
        currentCoyoteTime = coyoteTime;
        currentMovementSpeed = movementSpeed;

        cameraStartVector = playerCamera.transform.localPosition;

        toMoveDownWalkVector = playerCamera.transform.localPosition - cameraMovementVector;
        toMoveUpWalkVector = playerCamera.transform.localPosition + cameraMovementVector;
        currentCameraSpeed = cameraWalkSpeed;
        currentJumpTime = jumpTime;
        currentJumpBufferTimer = jumpBufferTimer;

        StartCoroutine(StartCutScene(moveTime));
    }

    private void OnEnable()
    {
        inputActions.Enable();
    }

    private void OnDisable()
    {
        inputActions.Disable();
    }

    public void MouseMovement()
    {
        if (blockPlayerInput) return;

        Vector2 mouseDelta = inputActions.Keyboard.MouseMovement.ReadValue<Vector2>();
        mouseDelta = mouseDelta.normalized;

        cameraPitch -= mouseDelta.y * sensetivity;
        cameraPitch = Mathf.Clamp(cameraPitch, -90, 90f);

        playerCamera.localEulerAngles = Vector3.right * cameraPitch;

        transform.Rotate(Vector3.up * mouseDelta.x * sensetivity);
    }

    public void Jump()
    {
        if (blockPlayerInput) return;

        if (coyoteTimeGroundCheck)
        {
            isJumping = true;
        }
        else
        {
            jumpButtonPressed = true;
        }
    }

    public void OnSprintStart()
    {
        if (blockPlayerInput) return;

        currentMovementSpeed = sprintSpeed;
        isSprinting = true;
    }

    public void OnSprintStop()
    {
        currentMovementSpeed = movementSpeed;
        isSprinting = false;
    }


    bool lerpTo;
    bool lerpBack;
    bool lerpTrigger;

    private void Update()
    {
        #region ChangeGravityGate
        if (stopMovementUntilHitGroundAgain)
        {
            if (IsGrounded())
            {
                stopMovementUntilHitGroundAgain = false;
            }
            else
            {
                return;
            }
        }
        #endregion

        if (!IsGrounded())
        {
            if (currentCoyoteTime < 0)
            {
                currentCoyoteTime = coyoteTime;
                coyoteTimeGroundCheck = false;
            }
            else
            {
                currentCoyoteTime -= Time.deltaTime;
            }
        }
        else
        {
            currentCoyoteTime = coyoteTime;
            coyoteTimeGroundCheck = true;
        }

        CameraMovement();
        CamMovRot();

    }

    bool rotLeft;
    bool rotRight;
    bool rotSideBack;

    bool rotFront;
    bool rotBack;
    bool rotVerticalBack;
    private void CamMovRot()
    {
        if (playerInputVector.x > 0)
        {
            rotSideBack = true;
            if (!rotRight)
            {
                playerCamera.transform.eulerAngles += new Vector3(0, 0, -cameraSideMovRotValue);
                rotRight = true;
            }
        }
        else if (playerInputVector.x < 0)
        {
            rotSideBack = true;
            if (!rotLeft)
            {
                playerCamera.transform.eulerAngles += new Vector3(0, 0, cameraSideMovRotValue);
                rotLeft = true;
            }
        }
        else
        {
            if (rotSideBack)
            {
                if (rotLeft)
                {
                    playerCamera.transform.eulerAngles += new Vector3(0, 0, -cameraSideMovRotValue);
                }
                if (rotRight)
                {
                    playerCamera.transform.eulerAngles += new Vector3(0, 0, cameraSideMovRotValue);
                }

                rotLeft = false;
                rotRight = false;
                rotSideBack = false;

            }
        }

        if (playerInputVector.y > 0)
        {
            rotVerticalBack = true;
            if (!rotFront)
            {
                playerCamera.transform.eulerAngles += new Vector3(cameraFrontMovRotValue, 0,0);
                rotFront = true;
            }
        }
        else if (playerInputVector.y < 0)
        {
            rotVerticalBack = true;
            if (!rotBack)
            {
                playerCamera.transform.eulerAngles += new Vector3(cameraFrontMovRotValue, 0, 0);
                rotBack = true;
            }
        }
        else
        {
            if (rotVerticalBack)
            {
                if (rotFront)
                {
                    playerCamera.transform.eulerAngles += new Vector3(cameraFrontMovRotValue, 0, 0);
                }
                if (rotBack)
                {
                    playerCamera.transform.eulerAngles += new Vector3(-cameraFrontMovRotValue, 0, 0);
                }

                rotFront = false;
                rotBack = false;
                rotVerticalBack = false;

            }
        }






    }
    private void CameraMovement()
    {
        if (playerInputVector != Vector2.zero && IsGrounded())
        {
            lerpTrigger = true;
            if (!isSprinting)
            {
                currentCameraSpeed = cameraWalkSpeed;
            }
            else
            {
                currentCameraSpeed = cameraSprintSpeed;
            }

            if (lerpTo)
            {
                playerCamera.transform.localPosition -= cameraMovementVector * currentCameraSpeed * Time.deltaTime;
                if (Vector2.Distance(playerCamera.transform.localPosition, toMoveDownWalkVector) < 0.01f)
                {
                    lerpBack = true;
                    lerpTo = false;
                }
            }

            if (lerpBack)
            {
                playerCamera.transform.localPosition += cameraMovementVector * currentCameraSpeed * Time.deltaTime;
                if (Vector2.Distance(playerCamera.transform.localPosition, toMoveUpWalkVector) < 0.01f)
                {
                    lerpTo = true;
                    lerpBack = false;
                }
            }
        }
        else if (playerInputVector != Vector2.zero && isSprinting)
        {
            lerpTrigger = true;

        }
        else
        {
            if (lerpTrigger)
            {
                playerCamera.transform.localPosition = cameraStartVector;
                lerpTrigger = false;
            }
            lerpTo = true;
            lerpBack = false;
        }
    }

    private void FixedUpdate()
    {
        #region ChangeGravityGate
        float velocityValue = 1;
        if (stopMovementUntilHitGroundAgain)
        {
            if (IsGrounded())
            {
                stopMovementUntilHitGroundAgain = false;
            }
            else
            {
                velocityValue = 0;
            }
        }
        #endregion

        if (IsGrounded())
        {
            if (!isGroundedTrigger)
            {
                velocityY = 0.0f;
                rb.velocity = new Vector3(rb.velocity.x,0,rb.velocity.z);
                isGroundedTrigger = true;
            }
        }
        else
        {
            isGroundedTrigger = false;
            if (upsideDown)
            {
                velocityY += (Physics.gravity.y - extraGravity) * Time.deltaTime * Time.deltaTime;
            }
            else
            {
                velocityY += (Physics.gravity.y + extraGravity) * Time.deltaTime *Time.deltaTime;
            }
        }


        if (jumpButtonPressed)
        {
            if(currentJumpBufferTimer < 0)
            {
                currentJumpBufferTimer = jumpBufferTimer;
                jumpButtonPressed = false;
            }
            else
            {
                currentJumpBufferTimer -= Time.deltaTime;
            }


            if (IsGrounded())
            {
                isJumping = true;
                currentJumpBufferTimer = jumpBufferTimer;
                currentJumpTime = jumpTime;
                jumpButtonPressed = false;
            }
            
        }


        Vector3 jumpVector = Vector3.zero;

        if (isJumping)
        {
            if(currentJumpTime < 0)
            {
                currentJumpTime = jumpTime;
                isJumping = false;
            }
            else
            {
                float percentage = currentJumpTime / jumpTime;
                float jumpForce = jumpFallOf.Evaluate(percentage);
                jumpVector = transform.up * jumpForce * jumpVelocity * Time.deltaTime;
                currentJumpTime -= Time.deltaTime;
            }
        }

        if (!blockPlayerInput)
        {
            playerInputVector = new Vector2(inputActions.Keyboard.Horizontal.ReadValue<float>(), inputActions.Keyboard.Vertical.ReadValue<float>());
            playerInputVector = playerInputVector.normalized;
        }

        Vector3 horPlayerMovement = playerCamera.right * playerInputVector.x * currentMovementSpeed * Time.fixedDeltaTime * velocityValue;
        Vector3 vertPlayerMovement = transform.forward * - playerInputVector.y * currentMovementSpeed * Time.fixedDeltaTime * velocityValue;
        
        rb.MovePosition(transform.position + horPlayerMovement + vertPlayerMovement + jumpVector + Vector3.up * velocityY);
    }

    public bool IsGrounded()
    {
        return Physics.Raycast(transform.position, -transform.up, groundCheckDistance, groundLayer) && !forceAired;
    }

    public IEnumerator StartCutScene(float time)
    {
        onCutSceneStart.Invoke();
        blockPlayerInput = true;
        playerInputVector = new Vector2(0, -1);
        yield return new WaitForSeconds(time);
        blockPlayerInput = false;
        onCutSceneEnd.Invoke();
    }



}
