using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Camera")]
    public Transform mainCamera;

    [Header("Views")]
    public Transform viewDepth;
    public Transform viewTransparency;
    public Transform viewAA;

    [Header("UI Panels")]
    public GameObject panelDepth;
    public GameObject panelTransparency;
    public GameObject panelAA;

    [Header("Camera Offset")]
    public Vector3 positionOffset = new Vector3(0f, 12f, -10f);
    public Vector3 rotationOffset = new Vector3(30f, 0f, 0f);

    void Start()
    {
        ShowDepthStation();
    }

    public void ShowDepthStation()
    {
        MoveCamera(viewDepth);

        panelDepth.SetActive(true);
        panelTransparency.SetActive(false);
        panelAA.SetActive(false);
    }

    public void ShowTransparencyStation()
    {
        MoveCamera(viewTransparency);

        panelDepth.SetActive(false);
        panelTransparency.SetActive(true);
        panelAA.SetActive(false);
    }

    public void ShowAAStation()
    {
        MoveCamera(viewAA);

        panelDepth.SetActive(false);
        panelTransparency.SetActive(false);
        panelAA.SetActive(true);
    }

    private void MoveCamera(Transform targetView)
    {
        mainCamera.position = targetView.position + positionOffset;
        mainCamera.rotation = Quaternion.Euler(rotationOffset) * targetView.rotation;
    }
}