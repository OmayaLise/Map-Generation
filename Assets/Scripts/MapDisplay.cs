using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapDisplay : MonoBehaviour
{
 
    public Gradient grdColor;

    private GameObject m_terrainGO;

    public float[,] GenerateTex(GameObject terrain , float[,] array , int width,int height )
    {
        m_terrainGO = terrain;   
        SetTerrainTexture(width, height, array);
        return array;

    }

    public void SetTerrainTexture(int width,int height, float[,] arrayData)
    {
        Texture2D texture2D = new Texture2D(width,height);
        for (int i = 0; i < height; i++)
        {
            for (int j = 0; j < width; j++)
            {
                float data = arrayData[j, i];
                
                Color color = grdColor.Evaluate(data);
                texture2D.SetPixel(i, j, color);
               
            }
        }
        texture2D.Apply();

        m_terrainGO.GetComponent<Terrain>().materialTemplate.SetTexture("_MainTex", texture2D);
        


    }

    
}
