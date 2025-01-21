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
    }
}
