using System.Collections.Generic;
using UnityEngine;
//using static MultiTilemapPathfinding;

namespace IntoTheWilds.AI
{
    static public class Pathfinding
    {
        // Класс для хранения информации о ячейке во время работы A*
        private class PathNode
        {
            public GridNode node;
            public PathNode parent;
            public float gCost;       // Цена пути от старта до этого узла
            public float hCost;       // Гипотетическая цена пути до цели
            public float FCost => gCost + hCost; // Общая стоимость
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
                Debug.LogWarning("Невозможно найти путь: стартовая или конечная точка непроходима.");
                return null;
            }

            List<PathNode> openList = new();
            HashSet<GridNode> closedList = new();

            PathNode startPathNode = new() { node = startNode, gCost = 0, hCost = GetHeuristic(startNode, targetNode) };
            openList.Add(startPathNode);

            while (openList.Count > 0)
            {
                // Ищем узел с наименьшей стоимостью F в открытом списке
                PathNode currentNode = GetNodeWithLowestFCost(openList);

                // Если мы достигли целевого узла, формируем путь
                if (currentNode.node == targetNode)
                {
                    return RetracePath(GridMap, currentNode);
                }

                // Перемещаем текущий узел из открытого списка в закрытый
                _ = openList.Remove(currentNode);
                _ = closedList.Add(currentNode.node);

                // Проверяем соседей текущего узла
                foreach (GridNode neighbor in GetNeighbors(GridMap, currentNode.node))
                {
                    if (closedList.Contains(neighbor) || !neighbor.isWalkable)
                    {
                        continue;
                    }

                    // Рассчитываем новую стоимость пути до соседа
                    float newGCost = currentNode.gCost + Vector2.Distance(currentNode.node.position, neighbor.position);

                    // Проверяем, находится ли сосед в открытом списке
                    PathNode neighborPathNode = openList.Find(n => n.node == neighbor);
                    if (neighborPathNode == null)
                    {
                        // Если соседа нет в открытом списке, добавляем его
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
                        // Если найден более короткий путь до соседа, обновляем его
                        neighborPathNode.gCost = newGCost;
                        neighborPathNode.parent = currentNode;
                    }
                }
            }

            // Если путь не найден, возвращаем null
            Debug.LogWarning("Путь не найден!");
            return null;
        }

        private static List<GridNode> GetNeighbors(GridPathMapping GridMap, GridNode node)
        {
            List<GridNode> neighbors = new();

            // Смещения для проверки соседей (вверх, вправо, вниз, влево)
            Vector2Int[] directions = { Vector2Int.up, Vector2Int.right, Vector2Int.down, Vector2Int.left };

            // Таблица соответствия направления для соседа. Если сосед вверху, то для него наш тайл внизу (DirectionNeighborMap[0] = 2)
            // Направления аналогичны массиву directions 0 - вверх, 1 - вправо, 2 - вниз, 3 - влево;
            int[] DirectionNeighborMap = { 2, 3, 0, 1 };

            for (int i = 0; i <= 3; i++)
            {
                // Проверка можно ли из текущего тайла можно перейти в данном направлении 
                if (node.isWalkableSides[i] == true)
                {
                    Vector2Int neighborCoords = node.position + directions[i];

                    //Проверяем, находится ли сосед в пределах сетки
                    if (neighborCoords.x >= 0 && neighborCoords.x < GridMap.GridSize.x &&
                        neighborCoords.y >= 0 && neighborCoords.y < GridMap.GridSize.y)
                    {
                        GridNode neighborNode = GridMap.Grid[neighborCoords.x, neighborCoords.y];

                        //Проверка, можно ли из соседа перейти в текущий тайл
                        if (neighborNode.isWalkableSides[DirectionNeighborMap[i]] == true)
                        {
                            neighbors.Add(neighborNode);
                        }
                    }
                }
            }

            return neighbors;
        }

        // Метод для пересчёта пути (собираем путь, начиная с целевого узла)
        private static List<Vector2> RetracePath(GridPathMapping GridMap, PathNode endNode)
        {
            List<Vector2> path = new();
            PathNode currentNode = endNode;

            while (currentNode != null)
            {
                // Добавляем позиции узлов в путь
                Vector3 worldPosition = GridToWorldCoords(GridMap, currentNode.node.position);
                path.Add(worldPosition);
                currentNode = currentNode.parent;
            }

            path.Reverse(); // Переворачиваем путь, чтобы он шёл от старта к цели
            return path;
        }

        // Метод для нахождения узла с наименьшей стоимостью F
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

        // Метод для эвристики
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

