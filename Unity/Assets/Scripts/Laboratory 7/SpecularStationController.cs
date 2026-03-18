using UnityEngine;

public class SpecularStationController : MonoBehaviour
{
    [Header("References")]
    public Transform specularObject;
    public Light pointLight;
    public Renderer targetRenderer;
    public Transform viewer;

    [Header("Lighting Parameters")]
    public Color baseColor = new Color(0.05f, 0.05f, 0.05f, 1f);
    public Color diffuseColor = new Color(0.7f, 0.7f, 0.7f, 1f);
    public Color specularColor = Color.white;

    [Range(0f, 1f)] public float kd = 0.3f;
    [Range(0f, 2f)] public float ks = 1.0f;
    [Range(1f, 128f)] public float shininess = 32f;

    [Header("Orbit Settings")]
    public bool autoOrbitLight = true;
    public float orbitSpeed = 30f;
    public Vector3 orbitAxis = Vector3.up;

    [Header("Debug Values")]
    public Vector3 approximateNormal;
    public Vector3 lightDirection;
    public Vector3 viewDirection;
    public Vector3 reflectionDirection;
    [Range(-1f, 1f)] public float approximateRdotV;

    private Material runtimeMaterial;

    private static readonly int BaseColorID = Shader.PropertyToID("_BaseColor");
    private static readonly int DiffuseColorID = Shader.PropertyToID("_DiffuseColor");
    private static readonly int SpecColorID = Shader.PropertyToID("_SpecColor");
    private static readonly int KdID = Shader.PropertyToID("_Kd");
    private static readonly int KsID = Shader.PropertyToID("_Ks");
    private static readonly int ShininessID = Shader.PropertyToID("_Shininess");
    private static readonly int LightPositionID = Shader.PropertyToID("_LightPosition");
    private static readonly int LightColorID = Shader.PropertyToID("_LightColorCustom");
    private static readonly int CameraPositionID = Shader.PropertyToID("_CameraPositionCustom");

    void Start()
    {
        if (targetRenderer != null)
        {
            runtimeMaterial = targetRenderer.material;
        }

        if (viewer == null && Camera.main != null)
        {
            viewer = Camera.main.transform;
        }
    }

    void Update()
    {
        if (specularObject == null || pointLight == null || runtimeMaterial == null || viewer == null)
            return;

        if (autoOrbitLight)
        {
            pointLight.transform.RotateAround(
                specularObject.position,
                orbitAxis.normalized,
                orbitSpeed * Time.deltaTime
            );
        }

        runtimeMaterial.SetColor(BaseColorID, baseColor);
        runtimeMaterial.SetColor(DiffuseColorID, diffuseColor);
        runtimeMaterial.SetColor(SpecColorID, specularColor);

        runtimeMaterial.SetFloat(KdID, kd);
        runtimeMaterial.SetFloat(KsID, ks);
        runtimeMaterial.SetFloat(ShininessID, shininess);

        runtimeMaterial.SetVector(LightPositionID, pointLight.transform.position);
        runtimeMaterial.SetColor(LightColorID, pointLight.color * pointLight.intensity);
        runtimeMaterial.SetVector(CameraPositionID, viewer.position);

        // Approximate debug vectors using the sphere center forward direction.
        // These are only for scene understanding, not for the actual shading result.
        approximateNormal = specularObject.forward.normalized;
        lightDirection = (pointLight.transform.position - specularObject.position).normalized;
        viewDirection = (viewer.position - specularObject.position).normalized;
        reflectionDirection = Vector3.Reflect(-lightDirection, approximateNormal).normalized;
        approximateRdotV = Vector3.Dot(reflectionDirection, viewDirection);
    }

    void OnDrawGizmos()
    {
        if (specularObject == null || pointLight == null)
            return;

        Vector3 pos = specularObject.position;

        Gizmos.color = Color.blue;
        Gizmos.DrawLine(pos, pos + approximateNormal * 2f);

        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(pos, pos + lightDirection * 2f);

        if (viewer != null)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawLine(pos, pos + viewDirection * 2f);

            Gizmos.color = Color.red;
            Gizmos.DrawLine(pos, pos + reflectionDirection * 2f);
        }
    }
}