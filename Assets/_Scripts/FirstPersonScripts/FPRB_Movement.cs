using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class FPRB_Movement : MonoBehaviour
{
    // ---------------------------------------------------------- Variables --------------------------------------------------------------------

    [Header("Movement Stats")]
    private float moveSpeed;
    public float walkSpeed = 2f;
    public float runSpeed = 4f;    
    public float groundDrag = 5f;

    [Header("Jumping Stats")]
    public float jumpForce = 5f;
    public float jumpCooldown = 0.5f;
    public float airMultiplier = 0.5f;
    private bool readyToJump = true;

    [Header("Crouching Stats")]
    public float crouchSpeed = 1f;
    private float crouchYScale = 0.5f;
    private float startYScale;

    [Header("Slope Handling")]
    public float maxSlopeAngle = 40f;
    private RaycastHit slopeHit;
    private bool exitingSlope;

    [Header("Ground Check")]
    public float playerHeight = 2f;
    public LayerMask groundLayer;
    private bool isGrounded;

    [Header("Ping Stats")]
    public float pingCooldown = 1f;
    public float pingMaxRange = 5f;
    public float pingDuration = 1f;

    [Header("Shoot Ping Stats")]
    public float echoForce = 100f;
    public float shootPingCooldown = 2f;
    // public float currentAmmo = 3f;
    // public float maxAmmo = 3f;
    // public float reloadTime = 3f;

    [Header("References")]
    public Rigidbody rb;
    //public Transform orientation;
    public PlayerInputs playerInput;
    //public GameEvent pingEvent;
    public Transform shootingPoint;
    public GameObject echoSignalPrefab;
    public GameObject pingLight;
    public Collider ambientPing;
    public GameEvent pingEvent;
    public GameEvent shootPingEvent;
    public GameEvent playSoundEvent;

    public enum MovementState { walking, running, crouching, air }
    public MovementState state;

    private bool isCrouching = false;
    private bool isRunning = false;
    private bool canPing = true;
    private bool canShootPing = true;
    private Vector2 inputVector;
    private Vector3 moveDirection;
    private bool isPaused = false;


    // ---------------------------------------------------------- Input Handling --------------------------------------------------------------------
    private void Awake()
    {
        //Initialize Components
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        playerInput = new PlayerInputs();
        startYScale = transform.localScale.y;

        // Movement
        playerInput.Player.Move.started += OnMovementInput;
        playerInput.Player.Move.performed += OnMovementInput;
        playerInput.Player.Move.canceled += OnMovementInput;

        // Crouch
        playerInput.Player.CrouchToggle.performed += OnCrouchToggleInput;
        playerInput.Player.Crouch.started += OnCrouchHoldInput;
        playerInput.Player.Crouch.canceled += OnCrouchHoldInput;

        // Run
        playerInput.Player.Run.started += OnSprintInput;
        playerInput.Player.Run.canceled += OnSprintInput;

        // Jump
        playerInput.Player.Jump.performed += OnJumpInput;

        // Ping
        playerInput.Player.Ping.performed += HandlePing;
        playerInput.Player.Attack.performed += HandleShootPing;
    }

    void OnEnable()
    {
        playerInput.Player.Enable();
    }

    void OnDisable()
    {
        playerInput.Player.Disable();
    }
    private void OnMovementInput(InputAction.CallbackContext ctx)
    {
        //if (isPaused) return;
        inputVector = ctx.ReadValue<Vector2>();
        //moveDirection = new Vector3(inputVector.x, 0, inputVector.y);
    }

    private void OnCrouchToggleInput(InputAction.CallbackContext ctx)
    {
        if (isPaused) return;
        if (ctx.performed) {
            isCrouching = !isCrouching;
            if (isCrouching)
            {
                CrouchOn();
            }
            else
            {
                CrouchOff();
            }
        }
    }

    private void OnCrouchHoldInput(InputAction.CallbackContext ctx)
    {
        if (isPaused) return;
        isCrouching = ctx.ReadValueAsButton();
        if (isCrouching)
        {
            CrouchOn();
        }
        else
        {
            CrouchOff();
        }
    }

    private void OnSprintInput(InputAction.CallbackContext ctx)
    {
        if (isPaused) return;
        isRunning = ctx.ReadValueAsButton();
    }

    private void OnJumpInput(InputAction.CallbackContext ctx)
    {
        if (isPaused) return;
        if (ctx.performed && readyToJump && isGrounded) {
            readyToJump = false;
            Jump();
            Invoke(nameof(ResetJump), jumpCooldown);
        }
        
    }

    private void HandlePing(InputAction.CallbackContext ctx)
    {
        if (isPaused) return;
        if (ctx.performed && canPing) {
            canPing = false;
            Ping();
        }
    }

    private void HandleShootPing(InputAction.CallbackContext ctx)
    {
        if (isPaused) return;
        if (ctx.performed && canShootPing) {
            canShootPing = false;
            ShootPing();
            Invoke(nameof(ResetShootPing), shootPingCooldown);
        }
    }

    // ---------------------------------------------------------- Updates and Fixed Updates --------------------------------------------------------------------
    void Update()
    {
        isGrounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, groundLayer);
        LimitSpeed();
        StateHandler();
        
        if (isGrounded)
        {
            rb.linearDamping = groundDrag;
        }
        else
        {
            rb.linearDamping = 0f;
        }   
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    // ---------------------------------------------------------- Movement Mechanics --------------------------------------------------------------------
    private void StateHandler()
    {
        if (isGrounded &&isCrouching)
        {
            state = MovementState.crouching;
            moveSpeed = crouchSpeed;
        } else if (isGrounded && isRunning)
        {
            state = MovementState.running;
            moveSpeed = runSpeed;
        } else if (isGrounded)
        {
            state = MovementState.walking;
            moveSpeed = walkSpeed;
        }
        else
        {
            state = MovementState.air;
        }
    }

    private void MovePlayer()
    {
        moveDirection = transform.right * inputVector.x + transform.forward * inputVector.y;

        if (OnSlope() && !exitingSlope)
        {
            rb.AddForce(GetSlopeMoveDirection() * moveSpeed * 20f, ForceMode.Force);
            
            if (rb.linearVelocity.y > 0)
                rb.AddForce(Vector3.down * 40f, ForceMode.Force); 
        } else 
        if (isGrounded)
        {
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);
        }
        else
        {
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f * airMultiplier, ForceMode.Force);
        }

        rb.useGravity = !OnSlope();
        
    }

    private void LimitSpeed()
    {
        if(OnSlope() && !exitingSlope)
        {
            if (rb.linearVelocity.magnitude > moveSpeed)
            {
                rb.linearVelocity = rb.linearVelocity.normalized * moveSpeed;
            }
        } else {
            Vector3 flatVel = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);
            if (flatVel.magnitude > moveSpeed)
            {
                Vector3 limitedVel = flatVel.normalized * moveSpeed;
                rb.linearVelocity = new Vector3(limitedVel.x, rb.linearVelocity.y, limitedVel.z);
            }
        }

        
    }

    private void Jump()
    {
        exitingSlope = true;
        rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }

    private void ResetJump()
    {
        exitingSlope = false;
        readyToJump = true;
    }

    private void CrouchOn()
    {
        transform.localScale = new Vector3(transform.localScale.x, startYScale * crouchYScale, transform.localScale.z);
        isCrouching = true;
        rb.AddForce(Vector3.down * 5f, ForceMode.Impulse);
    }

    private void CrouchOff()
    {
        transform.localScale = new Vector3(transform.localScale.x, startYScale, transform.localScale.z);
        isCrouching = false;
    }

    private bool OnSlope()
    {
        if (Physics.Raycast(transform.position, Vector3.down, out slopeHit, playerHeight * 0.5f + 0.3f))
        {
            float angle = Vector3.Angle(Vector3.up, slopeHit.normal);
            return angle < maxSlopeAngle && angle != 0;
        }
        return false;
    }

    private Vector3 GetSlopeMoveDirection()
    {
        return Vector3.ProjectOnPlane(moveDirection, slopeHit.normal).normalized;
    }

    // ---------------------------------------------------------- Ping Mechanics --------------------------------------------------------------------
    private void ShootPing()
    {
        //ActivateLight();
        GameObject echoInstance = Instantiate(echoSignalPrefab, shootingPoint.position, Quaternion.identity);
        Rigidbody echoRb = echoInstance.GetComponent<Rigidbody>();
        echoRb.AddForce(shootingPoint.forward * echoForce, ForceMode.Impulse);
        shootPingEvent.Raise(this, null);
        playSoundEvent.Raise(this, 2); 
    } 
    private void ResetShootPing()
    {
        canShootPing = true;
    }

    private void Ping()
    {
        //ActivateLight();
        StartCoroutine(OverlapPing());
        pingEvent.Raise(this, null);
        playSoundEvent.Raise(this, 1); 
    }
    private void ResetPing()
    {
        StopAllCoroutines();
        canPing = true;
    }

    IEnumerator OverlapPing()
    {
        GameObject light = Instantiate(pingLight, transform.position, Quaternion.identity);
        Collider[] colliders = Physics.OverlapSphere(transform.position, pingMaxRange, LayerMask.GetMask("Invisible", "Revealed"));
        foreach (Collider c in colliders)
        {
            if (c.gameObject.layer == LayerMask.NameToLayer("Invisible"))
            {
                c.gameObject.layer = LayerMask.NameToLayer("Revealed");
            }
        }
        yield return new WaitForSeconds(pingDuration);
        foreach (Collider c in colliders)
        {
            if (c.gameObject.layer == LayerMask.NameToLayer("Revealed"))
            {
                c.gameObject.layer = LayerMask.NameToLayer("Invisible");
            }
        }
        Destroy(light);
        ResetPing();
    }

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log("Triggered with " + other.gameObject.name);
        if (other.gameObject.layer == LayerMask.NameToLayer("Invisible"))
        {
            other.gameObject.layer = LayerMask.NameToLayer("Revealed");
        }
    }

    private void OnTriggerStay(Collider other) {
        if (other.gameObject.layer == LayerMask.NameToLayer("Invisible"))
        {
            other.gameObject.layer = LayerMask.NameToLayer("Revealed");
        }
    }
    
    private void OnTriggerExit(Collider other)
    {
        //Debug.Log("Exited trigger with " + other.gameObject.name);
        if (other.gameObject.layer == LayerMask.NameToLayer("Revealed"))
        {
            other.gameObject.layer = LayerMask.NameToLayer("Invisible");
        }
    }

    public void SetPause(Component sender, object data)
    {
        isPaused = (bool) data;
    }

    // public void ActivateLight()
    // {
    //     //pingLight.GetComponent<Light>().enabled = true;
    //     GameObject light = Instantiate(pingLight, transform.position, Quaternion.identity);
    //     Invoke(nameof(DeactivateLight()), pingDuration);
    // }

    // public void DeactivateLight(GameObject light)
    // {
    //     //pingLight.GetComponent<Light>().enabled = false;
    //     light.GetComponent<Light>().enabled = false;
    //     Destroy(light);
    // }

}
