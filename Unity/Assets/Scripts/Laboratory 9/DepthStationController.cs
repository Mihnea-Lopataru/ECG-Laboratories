using UnityEngine;

public class DepthStationController : MonoBehaviour
{
    [Header("Cubes")]
    public Transform cubeLeft;
    public Transform cubeMiddle;
    public Transform cubeRight;

    [Header("Settings")]
    public float moveStep = 0.5f;

    [Header("Camera")]
    public Transform cameraTransform;

    [Header("Shake Settings")]
    public float shakeAmount = 0.05f;
    public float shakeSpeed = 10f;

    private Vector3 leftStart;
    private Vector3 middleStart;
    private Vector3 rightStart;
    private Vector3 cameraStartPos;

    private bool zFightingEnabled = false;

    void Start()
    {
        leftStart = cubeLeft.localPosition;
        middleStart = cubeMiddle.localPosition;
        rightStart = cubeRight.localPosition;

        if (cameraTransform != null)
            cameraStartPos = cameraTransform.position;
    }

    void Update()
    {
        if (zFightingEnabled && cameraTransform != null)
        {
            float offsetX = Mathf.Sin(Time.time * shakeSpeed) * shakeAmount;
            float offsetY = Mathf.Cos(Time.time * shakeSpeed) * shakeAmount;

            cameraTransform.position = cameraStartPos + new Vector3(offsetX, offsetY, 0);
        }
    }

    public void MoveCloser()
    {
        cubeMiddle.localPosition += new Vector3(0, 0, -moveStep);
    }

    public void MoveFarther()
    {
        cubeMiddle.localPosition += new Vector3(0, 0, moveStep);
    }

    public void ResetPositions()
    {
        cubeLeft.localPosition = leftStart;
        cubeMiddle.localPosition = middleStart;
        cubeRight.localPosition = rightStart;

        zFightingEnabled = false;
    }

    public void ToggleZFighting()
    {
        zFightingEnabled = !zFightingEnabled;

        if (zFightingEnabled)
        {
            float z = middleStart.z;

            cubeLeft.localPosition = new Vector3(-1.2f, leftStart.y, z);
            cubeMiddle.localPosition = new Vector3(0f, middleStart.y, z + 0.0001f);
            cubeRight.localPosition = new Vector3(1.2f, rightStart.y, z + 0.0002f);
        }
        else
        {
            ResetPositions();

            if (cameraTransform != null)
                cameraTransform.position = cameraStartPos;
        }
    }
}