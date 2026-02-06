using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private Vector3 offset = new Vector3(0f, 4f, -6f);
    [SerializeField] private float smoothTime = 0.08f;

    [Header("Collision")]
    [SerializeField] private float collisionPadding = 0.3f;
    [SerializeField] private float sphereRadius = 0.2f;
    [SerializeField] private LayerMask obstacleMask = ~0;

    private Vector3 velocity;

    private void LateUpdate()
    {
        if (target == null) return;

        Vector3 desired = target.position + offset;

        Vector3 from = target.position;
        Vector3 to = desired;
        Vector3 dir = (to - from);
        float dist = dir.magnitude;

        if (dist > 0.001f)
        {
            if (Physics.SphereCast(from, sphereRadius, dir.normalized, out RaycastHit hit, dist, obstacleMask))
            {
                desired = hit.point - dir.normalized * collisionPadding;
            }
        }

        transform.position = Vector3.SmoothDamp(transform.position, desired, ref velocity, smoothTime);
        transform.LookAt(target);
    }
}
