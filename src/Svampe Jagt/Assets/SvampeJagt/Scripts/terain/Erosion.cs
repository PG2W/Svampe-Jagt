using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Erosion
{
    public static float[] EroteMap(float[] map, int mapSize, int iterations, float inertia, float capacity, float deposition, float minCarryCapacity, float erosion, float gravaty, float evaporation, float stuckSpeed, int nSteps, int radius)
    {
        var random = new System.Random(0);
        var brush = new Brush();
        brush.Initialize(mapSize, mapSize, radius);


        for (int i = 0; i < iterations; i++)
        {
            map = SimulateDrop(map, mapSize, random, inertia, capacity, deposition, minCarryCapacity, erosion, gravaty, evaporation, stuckSpeed, nSteps, brush);
        }

        map = ArrayMath.Normalize(map);

        float minValue = 9999;
        float maxValue = -9999;
        for (int i = 0; i < mapSize * mapSize; i++)
        {
            minValue = Math.Min(minValue, map[i]);
            maxValue = Math.Max(maxValue, map[i]);
        }

        Debug.Log(maxValue);
        Debug.Log(minValue);

        return map;
    }

    private static float[] SimulateDrop(float[] map, int mapSize, System.Random random, float inertia, float capacity, float deposition, float minCarryCapacity, float erosion, float gravity, float evaporation, float stuckSpeed, int nSteps, Brush brush)
    {

        int startX = random.Next(0, mapSize - 2);
        int startY = random.Next(0, mapSize - 2);

        //map[startX, startY] = 0;

        Vector2 oldPos = new(startX, startY);
        Vector2 pos = new(startX, startY);
        Vector2 dir = new(0, 1);

        float speed = 1;
        float water = 1;
        float sediment = 0;

        var heightAndGradient = CalculateHeightAndGradient(map, mapSize, pos);

        if (heightAndGradient.gradient.magnitude != 0)
            dir = -heightAndGradient.gradient.normalized;

        float oldHeight = heightAndGradient.height;


        float newHight = oldHeight;
        float heightDiff = 0;
        float carryCapacity;

        int nodeX;
        int nodeY;
        float cellOffsetX;
        float cellOffsetY;

        for (int i = 0; i < nSteps; i++)
        {
            // Drop is out of map
            if (pos.x < 0 || pos.x > mapSize - 2 || pos.y < 0 || pos.y > mapSize - 2)
                return map;

            nodeX = (int)pos.x;
            nodeY = (int)pos.y;

            int dropIndex = nodeX + nodeY * mapSize;

            cellOffsetX = pos.x - nodeX;
            cellOffsetY = pos.y - nodeY;

            heightAndGradient = CalculateHeightAndGradient(map, mapSize, pos);

            dir = dir * inertia - heightAndGradient.gradient * (1 - inertia);

            if (dir.magnitude == 0)
                return map;
            else
                dir.Normalize();

            pos += dir;

            // Drop is out of map
            if (pos.x < 0 || pos.x > mapSize - 2 || pos.y < 0 || pos.y > mapSize - 2)
                return map;


            newHight = CalculateHeightAndGradient(map, mapSize, pos).height;


            heightDiff = newHight - oldHeight;
         
            carryCapacity = Mathf.Max(-heightDiff * speed * water * capacity, minCarryCapacity);

            // deposit
            if (sediment > carryCapacity || heightDiff > 0)
            {
                float droppedSediment = (heightDiff > 0) ? Mathf.Min(heightDiff, sediment) : (sediment - carryCapacity) * deposition;
                sediment -= droppedSediment;

                // if (cellOffsetX < 0 || cellOffsetX > 1 || cellOffsetY < 0 || cellOffsetY > 1)
                // {
                //   Debug.Log(cellOffsetX);
                //   Debug.Log(cellOffsetY);
                // }
                map[dropIndex] += droppedSediment * (1 - cellOffsetX) * (1 - cellOffsetY);
                map[dropIndex + 1] += droppedSediment * cellOffsetX * (1 - cellOffsetY);
                map[dropIndex + mapSize] += droppedSediment * (1 - cellOffsetX) * cellOffsetY;
                map[dropIndex + 1 + mapSize] += droppedSediment * cellOffsetX * cellOffsetY;
            }
            // erode
            else
            {
                float takenSediment = Mathf.Min((carryCapacity - sediment) * erosion, -heightDiff);
                sediment += takenSediment;
                brush.AddValue(map, dropIndex, -takenSediment);
            }

            speed = Mathf.Sqrt(speed * speed + heightDiff * gravity);
            water *= 1 - evaporation;

            oldHeight = newHight;
        }

        return map;
    }

    private static HeightAndGradient CalculateHeightAndGradient(float[] map, int mapSize, Vector2 pos)
    {
        int nodeX = (int)pos.x;
        int nodeY = (int)pos.y;
        int dropIndex = nodeX + nodeY * mapSize;

        float a = pos.x - nodeX;
        float b = pos.y - nodeY;

        float height1 = map[dropIndex];
        float height2 = map[dropIndex + 1];
        float height3 = map[dropIndex + mapSize];
        float height4 = map[dropIndex + mapSize + 1];

        float height = height1 * (1 - a) * (1 - b) + height2 * a * (1 - b) + height3 * (1 - a) * b + height4 * a * b;

        var gradient = new Vector2();
        gradient.x = (height2 - height1) * (1 - b) + (height4 - height3) * b;
        gradient.y = (height3 - height1) * (1 - a) + (height4 - height2) * a;

        return new HeightAndGradient() { height = height, gradient = gradient };

    }

    struct HeightAndGradient
    {
        public float height;
        public Vector2 gradient;
    }
}





public class Brush
{
    public int[][] brushIndexes;
    public float[][] brushWeights;

    public void Initialize(int width, int height, int radius)
    {
        int mapLength = width * height;
        int kernalSize = 4 * radius * radius + 4 * radius + 1;

        brushIndexes = new int[mapLength][];
        brushWeights = new float[mapLength][];

        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                int mainIndex = x + y * width;
                int localIndex = 1;
                brushIndexes[mainIndex] = new int[kernalSize + 1];
                brushWeights[mainIndex] = new float[kernalSize];

                for (int i = -radius; i <= radius; i++)
                {
                    for (int j = -radius; j <= radius; j++)
                    {
                        float sqrDist = i * i + j * j;

                        if (sqrDist < radius * radius)
                        {
                            int cordX = x + j;
                            int cordY = y + i;

                            if (cordX >= 0 && cordY >= 0 && cordX < width && cordY < height)
                            {
                                brushIndexes[mainIndex][localIndex] = cordX + cordY * width;
                                brushWeights[mainIndex][localIndex] = 1.0f - Mathf.Sqrt(sqrDist) / radius; // /sum
                                localIndex++;
                            }
                        }

                    }
                }
                brushIndexes[mainIndex][0] = localIndex;
            }
        }

        //normalize weights
        for (int i = 0; i < mapLength; i++)
            ArrayMath.SetMagnetude(brushWeights[i], 1);
        
    }

    public void AddValue(float[] map, int index, float value)
    {
        for (int i = 1; i < brushIndexes[index][0]; i++)
        {
            int mapIndex = brushIndexes[index][i];
            map[mapIndex] += value * brushWeights[index][i-1];
        }
            
    }
}

