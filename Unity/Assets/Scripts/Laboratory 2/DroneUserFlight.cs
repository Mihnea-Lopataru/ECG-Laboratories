using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class DroneUserFlightRB : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private bool normalizeMovement = true;

    [Header("Debug")]
    [SerializeField] private bool drawDebug = true;
    [SerializeField] private float debugRayLength = 2f;

    private Rigidbody rb;
    private Vector3 moveInput;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();

        rb.useGravity = false;
        rb.isKinematic = false;

        rb.interpolation = RigidbodyInterpolation.Interpolate;
        rb.collisionDetectionMode = CollisionDetectionMode.Continuous;

        rb.constraints = RigidbodyConstraints.FreezeRotation;
    }

    private void Update()
    {
        moveInput = Vector3.zero;

        if (Input.GetKey(KeyCode.W)) moveInput += transform.forward;
        if (Input.GetKey(KeyCode.S)) moveInput -= transform.forward;

        if (Input.GetKey(KeyCode.D)) moveInput += transform.right;
        if (Input.GetKey(KeyCode.A)) moveInput -= transform.right;

        if (Input.GetKey(KeyCode.LeftShift)) moveInput += transform.up;
        if (Input.GetKey(KeyCode.LeftControl)) moveInput -= transform.up;

        if (normalizeMovement && moveInput.sqrMagnitude > 1e-6f)
            moveInput = moveInput.normalized;

        if (drawDebug && moveInput.sqrMagnitude > 1e-6f)
            Debug.DrawRay(transform.position, moveInput * debugRayLength, Color.cyan);
    }

    private void FixedUpdate()
    {
        Vector3 desiredVelocity = moveInput * moveSpeed;
        rb.linearVelocity = desiredVelocity;
    }
}
