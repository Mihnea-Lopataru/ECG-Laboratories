using UnityEngine;

public class FlyCameraController : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 8f;
    public float fastMultiplier = 3f;
    public float verticalSpeed = 6f;

    [Header("Mouse Look")]
    public bool requireRightMouseButton = true;
    public float mouseSensitivity = 2.0f;
    public float pitchMin = -85f;
    public float pitchMax = 85f;

    [Header("Smoothing (optional)")]
    public bool smoothLook = false;
    public float lookSmoothTime = 0.05f;

    float yaw;
    float pitch;

    Vector2 currentLookDelta;
    Vector2 lookDeltaVelocity;

    void Start()
    {
        Vector3 euler = transform.eulerAngles;
        yaw = euler.y;
        pitch = euler.x;
    }

    void Update()
    {
        HandleMouseLook();
        HandleMovement();
    }

    void HandleMouseLook()
    {
        bool looking = !requireRightMouseButton || Input.GetMouseButton(1);
        if (!looking)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            return;
        }

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        float mx = Input.GetAxis("Mouse X") * mouseSensitivity;
        float my = Input.GetAxis("Mouse Y") * mouseSensitivity;

        if (smoothLook)
        {
            Vector2 target = new Vector2(mx, my);
            currentLookDelta = Vector2.SmoothDamp(currentLookDelta, target, ref lookDeltaVelocity, lookSmoothTime);
            mx = currentLookDelta.x;
            my = currentLookDelta.y;
        }

        yaw += mx;
        pitch -= my;
        pitch = Mathf.Clamp(pitch, pitchMin, pitchMax);

        transform.rotation = Quaternion.Euler(pitch, yaw, 0f);
    }

    void HandleMovement()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");

        float upDown = 0f;
        if (Input.GetKey(KeyCode.E) || Input.GetKey(KeyCode.Space)) upDown += 1f;
        if (Input.GetKey(KeyCode.Q) || Input.GetKey(KeyCode.LeftControl)) upDown -= 1f;

        float speed = moveSpeed * (Input.GetKey(KeyCode.LeftShift) ? fastMultiplier : 1f);

        Vector3 move =
            (transform.right * x + transform.forward * z) * speed +
            (transform.up * upDown) * verticalSpeed;

        transform.position += move * Time.deltaTime;
    }
}