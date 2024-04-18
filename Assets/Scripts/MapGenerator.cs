using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    [SerializeField] private Material m_materialTerrain;

    [Header("Map parameters")]
    [SerializeField] private int m_mapHeight =100;
    private int width = 1024;
    private int height = 1024;
    public int seed = 10;
    public float scale1 = 10;
    public float scale2 = 10;
    public float scale3 = 10;
    public bool activePower;
    public float fudgeFactor = 1.2f;
    public float power = 2;
    public bool activeStep;
    public int step = 20;

    private bool isDone = false;
    private bool m_isTerrainCreated;
    private const int m_heightMapResolution = 1025;

    private GameObject m_terrainGO;
    private Terrain m_terrainComponent;
    private MapDisplay m_displayComponent;

    public void RandomMap()
    {
        seed = Random.Range(0, int.MaxValue);
        GenerateMap();
    }

    public void GenerateMap()
    {
        if (m_isTerrainCreated && m_terrainGO == null)
            m_isTerrainCreated = false;

        if(!m_isTerrainCreated)
            CreateTerrain();
        float[,] perlinValue = Noise.GenerateNoiseMap(CreateNoiseParameter());
        m_displayComponent.GenerateTex(m_terrainGO, perlinValue,width,height);
        GenerateMapTopography(perlinValue);
    }

    private void CreateTerrain()
    {
        // Create a new terrain data
        TerrainData _terrainData = new TerrainData();
        _terrainData.heightmapResolution = m_heightMapResolution;

        m_terrainGO = Terrain.CreateTerrainGameObject(_terrainData);

        m_terrainComponent = m_terrainGO.GetComponent<Terrain>();
        m_terrainComponent.materialTemplate = m_materialTerrain;
        m_terrainComponent.terrainData.size = new Vector3(width, m_mapHeight, height);

        m_displayComponent =  GetComponent<MapDisplay>();
        m_isTerrainCreated = true;
    }

   
    private void GenerateMapTopography(float [,] map)
    {
        m_terrainComponent.terrainData.size = new Vector3(width, m_mapHeight, height);
        m_terrainComponent.terrainData.SetHeights(0,0, map);
    }

    private void Start()
    {
        GenerateMap();
    }



    private Noise.NoiseParemeters CreateNoiseParameter()
    {
        Noise.NoiseParemeters noiseParameters = new Noise.NoiseParemeters();
        noiseParameters.width = width;
        noiseParameters.height = height;
        noiseParameters.seed = seed;
        noiseParameters.scale1 = scale1;
        noiseParameters.scale2 = scale2;
        noiseParameters.scale3 = scale3;
        noiseParameters.activePowerMode = activePower;
        noiseParameters.power =power;
        noiseParameters.fudge_factor =fudgeFactor;
        noiseParameters.activeStep = activeStep;
        noiseParameters.step = step;

        return noiseParameters;
    }
}
