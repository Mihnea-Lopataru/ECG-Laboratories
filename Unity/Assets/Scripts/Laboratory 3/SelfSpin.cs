using UnityEngine;

[DisallowMultipleComponent]
public class SelfSpin : MonoBehaviour
{
    [SerializeField] private float angularSpeedDegPerSec = 30f;
    [SerializeField] private Vector3 localAxis = Vector3.up;
    [SerializeField] private float initialAngleDeg = 0f;
    [SerializeField] private bool useAbsoluteTime = true;

    private Vector3 axisN;
    private Quaternion baseLocalRotation;
    private float accumulatedAngleDeg;

    private void Awake()
    {
        axisN = localAxis.sqrMagnitude > 1e-8f ? localAxis.normalized : Vector3.up;
    }

    private void Start()
    {
        baseLocalRotation = transform.localRotation;
        accumulatedAngleDeg = initialAngleDeg;
        Apply(initialAngleDeg);
    }

    private void Update()
    {
        float angleDeg = useAbsoluteTime
            ? initialAngleDeg + angularSpeedDegPerSec * Time.time
            : (accumulatedAngleDeg += angularSpeedDegPerSec * Time.deltaTime);

        Apply(angleDeg);
    }

    private void Apply(float angleDeg)
    {
        transform.localRotation = baseLocalRotation * Quaternion.AngleAxis(angleDeg, axisN);
    }

    public void SetAngularSpeed(float degPerSec)
    {
        angularSpeedDegPerSec = degPerSec;
    }
}
