using System.Collections.Generic;

public class AStar
{
    public enum SearchState
    {
        None,
        Initialized,
        Searching,
        Success,
        Failure,
        OutOfMemory //TODO limit amount of memory that can be used
    }

    public SearchState State { get; private set; }

    public List<Node> Trace { get; private set; }

    private List<Node> openSet;
    private List<Node> closedSet;

    private Node startNode;
    private Node targetNode;

    public AStar()
    {
        State = SearchState.None;
    }

    public AStar(Node startNode, Node targetNode)
    {
        Initialize(startNode, targetNode);
    }

    public void Initialize(Node startNode, Node targetNode)
    {
        if (State != SearchState.None)
        {
            if (openSet != null)
                openSet.Clear();
            if (closedSet != null)
                closedSet.Clear();
        }

        openSet = new List<Node>();
        closedSet = new List<Node>();

        this.startNode = startNode;
        this.targetNode = targetNode;

        State = SearchState.Initialized;
    }

    public SearchState Iterate()
    {
        if (State == SearchState.Initialized)
        {
            openSet.Add(startNode);

            State = SearchState.Searching;
        }

        if (State != SearchState.Searching)
            return State;

        if(openSet.Count == 0)
        {
            State = SearchState.Failure;
            return State;
        }

        Node currentNode = openSet[0];
        for (int i = 1; i < openSet.Count; i++)
        {
            if (openSet[i].FCost < currentNode.FCost
                || (openSet[i].FCost == currentNode.FCost
                    && openSet[i].HCost < currentNode.HCost))
            {
                currentNode = openSet[i];
            }
        }

        openSet.Remove(currentNode);
        closedSet.Add(currentNode);

        if (currentNode == targetNode)
        {
            State = SearchState.Success;

            return State;
        }

        List<Node> successors = new List<Node>();
        currentNode.GetSuccessors(ref successors);
        foreach (Node successor in successors)
        {
            if (!closedSet.Contains(successor))
            {
                float costToConnection = currentNode.GCost + currentNode.State.GetEstimate(successor.State);

                if (costToConnection < successor.GCost || !openSet.Contains(successor))
                {
                    successor.GCost = costToConnection;
                    successor.HCost = targetNode.State.GetEstimate(successor.State);
                    successor.Parent = currentNode;

                    if (!openSet.Contains(successor))
                    {
                        openSet.Add(successor);
                    }
                }
            }
        }

        return State;
    }

    public List<Node> Retrace()
    {
        if (State != SearchState.Success)
            return null;

        List<Node> path = new List<Node>();

        Node currentNode = targetNode;

        while (currentNode != startNode)
        {
            path.Add(currentNode);
            currentNode = currentNode.Parent;
        }

        path.Reverse();

        return path;
    }
}
