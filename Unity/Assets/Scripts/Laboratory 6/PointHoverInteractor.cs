using UnityEngine;

public class PointInteractionManager : MonoBehaviour
{
    [Header("References")]
    public Camera mainCamera;
    public SimplePlayerController playerController;

    [Header("Interaction")]
    public float interactDistance = 20f;
    public LayerMask interactableLayer;

    private InteractablePoint currentHoveredPoint;
    private DraggablePoint currentHoveredDraggable;
    private DraggablePoint selectedPoint;

    private bool isDragging = false;

    void Update()
    {
        if (!isDragging)
        {
            CheckHover();
        }

        HandleSelection();
        HandleDragging();
        HandleRelease();
    }

    void CheckHover()
    {
        Ray ray = mainCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));

        InteractablePoint newHoveredPoint = null;
        DraggablePoint newHoveredDraggable = null;

        if (Physics.Raycast(ray, out RaycastHit hit, interactDistance, interactableLayer))
        {
            newHoveredPoint = hit.collider.GetComponentInParent<InteractablePoint>();
            newHoveredDraggable = hit.collider.GetComponentInParent<DraggablePoint>();
        }

        if (currentHoveredPoint != newHoveredPoint)
        {
            if (currentHoveredPoint != null)
            {
                currentHoveredPoint.SetHighlight(false);
            }

            currentHoveredPoint = newHoveredPoint;
            currentHoveredDraggable = newHoveredDraggable;

            if (currentHoveredPoint != null)
            {
                currentHoveredPoint.SetHighlight(true);
            }
        }
    }

    void HandleSelection()
    {
        if (Input.GetMouseButtonDown(0) && currentHoveredDraggable != null)
        {
            selectedPoint = currentHoveredDraggable;
            selectedPoint.IsSelected = true;
            isDragging = true;

            if (playerController != null)
            {
                playerController.canLook = false;
            }

            Debug.Log("Started dragging: " + selectedPoint.name);
        }
    }

    void HandleDragging()
    {
        if (!isDragging || selectedPoint == null)
            return;

        Ray ray = mainCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
        Plane dragPlane = selectedPoint.GetDragPlane();

        if (dragPlane.Raycast(ray, out float enter))
        {
            Vector3 hitPoint = ray.GetPoint(enter);
            Vector3 clampedPoint = selectedPoint.ClampToAllowedArea(hitPoint);
            selectedPoint.transform.position = clampedPoint;
        }
    }

    void HandleRelease()
    {
        if (Input.GetMouseButtonUp(0))
        {
            isDragging = false;

            if (selectedPoint != null)
            {
                selectedPoint.IsSelected = false;
                selectedPoint = null;
            }

            if (playerController != null)
            {
                playerController.canLook = true;
            }
        }
    }
}