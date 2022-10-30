using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Noise
{
  public static float[,] MakeNoiseMap(int width, int height, float scale, int seed,
                                   int nOctaves, float percistance, float lacranaraty, Vector2 offsett)
  {
    float[,] noiseMap = new float[width, height];
    float freq = lacranaraty;
    float amp = percistance;
    System.Random random = new System.Random(seed);

    float xOffset = random.Next(-100000, 100000) + offsett.x;
    float yOffset = random.Next(-100000, 100000) + offsett.y;

    for (int oct = 0; oct < nOctaves; oct++)
    {
      for (int z = 0; z < height; z++)
      {
        for (int x = 0; x < width; x++)
        {
          float xf = x * 1f;
          float zf = z * 1f;
          float y = Mathf.PerlinNoise(x * freq * scale + xOffset, z * freq * scale + yOffset) * amp;

          // float y = Mathf.PerlinNoise(xf, zf);

          noiseMap[x, z] += y;

        }
      }
      freq *= lacranaraty;
      amp *= percistance;
    }

    return noiseMap;
  }
}

