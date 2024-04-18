using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Noise
{
    
    public struct NoiseParemeters
    {
        public int width;
        public int height;
        public int seed;
        public float scale1;
        public float scale2;
        public float scale3;
        public bool activePowerMode;
        public float power;
        public float fudge_factor;
        public bool activeStep;
        public int step;
    }

    public static float[,] GenerateNoiseMap(NoiseParemeters noiseParameter)
    {
        Random.InitState(noiseParameter.seed);
        float rnd = Random.Range(-noiseParameter.width, noiseParameter.width);
        float[,] array = new float[noiseParameter.width, noiseParameter.height];
        for (int i = 0; i < noiseParameter.height; i++)
        {
            for (int j = 0; j < noiseParameter.width; j++)
            {
               
                float xValue =  rnd + ( i / (float)noiseParameter.height) ;
                float yValue = rnd +  (j / (float)noiseParameter.width) ;

                array[i, j] =  1*Mathf.PerlinNoise(yValue * noiseParameter.scale1, xValue * noiseParameter.scale1)
                    +0.5f * Mathf.PerlinNoise(noiseParameter.scale2 * yValue, noiseParameter.scale2 * xValue)
                    +0.25f * Mathf.PerlinNoise(noiseParameter.scale3 * yValue, noiseParameter.scale3 * xValue);
                
                array[i, j] = array[i, j] / (1.0f + 0.5f + 0.25f);
                if(noiseParameter.activePowerMode)  array[i, j] = Mathf.Pow(array[i, j] * noiseParameter.fudge_factor, noiseParameter.power);
                if (noiseParameter.activeStep) array[i, j] = Mathf.Round(array[i, j] * noiseParameter.step) / noiseParameter.step;
            }
        }
        return array;
    }
}
