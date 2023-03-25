using UnityEngine;
using UnityEngine.Tilemaps;

namespace PlatformerMVC
{
    public class GeneratorLevelView : MonoBehaviour
    {
        public Tilemap tilemap;
        public Tile tile;
        public int mapHeight;
        public int mapWidth;

        [Range(0, 100)] public int fillPercent;
        [Range(0, 100)] public int smoothPercent;

        public bool borders;
    }
}