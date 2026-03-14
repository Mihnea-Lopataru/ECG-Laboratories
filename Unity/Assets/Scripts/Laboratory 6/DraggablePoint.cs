using UnityEngine;

[RequireComponent(typeof(Collider))]
public class DraggablePoint : MonoBehaviour
{
    [Header("Bounds")]
    public Vector3 boundsCenter = Vector3.zero;
    public Vector3 boundsSize = new Vector3(1f, 6f, 10f);

    [Header("Axis Lock")]
    public bool lockX = true;

    private float lockedX;

    public bool IsSelected { get; set; }

    void Start()
    {
        lockedX = transform.position.x;
    }

    public Vector3 ClampToAllowedArea(Vector3 targetPosition)
    {
        Vector3 halfSize = boundsSize * 0.5f;

        float minY = boundsCenter.y - halfSize.y;
        float maxY = boundsCenter.y + halfSize.y;

        float minZ = boundsCenter.z - halfSize.z;
        float maxZ = boundsCenter.z + halfSize.z;

        targetPosition.y = Mathf.Clamp(targetPosition.y, minY, maxY);
        targetPosition.z = Mathf.Clamp(targetPosition.z, minZ, maxZ);

        if (lockX)
        {
            targetPosition.x = lockedX;
        }
        else
        {
            float minX = boundsCenter.x - halfSize.x;
            float maxX = boundsCenter.x + halfSize.x;
            targetPosition.x = Mathf.Clamp(targetPosition.x, minX, maxX);
        }

        return targetPosition;
    }

    public Plane GetDragPlane()
    {
        float xValue = lockX ? lockedX : transform.position.x;
        return new Plane(Vector3.right, new Vector3(xValue, 0f, 0f));
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(boundsCenter, boundsSize);
    }
}