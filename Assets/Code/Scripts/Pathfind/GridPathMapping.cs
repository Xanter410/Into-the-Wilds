using Tools.Singleton;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace IntoTheWilds.AI
{
    public class GridNode
    {
        public Vector2Int position;
        public bool isWalkable;
        public bool[] isWalkableSides; // ������������ ������: [Top, Right, Bottom, Left]

        public GridNode(Vector2Int position, bool isWalkable, bool[] isWalkableSides)
        {
            this.position = position;
            this.isWalkable = isWalkable;
            this.isWalkableSides = isWalkableSides;
        }
    }

    public class GridPathMapping : Singleton<GridPathMapping>
    {
        public Tilemap[] Tilemaps; // ������ Tilemap, ������� ��������� � �������� �����
        public float TileSize = 1f; // ������ ������ ����� � Unity Units

        public GridNode[,] Grid { get; private set; } // ��������� ������ ����� �����
        public Vector2Int GridSize { get; private set; } // ������� ������������ �����
        public Vector2Int GridOrigin { get; private set; } // ������ ������������ ����� � ����������� Tilemap

        void Start()
        {
            InitializeGrid();
        }

        void InitializeGrid()
        {
            if (Tilemaps == null || Tilemaps.Length == 0)
            {
                Debug.LogError("�� ������� Tilemap ��� ���������� �����!");
                return;
            }

            // 1. ���������� ����� ������� ���� Tilemap
            BoundsInt combinedBounds = CalculateCombinedBounds();

            GridSize = new Vector2Int(combinedBounds.size.x, combinedBounds.size.y);
            GridOrigin = (Vector2Int)combinedBounds.min;
            Grid = new GridNode[GridSize.x, GridSize.y];

            // 2. �������� �� ���� ������� ������������ �����
            for (int x = 0; x < GridSize.x; x++)
            {
                for (int y = 0; y < GridSize.y; y++)
                {
                    Vector2Int cellPosition = new(GridOrigin.x + x, GridOrigin.y + y);

                    bool isWalkable = false;
                    bool[] isWalkableSides = new bool[] { false, false, false, false };

                    foreach (Tilemap tilemap in Tilemaps)
                    {
                        TileBase tile = tilemap.GetTile((Vector3Int)cellPosition);
                        if (tile != null)
                        {
                            isWalkable = true;
                            bool[] tileWalkableSides = GetWalkableSides(tile);

                            for (int i = 0; i < 4; i++)
                            {
                                isWalkableSides[i] = tileWalkableSides[i];
                            }
                        }
                    }

                    Grid[x, y] = new GridNode(new Vector2Int(x, y), isWalkable, isWalkableSides);
                }
            }
        }

        private BoundsInt CalculateCombinedBounds()
        {
            BoundsInt combinedBounds = Tilemaps[0].cellBounds;

            foreach (Tilemap tilemap in Tilemaps)
            {
                combinedBounds.xMin = Mathf.Min(combinedBounds.xMin, tilemap.cellBounds.xMin);
                combinedBounds.yMin = Mathf.Min(combinedBounds.yMin, tilemap.cellBounds.yMin);
                combinedBounds.xMax = Mathf.Max(combinedBounds.xMax, tilemap.cellBounds.xMax);
                combinedBounds.yMax = Mathf.Max(combinedBounds.yMax, tilemap.cellBounds.yMax);
            }

            return combinedBounds;
        }

        bool[] GetWalkableSides(TileBase tile)
        {
            string tileName = tile.name;

            bool[] walkableSides = new bool[4];

            // ���������� ������� ��� ���������� ������ � ������������ �� ����� �����
            // ������ �����: "Tile_T1R0B1L0"
            // T1 - Top (������� ������� ���������)
            // R0 - Right (������ ������� �����������)

            if (tileName.Contains("T1")) walkableSides[0] = true;
            if (tileName.Contains("R1")) walkableSides[1] = true;
            if (tileName.Contains("B1")) walkableSides[2] = true;
            if (tileName.Contains("L1")) walkableSides[3] = true;

            return walkableSides;
        }

        // ������������ ����� � ��������� ��� �������
        void OnDrawGizmos()
        {
            if (Grid != null && Tilemaps != null && Tilemaps.Length != 0)
            {
                for (int x = 0; x < GridSize.x; x++)
                {
                    for (int y = 0; y < GridSize.y; y++)
                    {
                        GridNode node = Grid[x, y];
                        if (node != null)
                        {
                            Vector2 nodeCenter = Tilemaps[0].CellToWorld(new Vector3Int(GridOrigin.x + x, GridOrigin.y + y, 0)) + (0.5f * TileSize * Vector3.one);

                            if (node.isWalkable)
                            {
                                Gizmos.color = Color.green;
                                Gizmos.DrawWireCube(nodeCenter, 0.9f * TileSize * Vector3.one);
                            }
                            else
                            {
                                Gizmos.color = Color.red;
                                Gizmos.DrawCube(nodeCenter, 0.9f * TileSize * Vector3.one);
                            }

                            DrawTileSides(node, nodeCenter);
                        }
                    }
                }
            }
        }

        void DrawTileSides(GridNode node, Vector3 nodeCenter)
        {
            float sideLength = TileSize * 0.9f;

            if (node.isWalkableSides[0])
            {
                Gizmos.color = Color.green;
            }
            else
            {
                Gizmos.color = Color.red;
            }

            Gizmos.DrawLine(nodeCenter + (0.5f * TileSize * Vector3.up),
                            nodeCenter + (0.5f * TileSize * Vector3.up) + (Vector3.right * sideLength));

            // ������ ������� (Right)
            if (node.isWalkableSides[1])
            {
                Gizmos.color = Color.green;
            }
            else
            {
                Gizmos.color = Color.red;
            }

            Gizmos.DrawLine(nodeCenter + (0.5f * TileSize * Vector3.right),
                            nodeCenter + (0.5f * TileSize * Vector3.right) + (Vector3.up * sideLength));

            if (node.isWalkableSides[2])
            {
                Gizmos.color = Color.green;
            }
            else
            {
                Gizmos.color = Color.red;
            }

            Gizmos.DrawLine(nodeCenter + (0.5f * TileSize * Vector3.down),
                            nodeCenter + (0.5f * TileSize * Vector3.down) + (Vector3.right * sideLength));

            if (node.isWalkableSides[3])
            {
                Gizmos.color = Color.green;
            }
            else
            {
                Gizmos.color = Color.red;
            }

            Gizmos.DrawLine(nodeCenter + (0.5f * TileSize * Vector3.left),
                            nodeCenter + (0.5f * TileSize * Vector3.left) + (Vector3.up * sideLength));
        }
    }
}
