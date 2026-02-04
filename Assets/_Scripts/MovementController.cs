using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class MovementController : MonoBehaviour
{
    public PlayerInputs playerInput;
    public CharacterController characterController;
    public Animator animator;
    public AudioSource footstepSound;
    public GameEvent pingEvent;

    public float pingCooldown = 1f;

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

    public float moveSpeed = 1f;
    public float crouchMoveSpeed = 0.5f;
    public float runSpeed = 2f;
    public float rotationSpeed = 1f;

    private bool canPing = true;

    private float gravity = -9.81f;
    private float verticalVelocity = 0f;

    void Awake()
    {
        playerInput = new PlayerInputs();
        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();

        isWalkingHash = Animator.StringToHash("isWalking");
        isCrouchingHash = Animator.StringToHash("isCrouching");
        isRunningHash = Animator.StringToHash("isRunning");

        // Movement
        playerInput.Player.Move.started += OnMovementInput;
        playerInput.Player.Move.performed += OnMovementInput;
        playerInput.Player.Move.canceled += OnMovementInput;

        // Crouch
        playerInput.Player.CrouchToggle.performed += OnCrouchToggleInput;
        playerInput.Player.Crouch.started += OnCrouchHoldInput;
        playerInput.Player.Crouch.canceled += OnCrouchHoldInput;

        // Run
        playerInput.Player.Run.started += ctx => RunPressed = true;
        playerInput.Player.Run.canceled += ctx => RunPressed = false;

        // Ping
        playerInput.Player.Ping.performed += HandlePing;
    }

    private void OnMovementInput(InputAction.CallbackContext ctx)
    {
        inputVector = ctx.ReadValue<Vector2>();
        moveDirection = new Vector3(inputVector.x, 0, inputVector.y);
        isMoving = inputVector.sqrMagnitude > 0.01f;
    }

    private void OnCrouchToggleInput(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
            CrouchPressed = !CrouchPressed;
    }

    private void OnCrouchHoldInput(InputAction.CallbackContext ctx)
    {
        CrouchPressed = ctx.ReadValueAsButton();
    }

    private void HandlePing(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
            StartCoroutine(Ping());
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

    private void HandleAnimation()
    {
        animator.SetBool(isWalkingHash, isMoving);
        animator.SetBool(isCrouchingHash, CrouchPressed);
        animator.SetBool(isRunningHash, RunPressed && isMoving && !CrouchPressed);
    }

    private void HandleRotation()
    {
        if (isMoving)
        {
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
    }

    private IEnumerator FootstepSounds()
    {
        while (isMoving && !CrouchPressed)
        {
            footstepSound.Play();
            isWaitingForFootstep = true;
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

    void Update()
    {
        HandleRotation();
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

        // Movement speed
        float currentSpeed = moveSpeed;

        if (CrouchPressed)
            currentSpeed = crouchMoveSpeed;
        else if (RunPressed)
            currentSpeed = runSpeed;

        characterController.Move(moveDirection * Time.deltaTime * currentSpeed);
    }
}

