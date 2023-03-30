using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;

namespace PlatformerMVC
{
    public class MarshingSquareController
    {
        private Tilemap _tilemap;
        private Tile _tile;
        private SquareGrid _grid;

        public void GenerateGrid(int[,] map, float squareSize)
        {
            _grid = new SquareGrid(map, squareSize);
        }

        public void DrawTiles(bool active, Vector3 position)
        {
            if (active)
            {
                Vector3Int tilePosition = new Vector3Int((int)position.x, (int)position.y, 0);
                _tilemap.SetTile(tilePosition, _tile);
            }
        }

        public void DrawTiles(Tilemap tilemapG, Tile ground)
        {
            if (_grid == null) return;

            _tile = ground;
            _tilemap = tilemapG;

            for (int x = 0; x < _grid.Squares.GetLength(0); x++)
            {
                for (int y = 0; y < _grid.Squares.GetLength(1); y++)
                {
                    DrawTiles(_grid.Squares[x, y].TL.Active, _grid.Squares[x, y].TL.Position);
                    DrawTiles(_grid.Squares[x, y].TR.Active, _grid.Squares[x, y].TR.Position);
                    DrawTiles(_grid.Squares[x, y].BL.Active, _grid.Squares[x, y].BL.Position);
                    DrawTiles(_grid.Squares[x, y].BR.Active, _grid.Squares[x, y].BR.Position);
                    
                }

            }
        }
    }

    public class Node
    {
        public Vector3 Position;

        public Node(Vector3 position)
        {
            Position = position;
        }
    }

    public class ControlNode : Node
    {
        public bool Active;

        public ControlNode(Vector3 position, bool active) : base(position)
        {
            Active = active;
        }
    }

    public class Square
    {
        public ControlNode TL, TR, BL, BR;

        public Square(ControlNode tL, ControlNode tR, ControlNode bL, ControlNode bR)
        {
            TL = tL;
            TR = tR;
            BL = bL;
            BR = bR;
        }
    }

    public class SquareGrid
    {
        public Square[,] Squares;

        public SquareGrid(int[,] map, float squareSize)
        {
            int nodeCountX = map.GetLength(0);
            int nodeCountY = map.GetLength(1);

            float mapWidth = nodeCountX + squareSize;
            float mapHeight = nodeCountY + squareSize;

            float squareSizeHalf = squareSize / 2f;

            float widthHalf = -mapWidth / 2f;
            float heightHalf = -mapHeight / 2f;

            ControlNode[,] controlNodes = new ControlNode[nodeCountX, nodeCountY];

            for (int x = 0; x < nodeCountX; x++)
            {
                for (int y = 0; y < nodeCountY; y++)
                {
                    Vector3 position = new Vector3(
                        widthHalf + x * squareSize + squareSizeHalf,
                        heightHalf + y * squareSize + squareSizeHalf, 0);
                    controlNodes[x, y] = new ControlNode(position, map[x, y] == 1);
                }
            }

            Squares = new Square[nodeCountX-1, nodeCountY-1];

            for (int x = 0; x < nodeCountX - 1; x++)
            {
                for (int y = 0; y < nodeCountY - 1; y++)
                {
                    Squares[x, y] = new Square(
                        controlNodes[x, y + 1],
                        controlNodes[x + 1, y],
                        controlNodes[x + 1, y + 1],
                        controlNodes[x, y]
                    );
                }
            }
        }
    }
}