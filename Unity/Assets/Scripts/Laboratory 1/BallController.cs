using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(PlayerInput))]
public class BallController : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float acceleration = 8f;
    [SerializeField] private float maxSpeed = 6f;

    [Header("Physics")]
    [SerializeField] private float linearDamping = 0.5f;
    [SerializeField] private float angularDamping = 0.05f;

    private Rigidbody rb;
    private Camera mainCamera;

    private InputAction moveAction;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        mainCamera = Camera.main;

        PlayerInput playerInput = GetComponent<PlayerInput>();
        moveAction = playerInput.actions["Move"];
    }

    private void Start()
    {
        rb.linearDamping = linearDamping;
        rb.angularDamping = angularDamping;
    }

    private void FixedUpdate()
    {
        Vector2 input = moveAction.ReadValue<Vector2>();

        if (input == Vector2.zero)
            return;

        Vector3 moveDirection = CalculateMoveDirection(input);
        ApplyMovement(moveDirection);
        LimitHorizontalSpeed();
    }

    private Vector3 CalculateMoveDirection(Vector2 input)
    {
        Vector3 camForward = Vector3.ProjectOnPlane(
            mainCamera.transform.forward, Vector3.up).normalized;

        Vector3 camRight = Vector3.ProjectOnPlane(
            mainCamera.transform.right, Vector3.up).normalized;

        return camForward * input.y + camRight * input.x;
    }

    private void ApplyMovement(Vector3 direction)
    {
        rb.AddForce(direction * acceleration, ForceMode.Acceleration);
    }

    private void LimitHorizontalSpeed()
    {
        Vector3 velocity = rb.linearVelocity;
        Vector3 horizontalVelocity = new Vector3(velocity.x, 0f, velocity.z);

        if (horizontalVelocity.magnitude > maxSpeed)
        {
            Vector3 limitedVelocity = horizontalVelocity.normalized * maxSpeed;
            rb.linearVelocity = new Vector3(
                limitedVelocity.x,
                velocity.y,
                limitedVelocity.z
            );
        }
    }
}
