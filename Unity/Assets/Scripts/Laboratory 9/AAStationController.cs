using UnityEngine;
using UnityEngine.Rendering;

public class AAStationController : MonoBehaviour
{
    [Header("Resolution")]
    public float lowResolutionScale = 0.5f;
    public float defaultResolutionScale = 1f;

    private int defaultAA;
    private bool aaEnabled = true;

    void Start()
    {
        defaultAA = QualitySettings.antiAliasing;

        ScalableBufferManager.ResizeBuffers(lowResolutionScale, lowResolutionScale);
    }

    public void ToggleAA()
    {
        aaEnabled = !aaEnabled;

        if (aaEnabled)
        {
            QualitySettings.antiAliasing = defaultAA > 0 ? defaultAA : 4;
        }
        else
        {
            QualitySettings.antiAliasing = 0;
        }
    }

    public void ResetAA()
    {
        QualitySettings.antiAliasing = defaultAA;
        aaEnabled = true;

        ScalableBufferManager.ResizeBuffers(defaultResolutionScale, defaultResolutionScale);
    }
}