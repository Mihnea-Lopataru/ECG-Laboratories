using UnityEngine;

[DisallowMultipleComponent]
public class OrbitPivotWrong : MonoBehaviour
{
    [SerializeField] private Transform orbitingBody;
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
    }

    private void Update()
    {
        if (orbitingBody == null) return;

        float thetaDeg = useAbsoluteTime
            ? initialAngleDeg + angularSpeedDegPerSec * Time.time
            : (accumulatedAngleDeg += angularSpeedDegPerSec * Time.deltaTime);

        orbitingBody.localRotation = Quaternion.AngleAxis(thetaDeg, axisN);
    }
}
