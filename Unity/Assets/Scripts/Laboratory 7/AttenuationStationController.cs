using UnityEngine;

public class AttenuationStationController : MonoBehaviour
{
    [Header("References")]
    public Transform attenuationObject;
    public Light pointLight;
    public Renderer targetRenderer;

    [Header("Material Settings")]
    public Color baseColor = new Color(0.1f, 0.1f, 0.1f, 1f);
    public Color diffuseColor = Color.white;

    [Range(0f, 1f)] public float kd = 1f;

    [Header("Attenuation Coefficients")]
    public float attenA = 1f;
    public float attenB = 0.2f;
    public float attenC = 0.05f;

    [Header("Light Motion")]
    public bool autoMoveLight = true;
    public float moveSpeed = 2f;
    public float minDistance = 2f;
    public float maxDistance = 8f;

    [Header("Debug")]
    public float currentDistance;
    public float attenuationFactor;

    private Material runtimeMaterial;
    private Vector3 initialDirection;

    private static readonly int BaseColorID = Shader.PropertyToID("_BaseColor");
    private static readonly int DiffuseColorID = Shader.PropertyToID("_DiffuseColor");
    private static readonly int KdID = Shader.PropertyToID("_Kd");
    private static readonly int AttenAID = Shader.PropertyToID("_AttenA");
    private static readonly int AttenBID = Shader.PropertyToID("_AttenB");
    private static readonly int AttenCID = Shader.PropertyToID("_AttenC");
    private static readonly int LightPositionID = Shader.PropertyToID("_LightPosition");
    private static readonly int LightColorID = Shader.PropertyToID("_LightColorCustom");

    void Start()
    {
        if (targetRenderer != null)
        {
            runtimeMaterial = targetRenderer.material;
        }

        if (attenuationObject != null && pointLight != null)
        {
            initialDirection = (pointLight.transform.position - attenuationObject.position).normalized;
            if (initialDirection == Vector3.zero)
                initialDirection = Vector3.forward;
        }
    }

    void Update()
    {
        if (attenuationObject == null || pointLight == null || runtimeMaterial == null)
            return;

        if (autoMoveLight)
        {
            float t = (Mathf.Sin(Time.time * moveSpeed) + 1f) * 0.5f;
            float distance = Mathf.Lerp(minDistance, maxDistance, t);
            pointLight.transform.position = attenuationObject.position + initialDirection * distance + Vector3.up * 1.0f;
        }

        currentDistance = Vector3.Distance(pointLight.transform.position, attenuationObject.position);
        attenuationFactor = 1f / (attenA + attenB * currentDistance + attenC * currentDistance * currentDistance);

        runtimeMaterial.SetColor(BaseColorID, baseColor);
        runtimeMaterial.SetColor(DiffuseColorID, diffuseColor);
        runtimeMaterial.SetFloat(KdID, kd);

        runtimeMaterial.SetFloat(AttenAID, attenA);
        runtimeMaterial.SetFloat(AttenBID, attenB);
        runtimeMaterial.SetFloat(AttenCID, attenC);

        runtimeMaterial.SetVector(LightPositionID, pointLight.transform.position);
        runtimeMaterial.SetColor(LightColorID, pointLight.color * pointLight.intensity);
    }

    void OnDrawGizmos()
    {
        if (attenuationObject == null || pointLight == null)
            return;

        Gizmos.color = Color.cyan;
        Gizmos.DrawLine(attenuationObject.position, pointLight.transform.position);
    }
}