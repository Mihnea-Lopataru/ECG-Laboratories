using UnityEngine;

public class DiffuseStationController : MonoBehaviour
{
    [Header("References")]
    public Transform diffuseObject;
    public Light pointLight;
    public Renderer targetRenderer;

    [Header("Material Settings")]
    public Color baseColor = new Color(0.1f, 0.1f, 0.1f, 1f);
    public Color diffuseColor = Color.white;

    [Range(0f, 1f)] public float kd = 1f;

    [Header("Orbit Settings")]
    public bool autoOrbitLight = true;
    public float orbitSpeed = 30f;
    public Vector3 orbitAxis = Vector3.up;

    [Header("Debug")]
    [Range(-1f, 1f)] public float approximateNdotL;

    private Material runtimeMaterial;

    private static readonly int BaseColorID = Shader.PropertyToID("_BaseColor");
    private static readonly int DiffuseColorID = Shader.PropertyToID("_DiffuseColor");
    private static readonly int KdID = Shader.PropertyToID("_Kd");
    private static readonly int LightPositionID = Shader.PropertyToID("_LightPosition");
    private static readonly int LightColorID = Shader.PropertyToID("_LightColorCustom");

    void Start()
    {
        if (targetRenderer != null)
        {
            runtimeMaterial = targetRenderer.material;
        }
    }

    void Update()
    {
        if (diffuseObject == null || pointLight == null || runtimeMaterial == null)
            return;

        if (autoOrbitLight)
        {
            pointLight.transform.RotateAround(
                diffuseObject.position,
                orbitAxis.normalized,
                orbitSpeed * Time.deltaTime
            );
        }

        runtimeMaterial.SetColor(BaseColorID, baseColor);
        runtimeMaterial.SetColor(DiffuseColorID, diffuseColor);
        runtimeMaterial.SetFloat(KdID, kd);

        runtimeMaterial.SetVector(LightPositionID, pointLight.transform.position);
        runtimeMaterial.SetColor(LightColorID, pointLight.color * pointLight.intensity);

        // Debug approximation (center normal)
        Vector3 normal = diffuseObject.forward.normalized;
        Vector3 lightDir = (pointLight.transform.position - diffuseObject.position).normalized;
        approximateNdotL = Vector3.Dot(normal, lightDir);
    }

    void OnDrawGizmos()
    {
        if (diffuseObject == null || pointLight == null)
            return;

        Vector3 pos = diffuseObject.position;

        Vector3 normal = diffuseObject.forward.normalized;
        Vector3 lightDir = (pointLight.transform.position - pos).normalized;

        Gizmos.color = Color.blue;
        Gizmos.DrawLine(pos, pos + normal * 2f);

        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(pos, pos + lightDir * 2f);
    }
}