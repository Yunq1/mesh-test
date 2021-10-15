using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{

    public enum DrawMode {NoiseMap, ColourMap, Mesh, Border};
    public DrawMode drawMode;

    const int mapChunkSize = 241;
    [Range(0,6)]
    public int levelDetail;
    public float noiseScale;
    public int ocatves;
    [Range(0,1)]
    public float persistance;
    public float lacunarity;
    public int seed;
    public Vector2 offset;

    public bool useBorder;

    public float meshHeightMultiplier;
    public AnimationCurve meshHeightCurve;

    public bool autoUpdate;

    public TerrainType[] regions;

    float[,] border;

    public List<GameObject> objSpwan = new List<GameObject>();

    void borderCall()
    {
        border = Border.GenerateBorder(mapChunkSize);
    }
    private void Start()
    {
        GenerateMap();
   //     GameObject randomObj = objSpwan[Random.Range(0, objSpwan.Count)];
      //  Instantiate(randomObj, transform.position, Quaternion.identity);
    }
    private void Update()
    {
        
    }
    public void GenerateMap()
    {
        float[,] noiseMap = Noise.GenerateNoiseMap(mapChunkSize, mapChunkSize, seed, noiseScale, ocatves, persistance, lacunarity, offset);

        Color[] colourMap = new Color[mapChunkSize * mapChunkSize];
        for (int y = 0; y < mapChunkSize; y++)
        {
            for (int x = 0; x < mapChunkSize; x++)
            {
                if (useBorder)
                {
                    noiseMap[x, y] = Mathf.Clamp01(noiseMap[x, y] - border[x, y]);
                }
                float currentHeight = noiseMap[x, y];
                for (int i = 0; i < regions.Length; i++)
                {
                    if (currentHeight <= regions[i].height)
                    {
                        colourMap[y * mapChunkSize + x] = regions[i].color;
                        break;
                    }
                }
            }
        }

        MapDisplay display = FindObjectOfType<MapDisplay>();
        if (drawMode == DrawMode.NoiseMap) 
        {
            display.DrawTexture(TextureGenerator.TextureHeightMap(noiseMap)); 
        }
        else if (drawMode == DrawMode.ColourMap) {
            display.DrawTexture(TextureGenerator.TextureColourMap(colourMap, mapChunkSize, mapChunkSize)); 
        }
        else if (drawMode == DrawMode.Mesh)
        {
            display.DrawMesh(MeshGenerator.GenerateTerrainMesh(noiseMap, meshHeightMultiplier, meshHeightCurve, levelDetail), TextureGenerator.TextureColourMap(colourMap, mapChunkSize, mapChunkSize));
        }
        else if (drawMode == DrawMode.Border)
        {
            display.DrawTexture(TextureGenerator.TextureHeightMap(Border.GenerateBorder(mapChunkSize)));
        }

    }

    private void OnValidate()
    {
        if (lacunarity < 1)
        {
            lacunarity = 1;
        }
        if (ocatves < 0)
        {
            ocatves = 0;
        }
        border = Border.GenerateBorder(mapChunkSize);
    }
}

[System.Serializable]
public struct TerrainType
{
    public string name;
    public float height;
    public Color color;
}