using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MeshGenerator
{
    public static MeshData GenerateMeshData(int mapSize, float noiseScale, int seed,
                                  int nOctaves, float percistance, float lacranaraty, Vector2 offsett, float amplitude, int normalsPerVertex, Gradient gradient)
    {
        float[] heightMap = Noise.MakeNoiseMap(mapSize * normalsPerVertex, noiseScale / normalsPerVertex, seed,
                                  nOctaves, percistance, lacranaraty, offsett);


        
        Erosion.EroteMap(heightMap, mapSize * normalsPerVertex, 200000, 0.05f, 4f, 0.3f, 0.01f, 0.3f, -4.0f, 0.01f, 0.000001f, 30, 3);

        var blurMap = new float[heightMap.GetLength(0)];
        blurMap = ArrayMath.BlurArray(heightMap, 5, mapSize, mapSize);

        heightMap = ArrayMath.CombineArrays(heightMap, blurMap, 0.5f);
        // for (int i = 0; i < heightMap.GetLength(0) / 2; i++)
        // {
        //     heightMap[i] = errotedMap[i];
        // }

        float[] stepnessMap = ArrayMath.CalculateStepnes(heightMap, mapSize, mapSize);
        ArrayMath.Normalize(stepnessMap);
        //ArrayMath.ScaleArray(stepnessMap, 1);


        MeshData meshData = new MeshData(mapSize, mapSize);
        int verticeIndex = 0;

        for (int x = 0; x < mapSize; x++)
        {
            for (int z = 0; z < mapSize; z++)
            {
                float y = heightMap[x * normalsPerVertex + z * normalsPerVertex * mapSize] * amplitude;
                meshData.vertices[verticeIndex] = new Vector3(x, y, z);
                meshData.colors[verticeIndex] = gradient.Evaluate(stepnessMap[x + z * mapSize] + heightMap[x + z * mapSize] * 0.3f);

                if (x < mapSize - 1 && z < mapSize - 1)
                {   
                    
                    meshData.AddTriangle(verticeIndex, verticeIndex + 1, verticeIndex + mapSize);
                    meshData.AddTriangle(verticeIndex + 1, verticeIndex + mapSize + 1, verticeIndex + mapSize);

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

    public Color[] colors;

    int numberOfTriangles;


    public MeshData(int meshWidth, int meshHeight)
    {
        vertices = new Vector3[meshWidth * meshHeight];
        triangles = new int[(meshWidth - 1) * (meshHeight - 1) * 6];
        colors = new Color[meshWidth * meshHeight];
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