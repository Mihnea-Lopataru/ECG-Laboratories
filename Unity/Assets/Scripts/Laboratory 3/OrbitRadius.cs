using UnityEngine;

[DisallowMultipleComponent]
public class OrbitRadius : MonoBehaviour
{
    [SerializeField, Min(0f)] private float radius = 10f;
    [SerializeField] private Vector3 localDirection = Vector3.right;

    private void Reset()
    {
        Apply();
    }

    private void OnValidate()
    {
        Apply();
    }

    public void SetRadius(float r)
    {
        radius = Mathf.Max(0f, r);
        Apply();
    }

    public void SetDirection(Vector3 dir)
    {
        localDirection = dir;
        Apply();
    }

    private void Apply()
    {
        Vector3 dir = localDirection.sqrMagnitude > 1e-8f ? localDirection.normalized : Vector3.right;
        transform.localPosition = dir * radius;
    }
}
