using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshDrawer : MonoBehaviour
{
    public int planeWidth = 5;
    public int planeHeight = 5;

    public int resolution = 200;

    public int normalsPerVertex = 5;
    public int seed = 0;
    public float noiseScale = 15.0f;
    public float amplitude = 10.0f;
    public int nOctaves = 15;
    public float lacranaraty = 2.0f;
    public float percistance = 0.4f;
    public Vector2 offsett;


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
        meshData = MeshGenerator.GenerateMeshData(meshWidth, meshHeight, noiseScale, seed,
                                                  nOctaves, percistance, lacranaraty, offsett, amplitude, normalsPerVertex);

        scaleFactor =  planeWidth / (float) meshWidth;

        ApplyMeshData(meshData);
        GetComponent<MeshFilter>().mesh = mesh;
        GetComponent<MeshCollider>().sharedMesh = mesh;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void ApplyMeshData(MeshData meshData)
    {
        mesh.vertices = meshData.vertices;
        mesh.triangles = meshData.triangles;
        mesh.RecalculateNormals();

        transform.localScale = Vector3.one * scaleFactor;
    }
}
