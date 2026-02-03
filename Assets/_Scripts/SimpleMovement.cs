using UnityEngine;
using UnityEngine.InputSystem;

public class SimpleMovement : MonoBehaviour
{
    PlayerInput playerInput;
    InputAction inputAction;
    new Rigidbody rigidbody;

    [SerializeField] private float moveSpeed = 5f;

    void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        //inputAction = playerInput.actions.FindAction("Move");
        rigidbody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        MovePlayer();
    }

    void MovePlayer()
    {
        Vector2 direction = inputAction.ReadValue<Vector2>();
        rigidbody.linearVelocity = new Vector3(direction.x, 0, direction.y) * moveSpeed;
    }
}