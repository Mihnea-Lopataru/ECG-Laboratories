using UnityEngine;

public class AliasingMipmapToggleButton : MonoBehaviour, IInteractable
{
    [SerializeField] private AliasingMipmapStation stationController;
    [SerializeField] private string animationStateName;

    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void Interact()
    {
        if (animator != null)
        {
            animator.Play(animationStateName, 0, 0f);
        }

        if (stationController != null)
        {
            stationController.ToggleMipmaps();
        }
    }
}