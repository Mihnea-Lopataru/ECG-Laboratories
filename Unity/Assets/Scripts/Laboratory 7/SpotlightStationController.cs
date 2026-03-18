using UnityEngine;

public class SpotlightStationController : MonoBehaviour
{
    [Header("References")]
    public Transform spotlightObject;
    public Transform spotlightTransform;
    public Renderer targetRenderer;

    [Header("Material Settings")]
    public Color baseColor = new Color(0.1f, 0.1f, 0.1f, 1f);
    public Color diffuseColor = Color.white;

    [Range(0f, 1f)] public float kd = 1f;

    [Header("Attenuation Coefficients")]
    public float attenA = 1f;
    public float attenB = 0.1f;
    public float attenC = 0.03f;

    [Header("Spotlight Angles")]
    [Range(1f, 89f)] public float innerCutoffAngle = 20f;
    [Range(1f, 89f)] public float outerCutoffAngle = 30f;

    [Header("Motion")]
    public bool autoRotateSpotlight = true;
    public float rotationSpeed = 25f;

    [Header("Debug")]
    public float currentDistance;
    [Range(-1f, 1f)] public float currentCosTheta;
    [Range(0f, 1f)] public float currentSpotFactor;

    private Material runtimeMaterial;

    private static readonly int BaseColorID = Shader.PropertyToID("_BaseColor");
    private static readonly int DiffuseColorID = Shader.PropertyToID("_DiffuseColor");
    private static readonly int KdID = Shader.PropertyToID("_Kd");
    private static readonly int AttenAID = Shader.PropertyToID("_AttenA");
    private static readonly int AttenBID = Shader.PropertyToID("_AttenB");
    private static readonly int AttenCID = Shader.PropertyToID("_AttenC");
    private static readonly int LightPositionID = Shader.PropertyToID("_LightPosition");
    private static readonly int LightDirectionID = Shader.PropertyToID("_LightDirection");
    private static readonly int LightColorID = Shader.PropertyToID("_LightColorCustom");
    private static readonly int InnerCutoffCosID = Shader.PropertyToID("_InnerCutoffCos");
    private static readonly int OuterCutoffCosID = Shader.PropertyToID("_OuterCutoffCos");

    void Start()
    {
        if (targetRenderer != null)
        {
            runtimeMaterial = targetRenderer.material;
        }
    }

    void Update()
    {
        if (spotlightObject == null || spotlightTransform == null || runtimeMaterial == null)
            return;

        if (autoRotateSpotlight)
        {
            spotlightTransform.RotateAround(
                spotlightObject.position,
                Vector3.up,
                rotationSpeed * Time.deltaTime
            );

            spotlightTransform.LookAt(spotlightObject.position);
        }

        float innerCos = Mathf.Cos(innerCutoffAngle * Mathf.Deg2Rad);
        float outerCos = Mathf.Cos(outerCutoffAngle * Mathf.Deg2Rad);

        // Ensure correct order for smoothstep
        if (innerCos < outerCos)
        {
            float temp = innerCos;
            innerCos = outerCos;
            outerCos = temp;
        }

        runtimeMaterial.SetColor(BaseColorID, baseColor);
        runtimeMaterial.SetColor(DiffuseColorID, diffuseColor);
        runtimeMaterial.SetFloat(KdID, kd);

        runtimeMaterial.SetFloat(AttenAID, attenA);
        runtimeMaterial.SetFloat(AttenBID, attenB);
        runtimeMaterial.SetFloat(AttenCID, attenC);

        runtimeMaterial.SetVector(LightPositionID, spotlightTransform.position);
        runtimeMaterial.SetVector(LightDirectionID, spotlightTransform.forward);
        runtimeMaterial.SetColor(LightColorID, Color.white);

        runtimeMaterial.SetFloat(InnerCutoffCosID, innerCos);
        runtimeMaterial.SetFloat(OuterCutoffCosID, outerCos);

        Vector3 lightVec = spotlightObject.position - spotlightTransform.position;
        currentDistance = lightVec.magnitude;

        Vector3 LfromPointToLight = (spotlightTransform.position - spotlightObject.position).normalized;
        Vector3 spotDir = spotlightTransform.forward.normalized;

        currentCosTheta = Vector3.Dot(-LfromPointToLight, spotDir);
        currentSpotFactor = Mathf.InverseLerp(outerCos, innerCos, currentCosTheta);
        currentSpotFactor = Mathf.Clamp01(currentSpotFactor);
    }

    void OnDrawGizmos()
    {
        if (spotlightTransform == null)
            return;

        Vector3 pos = spotlightTransform.position;
        Vector3 dir = spotlightTransform.forward.normalized;

        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(pos, pos + dir * 3f);

        if (spotlightObject != null)
        {
            Gizmos.color = Color.magenta;
            Gizmos.DrawLine(pos, spotlightObject.position);
        }
    }
}