using System;
using System.Drawing;

namespace WinFormsTest {
    class Dungeon {
        public TileType[,] DungoenTiles;
        long x, y;
        public int[] EntreCordinates = new int[2];

        public Dungeon(long x, long y, int ex, int ey) {
            this.x = x;
            this.y = y;
            CreateDungeon();

            EntreCordinates[1] = ex; EntreCordinates[2] = ey;
        }

        public void CreateDungeon() {
            DungoenTiles = new TileType[x, y];
        }

        public void DrawDungoen() {
            
        }


    }
}
