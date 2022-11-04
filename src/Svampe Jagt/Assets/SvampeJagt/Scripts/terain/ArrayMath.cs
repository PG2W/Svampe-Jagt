using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ArrayMath
{
    static public float[] Normalize(float[] array)
    {
        float minValue = array[0];
        float maxValue = array[0];
        int length = array.GetLength(0);

        for (int i = 0; i < length; i++)
        {
            minValue = Mathf.Min(minValue, array[i]);
            maxValue = Mathf.Max(maxValue, array[i]);
        }

        if (minValue != maxValue)
            for (int i = 0; i < length; i++)
                array[i] = Mathf.InverseLerp(minValue, maxValue, array[i]);

        return array;
    }

    static public float[] CalculateStepnes(float[] array, int width, int height)
    {
        float[] stepnesArray = new float[array.GetLength(0)];
        int index;

        //calculate stepness for mid of array
        for (int y = 0; y < height - 1; y++)
        {
            for (int x = 0; x < width - 1; x++)
            {
                index = x + y * width;
                stepnesArray[index] = CalculateSlope(array, index, width) * 10;
            }
        }

        //set stepnes for borders
        for (int i = 0; i < width; i++)
        {
            stepnesArray[width - 1 + i * width] = stepnesArray[width - 2 + i * width];
            stepnesArray[i + (height - 1) * width] = stepnesArray[i + (height - 2) * width];
        }

        //last corner
        stepnesArray[array.GetLength(0) - 1] = stepnesArray[array.GetLength(0) - width - 1];

        return stepnesArray;
    }

    static private float CalculateSlope(float[] array, int index, int width)
    {
        float height1 = array[index];
        float height2 = array[index + 1];
        float height3 = array[index + width];
        float height4 = array[index + width + 1];

        var gradient = new Vector2();
        gradient.x = (height2 - height1 + height4 - height3) * 0.5f;
        gradient.y = (height3 - height1 + height4 - height2) * 0.5f;

        float slope = gradient.magnitude;

        return slope;
    }

    static public float[] BlurArray(float[] array, int radius, int width, int height)
    {
        int length = width * height;
        int blurWidth = 2 * radius + width;
        float[] tempArray = new float[length];
  
        var blurData = CalcBlurData(array, width, height, radius);

        for (int i = 0; i < length; i++)
        {
            for (int j = 1; j < blurData.indexes[i][0]; j++)
            {
                int index = blurData.indexes[i][j];
                tempArray[i] += array[index] * blurData.weights[i][j - 1];
            }
        }

        return tempArray;
    }

    static private BlurData CalcBlurData(float[] map, int width, int height, int radius)
    {
        int mapLength = map.GetLength(0);
        int[][] blurIndexes = new int[mapLength][];
        float[][] weightValues = new float[mapLength][];

        int kernalWidth = radius * 2 + 1;
        int kernalSize = 4 * radius * radius + 4 * radius + 1;


        float sum = 0;
        int sqrRadius = radius * radius;
        for (int i = -radius; i <= radius; i++)
        {
            for (int j = -radius; j <= radius; j++)
            {
                float sqrDist = i * i + j * j;

                if (sqrDist < sqrRadius)
                    sum += 1.0f - Mathf.Sqrt(sqrDist) / radius;
            }
        }

        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                int mainIndex = x + y * width;
                int localIndex = 1;
                blurIndexes[mainIndex] = new int[kernalSize + 1];
                weightValues[mainIndex] = new float[kernalSize];

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
                                blurIndexes[mainIndex][localIndex] = cordX + cordY * width;
                                weightValues[mainIndex][localIndex] = (1.0f - Mathf.Sqrt(sqrDist) / radius); // /sum
                                localIndex++;
                            }
                        }

                    }
                }
                blurIndexes[mainIndex][0] = localIndex;
            }

        }

        //normalize weights
        for (int i = 0; i < mapLength; i++)
        {
            SetMagnetude(weightValues[i], 1);
        }

        var blurData = new BlurData();
        blurData.indexes = blurIndexes;
        blurData.weights = weightValues;

        return blurData;
    }

    public static void SetMagnetude(float[] array, int magnitude)
    {
        int length = array.GetLength(0);
        float sum = 0;
        for (int i = 0; i < length; i++)
            sum += array[i];

        for (int i = 0; i < length; i++)
            array[i] *= (float) magnitude / sum;        
    }

    public static void ScaleArray(float[] array, float scaler)
    {
        for (int i = 0; i < array.GetLength(0); i++)
            array[i] *= scaler;
    }

    public static float[] CombineArrays(float[] a, float[] b, float slider)
    {
        int length = a.GetLength(0);
        var result = new float[length];

        if (slider < 0 || slider > 1)
            Debug.LogError("Slider value must be between 0 and 1 in (CombineArrays)");
        
        for (int i = 0; i < length; i++)
            result[i] = a[i] * (1 - slider) + b[i] * slider;
        
        return result;
    }

    private struct BlurData
    {
        public int[][] indexes;
        public float[][] weights;
    }
}

