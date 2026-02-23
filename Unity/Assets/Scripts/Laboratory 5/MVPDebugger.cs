using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Text;

public class MVPDebugger : MonoBehaviour
{
    [Header("References")]
    public Camera targetCamera;
    public Transform trackedObject;

    [Header("Clipping Feedback")]
    public Light trackedLight;
    public Color visibleColor = Color.green;
    public Color clippedColor = Color.red;
    public float visibleIntensity = 50f;
    public float clippedIntensity = 50f;

    [Header("UI Text")]
    public TMP_Text debugText;
    public TMP_Text matrixText;

    [Header("UI Controls")]
    public Button toggleProjectionButton;

    [Header("Projection Settings")]
    [Range(5f, 120f)] public float perspectiveFov = 60f;
    public float orthographicSize = 6f;

    void Start()
    {
        if (toggleProjectionButton != null)
            toggleProjectionButton.onClick.AddListener(ToggleProjection);

        ApplyProjectionDefaults();
        UpdateButtonLabel();
    }

    void Update()
    {
        if (!targetCamera || !trackedObject || !debugText || !matrixText)
            return;

        if (!targetCamera.orthographic)
            targetCamera.fieldOfView = perspectiveFov;
        else
            targetCamera.orthographicSize = orthographicSize;

        Matrix4x4 M = trackedObject.localToWorldMatrix;
        Matrix4x4 V = targetCamera.worldToCameraMatrix;
        Matrix4x4 P = targetCamera.projectionMatrix;
        Matrix4x4 MVP = P * V * M;

        Vector4 localPoint = new Vector4(0, 0, 0, 1);
        Vector4 worldPoint = M * localPoint;
        Vector4 clip = MVP * localPoint;

        Vector3 ndc = new Vector3(
            clip.x / clip.w,
            clip.y / clip.w,
            clip.z / clip.w
        );

        Vector3 screen = targetCamera.WorldToScreenPoint(worldPoint);

        bool clipped =
            Mathf.Abs(ndc.x) > 1f ||
            Mathf.Abs(ndc.y) > 1f ||
            Mathf.Abs(ndc.z) > 1f;

        if (trackedLight != null)
        {
            trackedLight.color = clipped ? clippedColor : visibleColor;
            trackedLight.intensity = clipped ? clippedIntensity : visibleIntensity;
        }

        string projMode = targetCamera.orthographic ? "ORTHOGRAPHIC" : "PERSPECTIVE";

        debugText.text =
            "=== MVP DEBUG ===\n\n" +
            "PROJECTION: " + projMode + "\n" +
            "WORLD:\n" + worldPoint.ToString("F3") + "\n" +
            "CLIP:\n" + clip.ToString("F3") + "\n" +
            "W:\n" + clip.w.ToString("F4") + "\n" +
            "NDC:\n" + ndc.ToString("F3") + "\n" +
            "SCREEN:\n" + screen.ToString("F1") + "\n" +
            "CLIPPED: " + clipped;

        matrixText.text =
            "=== MATRICES ===\n\n" +
            "M (Model)\n" + FormatMatrix(M) + "\n" +
            "V (View)\n" + FormatMatrix(V) + "\n" +
            "P (Projection)\n" + FormatMatrix(P) + "\n" +
            "MVP\n" + FormatMatrix(MVP);
    }

    public void ToggleProjection()
    {
        if (!targetCamera) return;

        targetCamera.orthographic = !targetCamera.orthographic;
        ApplyProjectionDefaults();
        UpdateButtonLabel();
    }

    void ApplyProjectionDefaults()
    {
        if (!targetCamera) return;

        if (targetCamera.orthographic)
            targetCamera.orthographicSize = orthographicSize;
        else
            targetCamera.fieldOfView = perspectiveFov;
    }

    void UpdateButtonLabel()
    {
        if (toggleProjectionButton == null) return;

        var tmp = toggleProjectionButton.GetComponentInChildren<TMP_Text>();
        if (tmp != null)
            tmp.text = targetCamera.orthographic ? "Projection: Orthographic" : "Projection: Perspective";
    }

    string FormatMatrix(Matrix4x4 m)
    {
        StringBuilder sb = new StringBuilder();
        sb.AppendLine(Row(m, 0));
        sb.AppendLine(Row(m, 1));
        sb.AppendLine(Row(m, 2));
        sb.AppendLine(Row(m, 3));
        return sb.ToString();
    }

    string Row(Matrix4x4 m, int r)
    {
        return string.Format(
            "{0,8:F3} {1,8:F3} {2,8:F3} {3,8:F3}",
            m[r, 0], m[r, 1], m[r, 2], m[r, 3]
        );
    }

    void OnDrawGizmos()
    {
        if (!targetCamera)
            return;

        Gizmos.color = Color.cyan;

        Matrix4x4 temp = Gizmos.matrix;

        Gizmos.matrix = targetCamera.transform.localToWorldMatrix;

        if (targetCamera.orthographic)
        {
            float size = targetCamera.orthographicSize;
            float aspect = targetCamera.aspect;
            float near = targetCamera.nearClipPlane;
            float far = targetCamera.farClipPlane;

            float height = size * 2f;
            float width = height * aspect;

            Gizmos.DrawWireCube(new Vector3(0, 0, near), new Vector3(width, height, 0.01f));
            Gizmos.DrawWireCube(new Vector3(0, 0, far), new Vector3(width, height, 0.01f));
        }
        else
        {
            Gizmos.DrawFrustum(
                Vector3.zero,
                targetCamera.fieldOfView,
                targetCamera.farClipPlane,
                targetCamera.nearClipPlane,
                targetCamera.aspect
            );
        }

        Gizmos.matrix = temp;
    }
}