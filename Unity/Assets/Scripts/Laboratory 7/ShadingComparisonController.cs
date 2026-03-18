using UnityEngine;

public class ShadingComparisonController : MonoBehaviour
{
    [Header("References")]
    public Renderer flatRenderer;
    public Renderer gouraudRenderer;
    public Renderer phongRenderer;

    public Light pointLight;
    public Transform viewer;
    public Transform motionCenter;

    [Header("Lighting Parameters")]
    public Color baseColor = new Color(0.08f, 0.08f, 0.08f, 1f);
    public Color diffuseColor = new Color(0.7f, 0.7f, 0.7f, 1f);
    public Color specularColor = Color.white;

    [Range(0f, 1f)] public float kd = 0.8f;
    [Range(0f, 2f)] public float ks = 1.0f;
    [Range(1f, 128f)] public float shininess = 32f;

    [Header("Light Motion")]
    public bool autoMoveLight = true;
    public float moveSpeed = 1.5f;
    public float moveRange = 4f;
    public Vector3 moveAxis = Vector3.right;
    public float fixedHeight = 3f;
    public float fixedDepth = 3f;

    private Material flatMat;
    private Material gouraudMat;
    private Material phongMat;

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
        if (flatRenderer != null) flatMat = flatRenderer.material;
        if (gouraudRenderer != null) gouraudMat = gouraudRenderer.material;
        if (phongRenderer != null) phongMat = phongRenderer.material;

        if (viewer == null && Camera.main != null)
            viewer = Camera.main.transform;

        if (motionCenter == null)
            motionCenter = transform;
    }

    void Update()
    {
        if (pointLight == null || viewer == null)
            return;

        if (autoMoveLight)
        {
            Vector3 axis = moveAxis.normalized;
            float offset = Mathf.Sin(Time.time * moveSpeed) * moveRange;

            Vector3 centerPos = motionCenter.position;
            Vector3 lightPos = centerPos + axis * offset;
            lightPos.y = centerPos.y + fixedHeight;
            lightPos.z = centerPos.z + fixedDepth;

            pointLight.transform.position = lightPos;
        }

        UpdateMaterial(flatMat);
        UpdateMaterial(gouraudMat);
        UpdateMaterial(phongMat);
    }

    private void UpdateMaterial(Material mat)
    {
        if (mat == null) return;

        mat.SetColor(BaseColorID, baseColor);
        mat.SetColor(DiffuseColorID, diffuseColor);
        mat.SetColor(SpecColorID, specularColor);

        mat.SetFloat(KdID, kd);
        mat.SetFloat(KsID, ks);
        mat.SetFloat(ShininessID, shininess);

        mat.SetVector(LightPositionID, pointLight.transform.position);
        mat.SetColor(LightColorID, pointLight.color * pointLight.intensity);
        mat.SetVector(CameraPositionID, viewer.position);
    }
}