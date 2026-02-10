using UnityEngine;

[DisallowMultipleComponent]
public class OrbitPivot : MonoBehaviour
{
    [SerializeField] private float angularSpeedDegPerSec = 15f;
    [SerializeField] private Vector3 localAxis = Vector3.up;
    [SerializeField] private float initialAngleDeg = 0f;
    [SerializeField] private bool useAbsoluteTime = true;

    private Vector3 axisN;
    private float accumulatedAngleDeg;

    private void Awake()
    {
        axisN = localAxis.sqrMagnitude > 1e-8f ? localAxis.normalized : Vector3.up;
    }

    private void Start()
    {
        accumulatedAngleDeg = initialAngleDeg;
        ApplyRotationAngle(accumulatedAngleDeg);
    }

    private void Update()
    {
        float thetaDeg = useAbsoluteTime
            ? initialAngleDeg + angularSpeedDegPerSec * Time.time
            : (accumulatedAngleDeg += angularSpeedDegPerSec * Time.deltaTime);

        ApplyRotationAngle(thetaDeg);
    }

    private void ApplyRotationAngle(float thetaDeg)
    {
        transform.localRotation = Quaternion.AngleAxis(thetaDeg, axisN);
    }

    public void SetAngularSpeed(float degPerSec)
    {
        angularSpeedDegPerSec = Mathf.Max(0f, degPerSec);
    }
}
