using UnityEngine;
using UnityEngine.UI;

public class SolarSystemUIController : MonoBehaviour
{
    [Header("Orbit Pivots")]
    [SerializeField] private OrbitPivot earthOrbitCorrect;
    [SerializeField] private OrbitPivotWrong earthOrbitWrong;

    [SerializeField] private OrbitPivot moonOrbitCorrect;
    [SerializeField] private OrbitPivotWrong moonOrbitWrong;

    [Header("Self Spin")]
    [SerializeField] private SelfSpin earthSpinCorrect;
    [SerializeField] private SelfSpinWrong earthSpinWrong;

    [SerializeField] private SelfSpin moonSpinCorrect;
    [SerializeField] private SelfSpinWrong moonSpinWrong;

    [Header("UI Buttons")]
    [SerializeField] private Button earthOrbitButton;
    [SerializeField] private Button earthSpinButton;
    [SerializeField] private Button moonOrbitButton;
    [SerializeField] private Button moonSpinButton;

    [Header("Button Colors")]
    [SerializeField] private Color correctColor = Color.green;
    [SerializeField] private Color wrongColor = Color.red;

    [Header("Camera")]
    [SerializeField] private Camera mainCamera;
    [SerializeField] private Transform sunTarget;
    [SerializeField] private Transform earthTarget;
    [SerializeField] private Transform moonTarget;

    [SerializeField] private Vector3 cameraOffset = new Vector3(0, 5, -10);
    [SerializeField] private float cameraMoveSpeed = 5f;

    private Transform currentTarget;

    // ---- internal state ----
    private bool earthOrbitCorrectState = true;
    private bool earthSpinCorrectState = true;
    private bool moonOrbitCorrectState = true;
    private bool moonSpinCorrectState = true;

    private void Start()
    {
        ApplyEarthOrbitState();
        ApplyEarthSpinState();
        ApplyMoonOrbitState();
        ApplyMoonSpinState();

        currentTarget = sunTarget;
    }

    private void LateUpdate()
    {
        if (currentTarget == null) return;

        Vector3 desiredPos = currentTarget.position + cameraOffset;
        mainCamera.transform.position =
            Vector3.Lerp(mainCamera.transform.position, desiredPos, Time.deltaTime * cameraMoveSpeed);

        mainCamera.transform.LookAt(currentTarget);
    }

    // ---------- Button callbacks ----------

    public void OnEarthOrbitClicked()
    {
        earthOrbitCorrectState = !earthOrbitCorrectState;
        ApplyEarthOrbitState();
    }

    public void OnEarthSpinClicked()
    {
        earthSpinCorrectState = !earthSpinCorrectState;
        ApplyEarthSpinState();
    }

    public void OnMoonOrbitClicked()
    {
        moonOrbitCorrectState = !moonOrbitCorrectState;
        ApplyMoonOrbitState();
    }

    public void OnMoonSpinClicked()
    {
        moonSpinCorrectState = !moonSpinCorrectState;
        ApplyMoonSpinState();
    }

    // ---------- State application ----------

    private void ApplyEarthOrbitState()
    {
        earthOrbitCorrect.enabled = earthOrbitCorrectState;
        earthOrbitWrong.enabled = !earthOrbitCorrectState;

        earthOrbitCorrect.transform.localRotation = Quaternion.identity;
        earthOrbitWrong.transform.localRotation = Quaternion.identity;

        SetButtonColor(earthOrbitButton, earthOrbitCorrectState);
    }

    private void ApplyEarthSpinState()
    {
        earthSpinCorrect.enabled = earthSpinCorrectState;
        earthSpinWrong.enabled = !earthSpinCorrectState;

        SetButtonColor(earthSpinButton, earthSpinCorrectState);
    }

    private void ApplyMoonOrbitState()
    {
        moonOrbitCorrect.enabled = moonOrbitCorrectState;
        moonOrbitWrong.enabled = !moonOrbitCorrectState;

        moonOrbitCorrect.transform.localRotation = Quaternion.identity;
        moonOrbitWrong.transform.localRotation = Quaternion.identity;

        SetButtonColor(moonOrbitButton, moonOrbitCorrectState);
    }

    private void ApplyMoonSpinState()
    {
        moonSpinCorrect.enabled = moonSpinCorrectState;
        moonSpinWrong.enabled = !moonSpinCorrectState;

        SetButtonColor(moonSpinButton, moonSpinCorrectState);
    }

    // ---------- UI helpers ----------

    private void SetButtonColor(Button button, bool isCorrect)
    {
        if (button == null) return;

        Image img = button.GetComponent<Image>();
        if (img == null) return;

        img.color = isCorrect ? correctColor : wrongColor;
    }

    // ---------- Camera focus ----------

    public void FocusSun()
    {
        currentTarget = sunTarget;
    }

    public void FocusEarth()
    {
        currentTarget = earthTarget;
    }

    public void FocusMoon()
    {
        currentTarget = moonTarget;
    }
}
