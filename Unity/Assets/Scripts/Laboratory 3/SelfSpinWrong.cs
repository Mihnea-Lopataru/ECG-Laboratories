using UnityEngine;

[DisallowMultipleComponent]
public class SelfSpinWrong : MonoBehaviour
{
    [SerializeField] private float angularSpeedDegPerSec = 30f;
    [SerializeField] private Vector3 worldAxis = Vector3.up;
    [SerializeField] private float initialAngleDeg = 0f;
    [SerializeField] private bool useAbsoluteTime = true;

    private Vector3 axisN;
    private Quaternion baseWorldRotation;
    private float accumulatedAngleDeg;

    private void Awake()
    {
        axisN = worldAxis.sqrMagnitude > 1e-8f ? worldAxis.normalized : Vector3.up;
    }

    private void Start()
    {
        baseWorldRotation = transform.rotation;
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
        transform.rotation = baseWorldRotation * Quaternion.AngleAxis(angleDeg, axisN);
    }

    public void SetAngularSpeed(float degPerSec)
    {
        angularSpeedDegPerSec = degPerSec;
    }
}
