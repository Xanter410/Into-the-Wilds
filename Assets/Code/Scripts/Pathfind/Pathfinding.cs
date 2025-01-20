using System.Collections.Generic;
using UnityEngine;
//using static MultiTilemapPathfinding;

namespace IntoTheWilds.AI
{
    static public class Pathfinding
    {
        // ����� ��� �������� ���������� � ������ �� ����� ������ A*
        private class PathNode
        {
            public GridNode node;
            public PathNode parent;
            public float gCost;       // ���� ���� �� ������ �� ����� ����
            public float hCost;       // �������������� ���� ���� �� ����
            public float FCost => gCost + hCost; // ����� ���������
        }

        public static List<Vector2> FindPath(Vector2 startPosition, Vector2 targetPosition)
        {
            var GridMap = GridPathMapping.Instance;

            Vector2Int startCoords = WorldToGridCoords(GridMap, startPosition);
            Vector2Int targetCoords = WorldToGridCoords(GridMap, targetPosition);

            GridNode startNode = GridMap.Grid[startCoords.x, startCoords.y];
            GridNode targetNode = GridMap.Grid[targetCoords.x, targetCoords.y];

            if (!startNode.isWalkable || !targetNode.isWalkable)
            {
                Debug.LogWarning("���������� ����� ����: ��������� ��� �������� ����� �����������.");
                return null;
            }

            List<PathNode> openList = new();
            HashSet<GridNode> closedList = new();

            PathNode startPathNode = new() { node = startNode, gCost = 0, hCost = GetHeuristic(startNode, targetNode) };
            openList.Add(startPathNode);

            while (openList.Count > 0)
            {
                // ���� ���� � ���������� ���������� F � �������� ������
                PathNode currentNode = GetNodeWithLowestFCost(openList);

                // ���� �� �������� �������� ����, ��������� ����
                if (currentNode.node == targetNode)
                {
                    return RetracePath(GridMap, currentNode);
                }

                // ���������� ������� ���� �� ��������� ������ � ��������
                _ = openList.Remove(currentNode);
                _ = closedList.Add(currentNode.node);

                // ��������� ������� �������� ����
                foreach (GridNode neighbor in GetNeighbors(GridMap, currentNode.node))
                {
                    if (closedList.Contains(neighbor) || !neighbor.isWalkable)
                    {
                        continue;
                    }

                    // ������������ ����� ��������� ���� �� ������
                    float newGCost = currentNode.gCost + Vector2.Distance(currentNode.node.position, neighbor.position);

                    // ���������, ��������� �� ����� � �������� ������
                    PathNode neighborPathNode = openList.Find(n => n.node == neighbor);
                    if (neighborPathNode == null)
                    {
                        // ���� ������ ��� � �������� ������, ��������� ���
                        neighborPathNode = new PathNode
                        {
                            node = neighbor,
                            parent = currentNode,
                            gCost = newGCost,
                            hCost = GetHeuristic(neighbor, targetNode)
                        };
                        openList.Add(neighborPathNode);
                    }
                    else if (newGCost < neighborPathNode.gCost)
                    {
                        // ���� ������ ����� �������� ���� �� ������, ��������� ���
                        neighborPathNode.gCost = newGCost;
                        neighborPathNode.parent = currentNode;
                    }
                }
            }

            // ���� ���� �� ������, ���������� null
            Debug.LogWarning("���� �� ������!");
            return null;
        }

        private static List<GridNode> GetNeighbors(GridPathMapping GridMap, GridNode node)
        {
            List<GridNode> neighbors = new();

            // �������� ��� �������� ������� (�����, ������, ����, �����)
            Vector2Int[] directions = { Vector2Int.up, Vector2Int.right, Vector2Int.down, Vector2Int.left };

            // ������� ������������ ����������� ��� ������. ���� ����� ������, �� ��� ���� ��� ���� ����� (DirectionNeighborMap[0] = 2)
            // ����������� ���������� ������� directions 0 - �����, 1 - ������, 2 - ����, 3 - �����;
            int[] DirectionNeighborMap = { 2, 3, 0, 1 };

            for (int i = 0; i <= 3; i++)
            {
                // �������� ����� �� �� �������� ����� ����� ������� � ������ ����������� 
                if (node.isWalkableSides[i] == true)
                {
                    Vector2Int neighborCoords = node.position + directions[i];

                    //���������, ��������� �� ����� � �������� �����
                    if (neighborCoords.x >= 0 && neighborCoords.x < GridMap.GridSize.x &&
                        neighborCoords.y >= 0 && neighborCoords.y < GridMap.GridSize.y)
                    {
                        GridNode neighborNode = GridMap.Grid[neighborCoords.x, neighborCoords.y];

                        //��������, ����� �� �� ������ ������� � ������� ����
                        if (neighborNode.isWalkableSides[DirectionNeighborMap[i]] == true)
                        {
                            neighbors.Add(neighborNode);
                        }
                    }
                }
            }

            return neighbors;
        }

        // ����� ��� ��������� ���� (�������� ����, ������� � �������� ����)
        private static List<Vector2> RetracePath(GridPathMapping GridMap, PathNode endNode)
        {
            List<Vector2> path = new();
            PathNode currentNode = endNode;

            while (currentNode != null)
            {
                // ��������� ������� ����� � ����
                Vector3 worldPosition = GridToWorldCoords(GridMap, currentNode.node.position);
                path.Add(worldPosition);
                currentNode = currentNode.parent;
            }

            path.Reverse(); // �������������� ����, ����� �� ��� �� ������ � ����
            return path;
        }

        // ����� ��� ���������� ���� � ���������� ���������� F
        private static PathNode GetNodeWithLowestFCost(List<PathNode> nodes)
        {
            PathNode lowestFCostNode = nodes[0];
            foreach (PathNode node in nodes)
            {
                if (node.FCost < lowestFCostNode.FCost)
                {
                    lowestFCostNode = node;
                }
            }

            return lowestFCostNode;
        }

        // ����� ��� ���������
        private static float GetHeuristic(GridNode fromNode, GridNode toNode)
        {
            return Vector2.Distance(fromNode.position, toNode.position);
        }

        private static Vector2Int WorldToGridCoords(GridPathMapping GridMap, Vector3 worldPosition)
        {
            Vector3Int cellCoords = GridMap.Tilemaps[0].WorldToCell(worldPosition);
            return new Vector2Int(cellCoords.x - GridMap.GridOrigin.x, cellCoords.y - GridMap.GridOrigin.y);
        }

        private static Vector2 GridToWorldCoords(GridPathMapping GridMap, Vector2Int gridCoords)
        {
            Vector3Int cellCoords = new(gridCoords.x + GridMap.GridOrigin.x, gridCoords.y + GridMap.GridOrigin.y, 0);
            return GridMap.Tilemaps[0].CellToWorld(cellCoords) + (0.5f * GridMap.TileSize * Vector3.one);
        }
    }
}

