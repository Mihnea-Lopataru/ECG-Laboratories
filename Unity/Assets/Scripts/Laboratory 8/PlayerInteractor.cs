using UnityEngine;

public class PlayerInteractor : MonoBehaviour
{
    [Header("Ray Settings")]
    [SerializeField] private Camera playerCamera;
    [SerializeField] private float interactDistance = 4f;
    [SerializeField] private KeyCode interactKey = KeyCode.E;

    private void Update()
    {
        if (Input.GetKeyDown(interactKey))
        {
            TryInteract();
        }
    }

    private void TryInteract()
    {
        if (playerCamera == null)
        {
            Debug.LogWarning("PlayerInteractor: No player camera assigned.");
            return;
        }

        Ray ray = new Ray(playerCamera.transform.position, playerCamera.transform.forward);

        if (Physics.Raycast(ray, out RaycastHit hit, interactDistance))
        {
            IInteractable interactable = hit.collider.GetComponent<IInteractable>();

            if (interactable != null)
            {
                interactable.Interact();
            }
        }
    }
}