using UnityEngine;
using System.Collections.Generic;
using Pathfinding;

public class GridManager : MonoBehaviour 
{
    private List<NodeVisual> visuals;

    [SerializeField] private int gridSizeX;
    [SerializeField] private int gridSizeY;

    [SerializeField] private float size = 1f;

    private readonly string pathLocation =  "NodeVisual";

    private static GridManager instance;
    public static GridManager Instance
    {
        get { return instance; }
    }

    private Grid grid;

    private List<Node> path;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        if (instance == this)
        {
            instance = null;
        }
    }

    private void Start()
    {
        GenerateGrid();
    }

    private void GenerateGrid()
    {
        visuals = new List<NodeVisual>(gridSizeX * gridSizeY);

        grid = new Grid(gridSizeX, gridSizeY);

        List<Node> gridNodes = grid.GetNodes();

        GameObject nodeVisual = Resources.Load<GameObject>(pathLocation);

        NodeVisual visual;

        for (int y = 0; y < gridSizeY; y++)
        {
            for (int x = 0; x < gridSizeX; x++)
            {
                GameObject newNodeVisual = Instantiate(nodeVisual, new Vector3(x * size, 0f, y * size), Quaternion.identity) as GameObject;

                visual = newNodeVisual.GetComponent<NodeVisual>();
                visuals.Add(visual);
                visual.Initialize(gridNodes[x + y * gridSizeX]);
            }
        }
    }

    public List<Node> GetPath(Node start, Node target)
    {
        return AStar.FindPath(start, target, grid);
    }

    public NodeVisual GetVisual(Node node)
    {
        return GetVisual(node.X, node.Y);
    }
       
    private NodeVisual GetVisual(int x, int y)
    {
        return visuals[x + y * gridSizeX];
    }

    public Node GetClosestNodeToWorldPosition(Vector3 position)
    {
        int x = Mathf.RoundToInt((position.x + size) / size) - 1;
        int y = Mathf.RoundToInt((position.z + size) / size) - 1;

        x = Mathf.Clamp(x, 0, gridSizeX - 1);
        y = Mathf.Clamp(y, 0, gridSizeY - 1);

        return grid.GetNodeFromPosition(x, y);
    }
        
    public Vector3 GetWorldPosition(Node node)
    {
        return new Vector3(node.X * size, 0f, node.Y * size);
    }
}
