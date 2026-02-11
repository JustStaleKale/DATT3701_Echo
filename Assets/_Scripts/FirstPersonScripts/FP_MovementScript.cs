using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class FP_MovementScript : MonoBehaviour
{
    public PlayerInputs playerInput;
    public CharacterController characterController;
    public Animator animator;
    public AudioSource footstepSound;
    public GameEvent pingEvent;
    public Transform shootingPoint;
    public GameObject echoSignalPrefab;
    public PlayerStats playerStats;

    private float echoForce = 100f;
    private float pingCooldown = 1f;
    private float moveSpeed = 2f;
    private float crouchMoveSpeed = 1f;
    private float runSpeed = 4f;

    private bool isWaitingForFootstep = false;
    public float footstepInterval = 0.5f;

    private int isWalkingHash;
    private int isCrouchingHash;
    private int isRunningHash;


    private Vector2 inputVector;
    private Vector3 moveDirection;
    private bool isMoving;
    private bool CrouchPressed = false;
    private bool RunPressed = false;
    private bool canPing = true;

    private float gravity = -9.81f;
    private float verticalVelocity = 0f;

    private bool isReloading = false;

    void Awake()
    {
        playerInput = new PlayerInputs();
        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();

        isWalkingHash = Animator.StringToHash("isWalking");
        isCrouchingHash = Animator.StringToHash("isCrouching");
        isRunningHash = Animator.StringToHash("isRunning");

        playerStats.currentAmmo = playerStats.maxAmmo;
        echoForce = playerStats.echoForce;
        pingCooldown = playerStats.pingCooldown;
        moveSpeed = playerStats.walkSpeed;
        crouchMoveSpeed = playerStats.crouchSpeed;
        runSpeed = playerStats.runSpeed;

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

        // Ping
        playerInput.Player.Ping.performed += HandlePing;
        playerInput.Player.Attack.performed += HandleShootPing;
    }

    private void OnMovementInput(InputAction.CallbackContext ctx)
    {
        inputVector = ctx.ReadValue<Vector2>();
        moveDirection = new Vector3(inputVector.x, 0, inputVector.y);
        isMoving = inputVector.sqrMagnitude > 0.01f;
    }

    private void OnCrouchToggleInput(InputAction.CallbackContext ctx)
    {
        if (ctx.performed) {
            CrouchPressed = !CrouchPressed;
        }
    }

    private void OnCrouchHoldInput(InputAction.CallbackContext ctx)
    {
        CrouchPressed = ctx.ReadValueAsButton();
    }

    private void OnSprintInput(InputAction.CallbackContext ctx)
    {
        RunPressed = ctx.ReadValueAsButton();
    }

    private void HandlePing(InputAction.CallbackContext ctx)
    {
        if (ctx.performed && canPing) {
            StartCoroutine(Ping());
        }
    }

    private void HandleShootPing(InputAction.CallbackContext ctx)
    {
        if (ctx.performed && canPing) {
            ShootPing();
        }
    }

    private void ShootPing()
    {
        if (playerStats.currentAmmo > 0)
        {
            playerStats.currentAmmo--;
            GameObject echoInstance = Instantiate(echoSignalPrefab, shootingPoint.position, Quaternion.identity);
            Rigidbody echoRb = echoInstance.GetComponent<Rigidbody>();
            echoRb.AddForce(shootingPoint.forward * echoForce, ForceMode.Impulse);
            
            if (!isReloading && playerStats.currentAmmo < playerStats.maxAmmo)
            {
                StartCoroutine(ReloadAmmo());
            }
            
        } 
    }

    IEnumerator ReloadAmmo()
    {
        while (playerStats.currentAmmo < playerStats.maxAmmo)
        {
            isReloading = true;
            yield return new WaitForSeconds(playerStats.reloadTime);
            playerStats.currentAmmo++;
        }
        isReloading = false;
    }

    IEnumerator Ping()
    {
        if (canPing)
        {
            canPing = false;
            pingEvent.Raise(this, true);
            yield return new WaitForSeconds(pingCooldown);
            canPing = true;
        }
    }

    void OnEnable()
    {
        playerInput.Player.Enable();
    }

    void OnDisable()
    {
        playerInput.Player.Disable();
    }

    private IEnumerator FootstepSounds()
    {
        while (isMoving && !isWaitingForFootstep && !CrouchPressed)
        {
            isWaitingForFootstep = true;
            footstepSound.Play();
            yield return new WaitForSeconds(footstepInterval);
            isWaitingForFootstep = false;
        }
    }

    private void HandleGravity()
    {
        if (characterController.isGrounded)
        {
            verticalVelocity = 0f;
        }
        else
        {
            verticalVelocity += gravity * Time.deltaTime;
        }

        moveDirection.y = verticalVelocity;
    }

    private void HandleAnimation()
    {
        animator.SetBool(isWalkingHash, isMoving);
        animator.SetBool(isCrouchingHash, CrouchPressed);
        animator.SetBool(isRunningHash, RunPressed && isMoving && !CrouchPressed);
    }

    void Update()
    {
        HandleGravity();
        HandleAnimation();

        // Footsteps
        if (isMoving && !isWaitingForFootstep && !CrouchPressed)
            StartCoroutine(FootstepSounds());
        else if (!isMoving || CrouchPressed)
        {
            isWaitingForFootstep = false;
            StopCoroutine(FootstepSounds());
        }
        Vector3 FP_moveDirection = transform.right * inputVector.x + transform.forward * inputVector.y + moveDirection.y * Vector3.up;

        // Movement speed
        float currentSpeed = moveSpeed;

        if (CrouchPressed)
            currentSpeed = crouchMoveSpeed;
        else if (RunPressed)
            currentSpeed = runSpeed;

        characterController.Move(FP_moveDirection * Time.deltaTime * currentSpeed);
    }
}
