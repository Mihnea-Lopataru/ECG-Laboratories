using UnityEngine;
using TMPro;

public class HudController : MonoBehaviour
{
    [Header("Target")]
    public Transform drone;

    [Header("Rotation Controller")]
    public DroneController droneController;

    [Header("UI References")]
    public TMP_Text positionText;
    public TMP_Text eulerText;
    public TMP_Text quaternionText;
    public TMP_Text axisAngleText;

    [Header("Toggle Button Label")]
    public TMP_Text rotationTypeButtonLabel;

    void Update()
    {
        if (drone == null) return;

        UpdatePosition();
        UpdateEuler();
        UpdateQuaternion();
        UpdateAxisAngle();
        UpdateRotationModeLabel();
    }

    void UpdateRotationModeLabel()
    {
        if (rotationTypeButtonLabel == null || droneController == null) return;

        rotationTypeButtonLabel.text = $"Rotation Type: {droneController.CurrentRotationMode}";
    }

    void UpdatePosition()
    {
        Vector3 p = drone.position;
        positionText.text =
            "Position (world):\n" +
            $"X: {p.x:F3}\n" +
            $"Y: {p.y:F3}\n" +
            $"Z: {p.z:F3}";
    }

    void UpdateEuler()
    {
        Vector3 e = drone.eulerAngles;
        eulerText.text =
            "Euler Angles (deg):\n" +
            $"X: {e.x:F3}\n" +
            $"Y: {e.y:F3}\n" +
            $"Z: {e.z:F3}";
    }

    void UpdateQuaternion()
    {
        Quaternion q = drone.rotation;
        quaternionText.text =
            "Quaternion:\n" +
            $"X: {q.x:F3}\n" +
            $"Y: {q.y:F3}\n" +
            $"Z: {q.z:F3}\n" +
            $"W: {q.w:F3}";
    }

    void UpdateAxisAngle()
    {
        drone.rotation.ToAngleAxis(out float angle, out Vector3 axis);
        axisAngleText.text =
            "Axis-Angle:\n" +
            $"Axis:\n({axis.x:F3}, {axis.y:F3}, {axis.z:F3})\n" +
            $"Angle: {angle:F3}°";
    }

    public void ToggleRotationModeFromUI()
    {
        if (droneController == null) return;
        droneController.ToggleRotationMode();
        UpdateRotationModeLabel();
    }
}
