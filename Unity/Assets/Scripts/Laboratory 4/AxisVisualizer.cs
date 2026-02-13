using UnityEngine;

public class AxisVisualizer : MonoBehaviour
{
    [Header("Axis Settings")]
    [Min(0.1f)]
    public float axisLength = 1.5f;

    [Tooltip("Draw axes at the world origin as well.")]
    public bool drawWorldAxes = true;

    [Tooltip("Draw local axes at the object's pivot.")]
    public bool drawLocalAxes = true;

    [Header("World Axes Origin")]
    public Vector3 worldOrigin = Vector3.zero;

    private void Update()
    {
        if (drawLocalAxes)
            DrawLocalAxes();

        if (drawWorldAxes)
            DrawWorldAxes();
    }

    private void DrawLocalAxes()
    {
        Vector3 p = transform.position;
        Debug.DrawRay(p, transform.right * axisLength, Color.red);
        Debug.DrawRay(p, transform.up * axisLength, Color.green);
        Debug.DrawRay(p, transform.forward * axisLength, Color.blue);
    }

    private void DrawWorldAxes()
    {
        Debug.DrawRay(worldOrigin, Vector3.right * axisLength, Color.red);
        Debug.DrawRay(worldOrigin, Vector3.up * axisLength, Color.green);
        Debug.DrawRay(worldOrigin, Vector3.forward * axisLength, Color.blue);
    }
}
