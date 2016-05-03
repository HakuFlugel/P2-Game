using System;
using System.Collections.Generic;
using System.Drawing;


namespace RPGame {
    public class Region {
        public List<Character> characters = new List<Character>();

        private int[,] tiles = new int[32,32];
        public int this[long x, long y] {
            get {
                return tiles[x, y];
            }
            set {
                tiles[x, y] = value;
            }
        }

        public int x, y;
        public int townx, towny;

        public Region(int x, int y) {
            this.x = x;
            this.y = y;
        }
        public void makeTown(Random rand) {
            int x, y;
            while (this[x = 4+rand.Next() % 24, y = 4+rand.Next() % 24] != (int)World.GeneratedTile.Ground);

            this[x, y] = (int)World.GeneratedTile.Town;
            townx = x;
            towny = y;
        }

        public void draw(Game game, Graphics gfx, Position cameraPosition) {
            int cameraPositionRegionX = cameraPosition.x - this.x * tiles.GetLength(0);// * 64 * 2;
            int cameraPositionRegionY = cameraPosition.y - this.y * tiles.GetLength(1);// * 64 * 2;

            // X range
            int cameraStartRegionX = (cameraPositionRegionX - game.ClientSize.Width / 2 / (64 * 2)) - 2;
            cameraStartRegionX = Math.Max(cameraStartRegionX, 0);

            int cameraEndRegionX = (cameraPositionRegionX + game.ClientSize.Width / 2 / (64 * 2)) + 2;
            cameraEndRegionX = Math.Min(cameraEndRegionX, tiles.GetUpperBound(0));

            // Y range
            int cameraStartRegionY = (cameraPositionRegionY - game.ClientSize.Height / 2 / (64 * 2)) - 2;
            cameraStartRegionY = Math.Max(cameraStartRegionY, 0);


            int cameraEndRegionY = (cameraPositionRegionY + game.ClientSize.Height / 2 / (64 * 2)) + 2;
            cameraEndRegionY = Math.Min(cameraEndRegionY, tiles.GetUpperBound(1));

            // drawing it
            for (long xindex = cameraStartRegionX; xindex <= cameraEndRegionX; xindex++) {
                for (long yindex = cameraStartRegionY; yindex <= cameraEndRegionY; yindex++) {

                    int tileID = tiles[xindex, yindex];

                    int x, y;
                    x = (int)((xindex - cameraPositionRegionX - cameraPosition.xoffset * cameraPosition.offsetScale) * 64 * 2 + game.ClientSize.Width / 2 - 64);
                    y = (int)((yindex - cameraPositionRegionY - cameraPosition.yoffset * cameraPosition.offsetScale) * 64 * 2 - game.ClientSize.Height / 2 + 64);
                    
                    TileType tt = TileType.tileTypes[tileID];

//                    x /= 2;
//                    y /= 2;

                    gfx.DrawImage( TileType.Image,
                        new RectangleF(x, -y, 64.0f * 2, 64.0f * 2),
                        new Rectangle(1+tt.imageIndex % 21 * 64, 1+ tt.imageIndex / 21 * 64, 62, 62), 
                        GraphicsUnit.Pixel);

                }
            }
            foreach (var character in characters) {
                character.draw(game, gfx, cameraPosition);
            }
        }

        internal void update(Game game, double deltaTime) {
            foreach (var character in characters) {
                character.update(game, deltaTime);
            }
        }
    }
}

