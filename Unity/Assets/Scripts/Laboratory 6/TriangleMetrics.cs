using UnityEngine;
using TMPro;

public class TriangleMetrics : MonoBehaviour
{
    public Transform vertexA;
    public Transform vertexB;
    public Transform vertexC;

    public Transform centroidMarker;
    public Transform normalArrow;
    public Transform testPointP;

    public TMP_Text triangleMetricsText;

    public LineRenderer edgeAB;
    public LineRenderer edgeBC;
    public LineRenderer edgeCA;

    public float normalLength = 1.0f;

    void Update()
    {
        UpdateTriangleMetrics();
    }

    void UpdateTriangleMetrics()
    {
        Vector3 A = vertexA.position;
        Vector3 B = vertexB.position;
        Vector3 C = vertexC.position;
        Vector3 P = testPointP.position;

        // Centroid
        Vector3 centroid = (A + B + C) / 3f;
        centroidMarker.position = centroid;

        // Edge vectors
        Vector3 AB = B - A;
        Vector3 BC = C - B;
        Vector3 CA = A - C;
        Vector3 AC = C - A;

        // Edge lengths
        float lengthAB = AB.magnitude;
        float lengthBC = BC.magnitude;
        float lengthCA = CA.magnitude;

        // Update edge renderers
        if (edgeAB != null)
        {
            edgeAB.SetPosition(0, A);
            edgeAB.SetPosition(1, B);
        }

        if (edgeBC != null)
        {
            edgeBC.SetPosition(0, B);
            edgeBC.SetPosition(1, C);
        }

        if (edgeCA != null)
        {
            edgeCA.SetPosition(0, C);
            edgeCA.SetPosition(1, A);
        }

        // Cross product
        Vector3 cross = -Vector3.Cross(AB, AC);

        // Area
        float area = 0.5f * cross.magnitude;

        // Normal
        if (cross.magnitude > 0.0001f)
        {
            Vector3 normal = cross.normalized;

            if (normalArrow != null)
            {
                normalArrow.position = centroid + normal * (normalLength * 0.5f);
                normalArrow.rotation = Quaternion.FromToRotation(Vector3.up, normal);
                normalArrow.localScale = new Vector3(0.05f, normalLength * 0.5f, 0.05f);
            }
        }

        // Barycentric coordinates
        float alpha, beta, gamma;
        ComputeBarycentricCoordinates(A, B, C, P, out alpha, out beta, out gamma);

        bool insideTriangle =
            alpha >= 0f && beta >= 0f && gamma >= 0f &&
            alpha <= 1f && beta <= 1f && gamma <= 1f;

        // UI text
        if (triangleMetricsText != null)
        {
            triangleMetricsText.text =
                "Triangle Metrics\n" +
                $"Area: {area:F3}\n" +
                $"|AB|: {lengthAB:F3}\n" +
                $"|BC|: {lengthBC:F3}\n" +
                $"|CA|: {lengthCA:F3}\n" +
                $"Centroid G: ({centroid.x:F2}, {centroid.y:F2}, {centroid.z:F2})\n\n" +
                "Barycentric Coordinates of P\n" +
                $"alpha: {alpha:F3}\n" +
                $"beta:  {beta:F3}\n" +
                $"gamma: {gamma:F3}\n" +
                $"Inside Triangle: {(insideTriangle ? "YES" : "NO")}";
        }
    }

    void ComputeBarycentricCoordinates(Vector3 A, Vector3 B, Vector3 C, Vector3 P,
        out float alpha, out float beta, out float gamma)
    {
        Vector3 v0 = B - A;
        Vector3 v1 = C - A;
        Vector3 v2 = P - A;

        float d00 = Vector3.Dot(v0, v0);
        float d01 = Vector3.Dot(v0, v1);
        float d11 = Vector3.Dot(v1, v1);
        float d20 = Vector3.Dot(v2, v0);
        float d21 = Vector3.Dot(v2, v1);

        float denom = d00 * d11 - d01 * d01;

        if (Mathf.Abs(denom) < 0.0001f)
        {
            alpha = beta = gamma = 0f;
            return;
        }

        beta = (d11 * d20 - d01 * d21) / denom;
        gamma = (d00 * d21 - d01 * d20) / denom;
        alpha = 1.0f - beta - gamma;
    }
}