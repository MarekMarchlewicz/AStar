﻿using UnityEngine;
using System.Collections.Generic;

namespace Pathfinding
{
    [System.Serializable]
    public class Grid2D
    {
        [SerializeField] private int sizeX;
        [SerializeField] private int sizeY;
        [SerializeField] private List<Node> nodes;

        public static Grid2D Instance { get; private set; }

        public Grid2D(int gridSizeX, int gridSizeY)
        {
            sizeX = gridSizeX;
            sizeY = gridSizeY;

            nodes = new List<Node>();

            for (int y = 0; y < sizeY; y++)
            {
                for (int x = 0; x < sizeX; x++)
                {
                    PathState pathState = new PathState(x, y, TerrainType.Grass, true);
                    Node newNode = new Node(pathState);

                    nodes.Add(newNode);
                }
            }

            Instance = this;
        }

        public List<Node> GetNodes()
        {
            return nodes;
        }

        public List<Node> GetConnections(PathState pathState)
        {
            List<Node> connections = new List<Node>();

            int xPosition;
            int yPosition;

            for (int x = -1; x <= 1; x++)
            {
                for (int y = -1; y <= 1; y++)
                {
                    if (!( x == 0 && y == 0))
                    {
                        xPosition = pathState.X + x;
                        yPosition = pathState.Y + y;

                        if (AssetWithinBorders(xPosition, yPosition))
                        {
                            connections.Add(GetNodeFromPosition(xPosition, yPosition));
                        }
                    }
                }
            }

            return connections;
        }

        public bool AssetWithinBorders(int x, int y)
        {
            return x >= 0 && x < sizeX && y >= 0 && y < sizeY;
        }

        public Node GetNodeFromPosition(int x, int y)
        {
            return nodes[x + y * sizeX];
        }
    }
}
