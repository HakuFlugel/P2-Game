using System;
using System.Drawing;


namespace RPGame {
    public class Region {
        private int[,] tiles = new int[32,32];
        public int this[long x, long y] {
            get {
                return tiles[x, y];
            }
            set {
                tiles[x, y] = value;
            }
        }

        public long x, y;

        public Region(long x, long y) {
            this.x = x;
            this.y = y;
        }

        public void draw(Game game, Graphics gfx, Position cameraPosition) {
            long cameraPositionRegionX = cameraPosition.x - this.x * tiles.GetLength(0);// * 64 * 2;
            long cameraPositionRegionY = cameraPosition.y - this.y * tiles.GetLength(1);// * 64 * 2;

            // X range
            long cameraStartRegionX = cameraPositionRegionX - (game.ClientSize.Width / 2 / (64 * 2)) - 1; // -1 ekstra?
            cameraStartRegionX = Math.Max(cameraStartRegionX, 0);

            long cameraEndRegionX = cameraPositionRegionX + (game.ClientSize.Width / 2 / (64 * 2)) + 1; // +1 ekstra?
            cameraEndRegionX = Math.Min(cameraEndRegionX, tiles.GetUpperBound(0));

            // Y range
            long cameraStartRegionY = cameraPositionRegionY - (game.ClientSize.Height / 2 / (64 * 2)) - 1; // -1 ekstra?
            cameraStartRegionY = Math.Max(cameraStartRegionY, 0);


            long cameraEndRegionY = cameraPositionRegionY + (game.ClientSize.Height / 2 / (64 * 2)) + 1; // +1 ekstra?
            cameraEndRegionY = Math.Min(cameraEndRegionY, tiles.GetUpperBound(1));

            // drawing it
            for (long xindex = cameraStartRegionX; xindex <= cameraEndRegionX; xindex++) {
                for (long yindex = cameraStartRegionY; yindex <= cameraEndRegionY; yindex++) {

                    int tileID = tiles[xindex, yindex];

                    // Draw TIEL HIER!!!
                    //if (TileType.tileTypes[tileID].Moveable) {
                    //    continue;
                    //}

                    double x, y;
                    x = (xindex - cameraPositionRegionX - cameraPosition.xoffset * cameraPosition.offsetScale) * 64 * 2 + game.ClientSize.Width / 2 - 64;
                    y = (yindex - cameraPositionRegionY - cameraPosition.yoffset * cameraPosition.offsetScale) * 64 * 2 - game.ClientSize.Height / 2 + 64;

                    //x = ((this.x - cameraPosition.x / Tiles.GetLength(0)) + (xindex - cameraPositionRegionX/*cameraPosition.x % 32*/ -cameraPosition.xoffset * cameraPosition.offsetScale)) * 64 * 2 + game.ClientSize.Width / 2 - 64;
                    //y = ((this.y - cameraPosition.y / Tiles.GetLength(1)) + (yindex - cameraPositionRegionY/*cameraPosition.y % 32*/ -cameraPosition.yoffset * cameraPosition.offsetScale)) * 64 * 2 - game.ClientSize.Height / 2 + 64;

                    
                    TileType tt = TileType.tileTypes[tileID];

                    gfx.DrawImage( TileType.Image ,
                                new RectangleF((float)x, -(float)y, 64.0f * 2, 64.0f * 2),
                                new Rectangle(tt.imageIndex % 19 * 64, tt.imageIndex / 19 * 64, 64, 64), 
                                GraphicsUnit.Pixel);

                }
            }
        }
    }
}

