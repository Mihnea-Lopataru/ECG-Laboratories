using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class DroneController : MonoBehaviour
{
    public enum RotationMode
    {
        Quaternion,
        Euler
    }

    [Header("Movement")]
    public float moveSpeed = 5f;
    public float verticalSpeed = 4f;

    [Header("Rotation Mode")]
    public RotationMode rotationMode = RotationMode.Quaternion;

    [Header("Rotation Speeds (deg/sec)")]
    public float yawSpeed = 90f;
    public float pitchSpeed = 75f;
    public float rollSpeed = 90f;

    [Header("Mode Switching")]
    public bool allowKeyboardModeSwitch = true;
    public KeyCode eulerModeKey = KeyCode.Alpha1;
    public KeyCode quaternionModeKey = KeyCode.Alpha2;

    public RotationMode CurrentRotationMode => rotationMode;
    
    private Rigidbody rb;
    private Vector3 moveInput;

    private Quaternion currentRotation;

    private Vector3 eulerState;
    void Awake()
    {
        rb = GetComponent<Rigidbody>();

        rb.useGravity = false;
        rb.constraints = RigidbodyConstraints.FreezeRotation;

        currentRotation = rb.rotation;
        eulerState = rb.rotation.eulerAngles;
    }

    void Update()
    {
        HandleInput();
        HandleModeSwitch();
    }

    void FixedUpdate()
    {
        HandleMovementPhysics();
        HandleRotationPhysics();
    }

    void HandleInput()
    {
        moveInput = Vector3.zero;

        if (Input.GetKey(KeyCode.W)) moveInput += transform.forward;
        if (Input.GetKey(KeyCode.S)) moveInput -= transform.forward;

        if (Input.GetKey(KeyCode.D)) moveInput += transform.right;
        if (Input.GetKey(KeyCode.A)) moveInput -= transform.right;

        float y = 0f;
        if (Input.GetKey(KeyCode.LeftShift)) y += 1f;
        if (Input.GetKey(KeyCode.LeftControl)) y -= 1f;

        Vector3 planar = new Vector3(moveInput.x, 0f, moveInput.z);
        if (planar.sqrMagnitude > 1f) planar = planar.normalized;

        moveInput = planar + Vector3.up * y;
        if (moveInput.sqrMagnitude > 1f) moveInput = moveInput.normalized;
    }

    void HandleModeSwitch()
    {
        if (!allowKeyboardModeSwitch) return;

        if (Input.GetKeyDown(eulerModeKey))
            SetRotationMode(RotationMode.Euler);

        if (Input.GetKeyDown(quaternionModeKey))
            SetRotationMode(RotationMode.Quaternion);
    }

    public void SetRotationMode(RotationMode mode)
    {
        rotationMode = mode;

        if (rotationMode == RotationMode.Euler)
        {
            eulerState = rb.rotation.eulerAngles;
        }
        else
        {
            currentRotation = rb.rotation;
        }

        Debug.Log($"Rotation mode: {rotationMode}");
    }

    void HandleMovementPhysics()
    {
        rb.linearVelocity = moveInput * moveSpeed;
    }

    void HandleRotationPhysics()
    {
        float yawInput = 0f;
        if (Input.GetKey(KeyCode.Q)) yawInput -= 1f;
        if (Input.GetKey(KeyCode.E)) yawInput += 1f;

        float pitchInput = 0f;
        if (Input.GetKey(KeyCode.UpArrow)) pitchInput += 1f;
        if (Input.GetKey(KeyCode.DownArrow)) pitchInput -= 1f;

        float rollInput = 0f;
        if (Input.GetKey(KeyCode.Z)) rollInput += 1f;
        if (Input.GetKey(KeyCode.C)) rollInput -= 1f;

        float yawDelta = yawInput * yawSpeed * Time.fixedDeltaTime;
        float pitchDelta = pitchInput * pitchSpeed * Time.fixedDeltaTime;
        float rollDelta = rollInput * rollSpeed * Time.fixedDeltaTime;

        if (rotationMode == RotationMode.Euler)
        {
            eulerState += new Vector3(pitchDelta, yawDelta, rollDelta);

            Quaternion q = Quaternion.Euler(eulerState);
            rb.MoveRotation(q);
        }
        else
        {
            currentRotation = rb.rotation;

            Vector3 localUp = currentRotation * Vector3.up;
            Vector3 localRight = currentRotation * Vector3.right;
            Vector3 localForward = currentRotation * Vector3.forward;

            Quaternion qYaw = Quaternion.AngleAxis(yawDelta, localUp);
            Quaternion qPitch = Quaternion.AngleAxis(pitchDelta, localRight);
            Quaternion qRoll = Quaternion.AngleAxis(rollDelta, localForward);

            Quaternion next = qYaw * qPitch * qRoll * currentRotation;
            rb.MoveRotation(next);
        }
    }

    public void ToggleRotationMode()
    {
        if (rotationMode == RotationMode.Euler)
            SetRotationMode(RotationMode.Quaternion);
        else
            SetRotationMode(RotationMode.Euler);
    }
}
