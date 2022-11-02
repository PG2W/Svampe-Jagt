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
    brush.Initialize(mapSize, radius);

    for (int i = 0; i < iterations; i++)
    {
      map = SimulateDrop(map, mapSize, random, inertia, capacity, deposition, minCarryCapacity, erosion, gravaty, evaporation, stuckSpeed, nSteps, brush);
    }

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
      // }
      // catch (Exception ex)
      // {
      //   Debug.Log(newPos.x);
      //   Debug.Log(newPos.y);
      // }

      // if (heightDiff > 0)
      // {
      //   if (heightDiff < sediment)
      //   {
      //     map[(int)oldPos.x, (int)oldPos.y] += heightDiff;
      //     sediment -= heightDiff;
      //   }
      //   else
      //   {
      //     map[(int)oldPos.x, (int)oldPos.y] = sediment;
      //     sediment = 0;
      //   }
      // }
      // else
      // {
      //   carryCapacity = Mathf.Max(-heightDiff, minSlope) * speed * water * capacity;

      //   if (sediment > carryCapacity)
      //   {
      //     var droppedSediment = (sediment - carryCapacity) * deposition;
      //     map[(int)oldPos.x, (int)oldPos.y] += droppedSediment;
      //     sediment -= droppedSediment;
      //   }
      //   else
      //   {
      //     var takenSediment = Mathf.Min((carryCapacity - sediment) * erosion, -heightDiff);
      //     map[(int)oldPos.x, (int)oldPos.y] -= takenSediment;
      //     sediment += takenSediment;
      //   }
      // }

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
        // map = brush.SetValue(map, -takenSediment, dropIndex);
        map[dropIndex] -= takenSediment * (1 - cellOffsetX) * (1 - cellOffsetY);
        map[dropIndex + 1] -= takenSediment * cellOffsetX * (1 - cellOffsetY);
        map[dropIndex + mapSize] -= takenSediment * (1 - cellOffsetX) * cellOffsetY;
        map[dropIndex + 1 + mapSize] -= takenSediment * cellOffsetX * cellOffsetY;
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
  public int radius;

  public void Initialize(int mapSize, int radius)
  {
    int arrayLength = mapSize * mapSize;
    int indexSize = radius * radius * 4;
    brushIndexes = new int[mapSize * mapSize][];
    brushWeights = new float[mapSize * mapSize][];

    this.radius = radius;

    for (int i = 0; i < mapSize; i++)
    {
      for (int j = 0; j < mapSize; j++)
      {
        int localIndex = 0;
        int arrayIndex = i * mapSize + j;
        brushIndexes[arrayIndex] = new int[indexSize + 1];
        brushWeights[arrayIndex] = new float[indexSize];

        for (int y = -radius; y <= radius; y++)
        {
          for (int x = -radius; x <= radius; x++)
          {
            if (j + x < mapSize && i + y < mapSize && j + x >= 0 && i + y >= 0)
            {
              int currentIndex = arrayIndex + x + y * mapSize;
              float sqrDist = x * x + y * y;
              if (sqrDist < radius * radius)
              {
                brushIndexes[arrayIndex][localIndex + 1] = currentIndex;
                brushWeights[arrayIndex][localIndex] = 1.0f - Mathf.Sqrt(sqrDist) / radius;
                localIndex++;
              }
            }
          }
        }
        brushIndexes[arrayIndex][0] = localIndex;
      }
    }
  }

  public float[] SetValue(float[] map, float value, int midIndex)
  {
    int numberOfIndexes = brushIndexes[midIndex][0];
    for (int i = 0; i < numberOfIndexes; i++)
    {
      int index = brushIndexes[midIndex][i + 1];
      float weight = brushWeights[midIndex][i];

      map[index] += value * weight;
      // if (float.IsNaN(map[index]))
      //   Debug.Log(i);
    }

    return map;
  }
}

