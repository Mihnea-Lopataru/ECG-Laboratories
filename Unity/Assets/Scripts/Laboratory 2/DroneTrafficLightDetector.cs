using UnityEngine;

public class DroneTrafficLightDetector : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform drone;

    [Tooltip("Optional. If left empty, this GameObject's transform is used as the sensor origin/forward.")]
    [SerializeField] private Transform sensorOrigin;

    [Header("Traffic Light Objects (toggle ON/OFF)")]
    [SerializeField] private GameObject redLight;
    [SerializeField] private GameObject yellowLight;
    [SerializeField] private GameObject greenLight;

    [Header("Detection (Chapter 2 math)")]
    [Tooltip("Max distance for any 'attention' state. Beyond this: RED.")]
    [SerializeField] private float farDistance = 12f;

    [Tooltip("Within this distance (and in front), show YELLOW.")]
    [SerializeField] private float nearDistance = 6f;

    [Tooltip("Within this distance AND inside FOV, show GREEN.")]
    [SerializeField] private float detectDistance = 6f;

    [Tooltip("Field of view angle (degrees). GREEN requires target to be inside this cone.")]
    [Range(1f, 179f)]
    [SerializeField] private float fovDegrees = 60f;

    [Header("Debug")]
    [SerializeField] private bool drawDebug = true;

    private void Awake()
    {
        if (sensorOrigin == null) sensorOrigin = transform;
        SetStateRed();
    }

    private void Update()
    {
        if (drone == null || redLight == null || yellowLight == null || greenLight == null) return;

        Vector3 originPos = sensorOrigin.position;
        Vector3 dronePos = drone.position;

        Vector3 toDrone = dronePos - originPos;
        float distance = toDrone.magnitude;

        if (distance > farDistance)
        {
            SetStateRed();
            DrawDebug(originPos, dronePos, toDrone, Color.red);
            return;
        }

        if (toDrone.sqrMagnitude < 1e-6f)
        {
            SetStateRed();
            return;
        }
        Vector3 dirToDrone = toDrone.normalized;

        float dot = Vector3.Dot(sensorOrigin.forward, dirToDrone);

        float threshold = Mathf.Cos(0.5f * fovDegrees * Mathf.Deg2Rad);
        bool inFrontAndInFov = dot >= threshold;

        if (distance <= detectDistance && inFrontAndInFov)
        {
            SetStateGreen();
            DrawDebug(originPos, dronePos, toDrone, Color.green);
            return;
        }

        if (distance <= nearDistance || (distance <= farDistance && dot > 0f))
        {
            SetStateYellow();
            DrawDebug(originPos, dronePos, toDrone, Color.yellow);
            return;
        }

        SetStateRed();
        DrawDebug(originPos, dronePos, toDrone, Color.red);
    }

    private void SetStateRed()
    {
        redLight.SetActive(true);
        yellowLight.SetActive(false);
        greenLight.SetActive(false);
    }

    private void SetStateYellow()
    {
        redLight.SetActive(false);
        yellowLight.SetActive(true);
        greenLight.SetActive(false);
    }

    private void SetStateGreen()
    {
        redLight.SetActive(false);
        yellowLight.SetActive(false);
        greenLight.SetActive(true);
    }

    private void DrawDebug(Vector3 originPos, Vector3 dronePos, Vector3 toDrone, Color c)
    {
        if (!drawDebug) return;

        Debug.DrawLine(originPos, dronePos, c);
        Debug.DrawRay(originPos, sensorOrigin.forward * 3f, Color.cyan);

        Vector3 fwd = sensorOrigin.forward; fwd.y = 0f;
        if (fwd.sqrMagnitude < 1e-6f) return;
        fwd.Normalize();

        float half = 0.5f * fovDegrees;
        Vector3 left = Quaternion.AngleAxis(-half, Vector3.up) * fwd;
        Vector3 right = Quaternion.AngleAxis(half, Vector3.up) * fwd;

        Debug.DrawRay(originPos, left * 3f, Color.white);
        Debug.DrawRay(originPos, right * 3f, Color.white);
    }
}
