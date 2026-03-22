using UnityEngine;

public class SamplingMipToggleButton : MonoBehaviour, IInteractable
{
    [SerializeField] private SamplingStationMipToggle stationController;
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
            stationController.ToggleMipMaps();
        }
    }
}