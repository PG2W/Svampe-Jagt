using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MeshGenerator
{
    public static MeshData GenerateMeshData(int width, int height, float noiseScale, int seed,
                                  int nOctaves, float percistance, float lacranaraty, Vector2 offsett, float amplitude, int normalsPerVertex)
    {
        float[,] heightMap = Noise.MakeNoiseMap(width * normalsPerVertex, height * normalsPerVertex, noiseScale / normalsPerVertex, seed,
                                  nOctaves, percistance, lacranaraty, offsett);
                                  
        MeshData meshData = new MeshData(width, height);
        int verticeIndex = 0;

        for (int x = 0; x < width; x++)
        {
            for (int z = 0; z < height; z++)
            {
                float y = heightMap[x * normalsPerVertex, z * normalsPerVertex] * amplitude;
                meshData.vertices[verticeIndex] = new Vector3(x, y, z);

                if (x < width - 1 && z < height - 1)
                {
                    meshData.AddTriangle(verticeIndex, verticeIndex + 1, verticeIndex + width);
                    meshData.AddTriangle(verticeIndex + 1, verticeIndex + width + 1, verticeIndex + width);
                }
                
                verticeIndex++;
            }
        }

        return meshData;
    }


}



public class MeshData
{
    public Vector3[] vertices;
    public int[] triangles;

    int numberOfTriangles;


    public MeshData(int meshWidth, int meshHeight)
    {
        vertices = new Vector3[meshWidth * meshHeight];
        triangles = new int[(meshWidth - 1) * (meshHeight - 1) * 6];
        numberOfTriangles = 0;
    }

    public void AddTriangle(int a, int b, int c)
    {
        triangles[numberOfTriangles] = a;
        triangles[numberOfTriangles + 1] = b;
        triangles[numberOfTriangles + 2] = c;
        numberOfTriangles += 3;
    }

}