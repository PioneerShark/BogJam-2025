using UnityEngine;
using System;
using System.Collections;

public class MapGenerator : MonoBehaviour
{
    [SerializeField] int width;
    [SerializeField] int height;

    [SerializeField] int seed;
    [SerializeField] bool useRandomSeed;

    [SerializeField] bool restart;
    [SerializeField] int smoothingPasses;

    [Range(0, 100)] [SerializeField] int randomFillPercent;

    int[,] map;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GenerateMap();
    }

    // Update is called once per frame
    void Update()
    {
        if (restart)
        {
            restart = false;
            GenerateMap();
        }
    }
    void GenerateMap()
    {
        map = new int[width, height];
        RandomMapFill();
        for (int i = 0; i < smoothingPasses; i++)
        {
            SmoothMap();
        }
    }
    void SmoothMap()
    {
        int[,] tempMap = map;
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                int neighbourWallTiles = GetSurroundingWallCount(x, y);

                if (neighbourWallTiles > 4)
                {
                    tempMap[x, y] = 1;
                }
                else if (neighbourWallTiles < 4)
                {
                    tempMap[x, y] = 0;
                }
            }
        }
        map = tempMap;
    }
    void RandomMapFill()
    {
        if (useRandomSeed)
        {
            seed = (int)System.DateTime.Now.Ticks;
        }

        System.Random seededRandom = new System.Random(seed);

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                map[x, y] = (seededRandom.Next(0, 100) < randomFillPercent)? 1 : 0;
            }
        }
    }

    int GetSurroundingWallCount(int gridX, int gridY)
    {
        int wallCount = 0;
        for (int x = gridX-1; x <= gridX+1; x++)
        {
            for (int y = gridY-1; y <= gridY+1; y++)
            {
                if (x >= 0 && x < width && y >=0 && y < height)
                {
                    if (x != gridX || y != gridY)
                    {
                        wallCount += map[x, y];
                    }
                }
                else
                {
                    wallCount++;
                }
                
            }
        }
        return wallCount;

    }

    private void OnDrawGizmos()
    {
        if (map != null)
        {
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    Gizmos.color = (map[x, y] == 1) ? Color.black : Color.white;
                    Vector3 pos = new Vector3(-width / 2 + x + .5f, 0, -height / 2 + y + .5f);
                    Gizmos.DrawCube(pos, Vector3.one);
                }
            }
        }
    }
}
