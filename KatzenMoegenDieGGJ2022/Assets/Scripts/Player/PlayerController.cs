using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    Transform playerCamera;
    Rigidbody rb;
    Animator cameraAnimator;
    public float sensetivity = 30;

    public float movementSpeed = 1f;
    public float sprintSpeed = 10f;
    float currentMovementSpeed;
    bool isSprinting;

    public float jumpVelocity = 300f;
    public LayerMask groundLayer;
    public float groundCheckDistance = 1f;

    bool coyoteTimeGroundCheck = true;
    public float coyoteTime = 1f;
    float currentCoyoteTime;


    PlayerInput inputActions;
    float cameraPitch = 0.0f;
    private Vector2 playerInputVector;

    Vector3 cameraStartVector;
    public Vector3 cameraMovementVector;
    public Vector3 toMoveDownWalkVector;
    public Vector3 toMoveUpWalkVector;

    public float cameraWalkSpeed;
    public float cameraSprintSpeed;

    float currentCameraSpeed;
    public float cameraMovRotValue;

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
        cameraAnimator = playerCamera.GetComponent<Animator>();

        rb = GetComponent<Rigidbody>();
        currentCoyoteTime = coyoteTime;
        currentMovementSpeed = movementSpeed;

        cameraStartVector = playerCamera.transform.localPosition;

        toMoveDownWalkVector = playerCamera.transform.localPosition - cameraMovementVector;
        toMoveUpWalkVector = playerCamera.transform.localPosition + cameraMovementVector;
        currentCameraSpeed = cameraWalkSpeed;
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
        Vector2 mouseDelta = inputActions.Keyboard.MouseMovement.ReadValue<Vector2>();
        mouseDelta = mouseDelta.normalized;

        cameraPitch -= mouseDelta.y * sensetivity;
        cameraPitch = Mathf.Clamp(cameraPitch, -90, 90f);

        playerCamera.localEulerAngles = Vector3.right * cameraPitch;
        transform.Rotate(Vector3.up * mouseDelta.x * sensetivity);
    }

    public void Jump()
    {
        if (coyoteTimeGroundCheck)
        {
            rb.AddForce(Vector3.up * jumpVelocity * Time.fixedDeltaTime, ForceMode.Impulse);
        }
    }

    public void OnSprintStart()
    {
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
    bool rotBack;
    private void CamMovRot()
    {
        if (playerInputVector.x > 0)
        {
            rotBack = true;
            if (!rotRight)
            {
                playerCamera.transform.eulerAngles += new Vector3(0, 0, -cameraMovRotValue);
                rotRight = true;
            }
        }
        else if (playerInputVector.x < 0)
        {
            rotBack = true;
            if (!rotLeft)
            {
                playerCamera.transform.eulerAngles += new Vector3(0, 0, cameraMovRotValue);
                rotLeft = true;
            }
        }
        else
        {
            if (rotBack)
            {
                if (rotLeft)
                {
                    playerCamera.transform.eulerAngles += new Vector3(0, 0, -cameraMovRotValue);
                }
                if (rotRight)
                {
                    playerCamera.transform.eulerAngles += new Vector3(0, 0, cameraMovRotValue);
                }

                rotLeft = false;
                rotRight = false;
                rotBack = false;

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
        playerInputVector = new Vector2(inputActions.Keyboard.Horizontal.ReadValue<float>(), inputActions.Keyboard.Vertical.ReadValue<float>());
        playerInputVector = playerInputVector.normalized;

        Vector3 horPlayerMovement = playerCamera.right * playerInputVector.x * currentMovementSpeed * Time.fixedDeltaTime;
        Vector3 vertPlayerMovement = transform.forward * - playerInputVector.y * currentMovementSpeed * Time.fixedDeltaTime;

        rb.MovePosition(transform.position + horPlayerMovement + vertPlayerMovement);

        
    }


    public bool IsGrounded()
    {
        return Physics.Raycast(transform.position, -Vector3.up, groundCheckDistance, groundLayer);

    }



}
