using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class PathState : IState
{
    public int X { get; private set; }
    public int Y { get; private set; }

    public TerrainType TerrainType { get; set;} 

    public bool Walkable { get; set; }

    private Node node;

    public void Initialize(Node node)
    {
        this.node = node;
    }

    public PathState(int x, int y, TerrainType terrainType, bool isWalkable)
    {
        X = x;
        Y = y;
        TerrainType = terrainType;
        Walkable = isWalkable;
    }

    public float GetEstimate(IState toState)
    {
        PathState pathState = toState as PathState;
        return Vector2.Distance(new Vector2(X, Y), new Vector2(pathState.X, pathState.Y)) +GetCost();
    }

    private float GetCost()
    {
        return Tiles.GetCost(TerrainType);
    }

    public List<Node> GetSuccessors()
    {
        return Grid2D.Instance.GetConnections(this);
    }

    public Node GetNode()
    {
        return node;
    }
}
