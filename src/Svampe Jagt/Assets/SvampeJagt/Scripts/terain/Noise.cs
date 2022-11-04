using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Noise
{
    public static float[] MakeNoiseMap(int mapSize, float scale, int seed,
                                     int nOctaves, float percistance, float lacranaraty, Vector2 offsett)
    {
        float[] map = new float[mapSize * mapSize];
        float freq = lacranaraty;
        float amp = percistance;
        System.Random random = new System.Random(seed);

        float xOffset = random.Next(-100000, 100000) + offsett.x;
        float yOffset = random.Next(-100000, 100000) + offsett.y;

        float minValue = 999999f;
        float maxValue = -999999f;

        for (int oct = 0; oct < nOctaves; oct++)
        {
            for (int z = 0; z < mapSize; z++)
            {
                for (int x = 0; x < mapSize; x++)
                {
                    float y = Mathf.PerlinNoise(x * freq * scale + xOffset, z * freq * scale + yOffset) * amp;
                    minValue = Mathf.Min(minValue, y);
                    maxValue = Mathf.Max(maxValue, y);
                    map[x + z * mapSize] += y;

                }
            }
            freq *= lacranaraty;
            amp *= percistance;
        }

        map = ArrayMath.Normalize(map);
  
        return map;
    }
}

