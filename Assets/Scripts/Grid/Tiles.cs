using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu]
public class Tiles : ScriptableObject 
{
    [System.Serializable]
    public class Tile
    {
        [Header("Tile")]
        public TerrainType terrainType;

        [Header("Color")]
        public Color color;

        [Range(0, 10)]
        public int cost;

        public bool walkable = true;
    }

    private static readonly string path = "Tiles";

    private static Tiles instance;

    private static Tiles Instance
    {
        get
        {
            if (instance == null)
            {
                instance = Resources.Load<Tiles>(path);
            }

            return instance;
        }
    }

    [SerializeField] private List<Tile> tiles;

    private void OnValidate()
    {
        if (tiles == null)
        {
            tiles = new List<Tile>();
        }

        int numberOfTilesToAdd = System.Enum.GetNames(typeof(TerrainType)).Length - tiles.Count;

        if (numberOfTilesToAdd > 0)
        {
            for (int i = 0; i < numberOfTilesToAdd; i++)
            {
                tiles.Add(new Tile());
            }
        }
        else
        {
            tiles.RemoveRange(tiles.Count + numberOfTilesToAdd, -numberOfTilesToAdd);
        }

        for (int i = 0; i < tiles.Count; i++)
        {
            tiles[i].terrainType = (TerrainType)i;
        }
    }

    public Color GetColorOfType(TerrainType terrainType)
    {
        return tiles[(int)terrainType].color;
    }

    public static Color GetColor(TerrainType terrainType)
    {
        return Instance.GetColorOfType(terrainType);
    }

    public int GetCostOfType(TerrainType terrainType)
    {
        return tiles[(int)terrainType].cost;
    }

    public static int GetCost(TerrainType terrainType)
    {
        return Instance.GetCostOfType(terrainType);
    }

    public bool Walkable(TerrainType terrainType)
    {
        return tiles[(int)terrainType].walkable;
    }

    public static bool IsWalkable(TerrainType terrainType)
    {
        return Instance.Walkable(terrainType);
    }
}
