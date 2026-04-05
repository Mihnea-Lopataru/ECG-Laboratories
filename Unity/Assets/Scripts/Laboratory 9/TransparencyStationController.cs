using UnityEngine;
using UnityEngine.Rendering;

public class TransparencyStationController : MonoBehaviour
{
    [Header("Objects")]
    public Transform rotatingGroup;
    public Renderer quadA;
    public Renderer quadB;

    [Header("Rotation")]
    public float rotationStep = 15f;

    private Quaternion startRotation;
    private int quadAStartOrder;
    private int quadBStartOrder;
    private bool swapped = false;

    void Start()
    {
        startRotation = rotatingGroup.localRotation;
        quadAStartOrder = quadA.sortingOrder;
        quadBStartOrder = quadB.sortingOrder;
    }

    public void RotateLeft()
    {
        rotatingGroup.Rotate(0f, -rotationStep, 0f, Space.Self);
    }

    public void RotateRight()
    {
        rotatingGroup.Rotate(0f, rotationStep, 0f, Space.Self);
    }

    public void SwapOrder()
    {
        swapped = !swapped;

        if (swapped)
        {
            quadA.sortingOrder = 1;
            quadB.sortingOrder = 0;
        }
        else
        {
            quadA.sortingOrder = quadAStartOrder;
            quadB.sortingOrder = quadBStartOrder;
        }
    }

    public void ResetStation()
    {
        rotatingGroup.localRotation = startRotation;
        quadA.sortingOrder = quadAStartOrder;
        quadB.sortingOrder = quadBStartOrder;
        swapped = false;
    }
}