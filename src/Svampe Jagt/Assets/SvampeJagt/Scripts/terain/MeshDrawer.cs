using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshDrawer : MonoBehaviour
{
    public int planeWidth = 1000;
    public int planeHeight = 1000;

    public int resolution = 255;

    public int normalsPerVertex = 1;
    public int seed = 0;
    public float noiseScale = 0.005f;
    public float amplitude = 60.0f;
    public int nOctaves = 30;
    public float lacranaraty = 2.0f;
    public float percistance = 0.4f;
    public Vector2 offsett;

    public Gradient gradient;


    MeshData meshData;

    float scaleFactor;

    Mesh mesh;
    // Start is called before the first frame update

    void Start()
    {
        mesh = new Mesh();
        mesh.indexFormat = UnityEngine.Rendering.IndexFormat.UInt32;
        int meshWidth = resolution;
        int meshHeight = resolution;// * planeHeight / planeWidth;
        meshData = MeshGenerator.GenerateMeshData(meshWidth, noiseScale, seed,
                                                  nOctaves, percistance, lacranaraty, offsett, amplitude, normalsPerVertex, gradient);

        scaleFactor =  planeWidth / (float) meshWidth;

        ApplyMeshData(meshData);
        GetComponent<MeshFilter>().mesh = mesh;
        GetComponent<MeshCollider>().sharedMesh = mesh;
    }

    // Update is called once per frame
    void Update()
    {
        // meshData = MeshGenerator.GenerateMeshData(resolution, noiseScale, seed,
        //                                           nOctaves, percistance, lacranaraty, offsett, amplitude, normalsPerVertex, gradient);

        // ApplyMeshData(meshData);
    }

    void ApplyMeshData(MeshData meshData)
    {
        mesh.Clear();
        mesh.vertices = meshData.vertices;
        mesh.triangles = meshData.triangles;
        mesh.colors = meshData.colors;
        mesh.RecalculateNormals();

        transform.localScale = Vector3.one * scaleFactor;
    }
}
