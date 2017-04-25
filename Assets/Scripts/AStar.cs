using UnityEngine;
using System.Collections.Generic;

namespace Pathfinding
{
    public static class AStar 
    {
        static public List<Node> FindPath(Node startNode, Node targetNode, Grid grid) 
        {
            List<Node> openSet = new List<Node>();
            openSet.Add(startNode);

            List<Node> closedSet = new List<Node>();

            while (openSet.Count > 0) 
            {
                Node currentNode = openSet[0];
                for (int i = 1; i < openSet.Count; i ++) 
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
                    return RetracePath(startNode, targetNode);
                }

                foreach (Node connection in grid.GetConnections(currentNode)) 
                {
                    if (connection.Walkable && !closedSet.Contains(connection))
                    {
                        int costToConnection = currentNode.GCost + GetEstimate(currentNode, connection) + connection.Cost;

                        if (costToConnection < connection.GCost || !openSet.Contains(connection))
                        {
                            connection.GCost = costToConnection;
                            connection.HCost = GetEstimate(connection, targetNode);
                            connection.Parent = currentNode;

                            if (!openSet.Contains(connection))
                            {
                                openSet.Add(connection);
                            }
                        }
                    }
                }
            }

            return null;
        }

        private static List<Node> RetracePath(Node startNode, Node endNode) 
        {
            List<Node> path = new List<Node>();

            Node currentNode = endNode;

            while (currentNode != startNode) 
            {
                path.Add(currentNode);
                currentNode = currentNode.Parent;
            }

            path.Reverse();

            return path;
        }

        private static int GetEstimate(Node nodeA, Node nodeB) //Diagonal Shortcut
        {
            int distance;

            int xDistance = Mathf.Abs(nodeA.X - nodeB.X);
            int yDistance = Mathf.Abs(nodeA.Y - nodeB.Y);

            if (xDistance > yDistance)
            {
                distance = 14 * yDistance + 10 * (xDistance - yDistance);
            }
            else
            {
                distance = 14 * xDistance + 10 * (yDistance - xDistance);
            }

            return distance;
        }
    }
}
