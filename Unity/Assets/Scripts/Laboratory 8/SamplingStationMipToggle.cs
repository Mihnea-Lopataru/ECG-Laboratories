using UnityEngine;

public class SamplingStationMipToggle : MonoBehaviour
{
    [Header("Materials")]
    [SerializeField] private Material pointMaterial;
    [SerializeField] private Material bilinearMaterial;
    [SerializeField] private Material trilinearMaterial;

    [Header("Mip ON Textures")]
    [SerializeField] private Texture pointMipOn;
    [SerializeField] private Texture bilinearMipOn;
    [SerializeField] private Texture trilinearMipOn;

    [Header("Mip OFF Textures")]
    [SerializeField] private Texture pointMipOff;
    [SerializeField] private Texture bilinearMipOff;
    [SerializeField] private Texture trilinearMipOff;

    [Header("State")]
    [SerializeField] private bool useMipMaps = true;

    private void Start()
    {
        ApplyCurrentMode();
    }

    public void ToggleMipMaps()
    {
        useMipMaps = !useMipMaps;
        ApplyCurrentMode();
    }

    public bool AreMipMapsEnabled()
    {
        return useMipMaps;
    }

    private void ApplyCurrentMode()
    {
        if (useMipMaps)
        {
            pointMaterial.mainTexture = pointMipOn;
            bilinearMaterial.mainTexture = bilinearMipOn;
            trilinearMaterial.mainTexture = trilinearMipOn;
        }
        else
        {
            pointMaterial.mainTexture = pointMipOff;
            bilinearMaterial.mainTexture = bilinearMipOff;
            trilinearMaterial.mainTexture = trilinearMipOff;
        }

        Debug.Log("MipMaps: " + (useMipMaps ? "ON" : "OFF"));
    }
}