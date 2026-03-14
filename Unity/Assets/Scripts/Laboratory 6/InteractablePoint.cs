using UnityEngine;

[RequireComponent(typeof(Collider))]
public class InteractablePoint : MonoBehaviour
{
    public Material highlightMaterial;

    private Renderer objectRenderer;
    private Material originalMaterial;
    private bool isHighlighted = false;

    void Awake()
    {
        objectRenderer = GetComponent<Renderer>();

        if (objectRenderer == null)
        {
            objectRenderer = GetComponentInChildren<Renderer>();
        }

        if (objectRenderer != null)
        {
            originalMaterial = objectRenderer.material;
        }
    }

    public void SetHighlight(bool highlight)
    {
        if (objectRenderer == null || highlightMaterial == null || originalMaterial == null)
            return;

        if (highlight && !isHighlighted)
        {
            objectRenderer.material = highlightMaterial;
            isHighlighted = true;
        }
        else if (!highlight && isHighlighted)
        {
            objectRenderer.material = originalMaterial;
            isHighlighted = false;
        }
    }
}