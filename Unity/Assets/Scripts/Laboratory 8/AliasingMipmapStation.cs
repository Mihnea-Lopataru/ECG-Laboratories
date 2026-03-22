using UnityEngine;

public class AliasingMipmapStation : MonoBehaviour
{
    [SerializeField] private Material targetMaterial;
    [SerializeField] private Texture mipOnTexture;
    [SerializeField] private Texture mipOffTexture;
    [SerializeField] private bool mipmapsEnabled = true;

    private void Start()
    {
        ApplyCurrentMode();
    }

    public void ToggleMipmaps()
    {
        mipmapsEnabled = !mipmapsEnabled;
        ApplyCurrentMode();
    }

    private void ApplyCurrentMode()
    {
        if (targetMaterial == null)
            return;

        targetMaterial.mainTexture = mipmapsEnabled ? mipOnTexture : mipOffTexture;

        Debug.Log("Aliasing station mipmaps: " + (mipmapsEnabled ? "ON" : "OFF"));
    }
}