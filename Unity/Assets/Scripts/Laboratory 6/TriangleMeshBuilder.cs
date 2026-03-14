using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class TriangleMeshBuilder : MonoBehaviour
{
    public Transform vertexA;
    public Transform vertexB;
    public Transform vertexC;

    private Mesh mesh;

    private void Start()
    {
        mesh = new Mesh();
        mesh.name = "Triangle Mesh";
        GetComponent<MeshFilter>().mesh = mesh;
    }

    private void Update()
    {
        UpdateTriangle();
    }

    private void UpdateTriangle()
    {
        Vector3[] vertices = new Vector3[3];

        vertices[0] = vertexA.localPosition;
        vertices[1] = vertexB.localPosition;
        vertices[2] = vertexC.localPosition;

        int[] triangles = new int[] { 0, 2, 1 };

        mesh.Clear();
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.RecalculateNormals();
        mesh.RecalculateBounds();
    }
}