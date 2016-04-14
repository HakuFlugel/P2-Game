using System;
using System.Drawing;


namespace WinFormsTest {
    public class Region {
        public TileType[,] Tiles = new TileType[32,32];

        public long x, y;

        public Region(long x, long y) {
            this.x = x;
            this.y = y;
        }

        public void draw(Graphics gfx, Position cameraPosition) {
            long cameraPositionRegionX = cameraPosition.x - this.x * 32 * 64 * 2;
            long cameraPositionRegionY = cameraPosition.y - this.y * 32 * 64 * 2;

            // X range
            long cameraStartRegionX = cameraPositionRegionX - (Game.instance.Width / 2 / (64 * 2)) - 1; // -1 ekstra?
            cameraStartRegionX = Math.Max(cameraStartRegionX, 0);

            long cameraEndRegionX = cameraPositionRegionX + (Game.instance.Width / 2 / (64 * 2)) + 1; // +1 ekstra?
            cameraEndRegionX = Math.Min(cameraEndRegionX, Tiles.GetLength(0));

            // Y range
            long cameraStartRegionY = cameraPositionRegionY - (Game.instance.Height / 2 / (64 * 2)) - 1; // -1 ekstra?
            cameraStartRegionY = Math.Max(cameraStartRegionY, 0);

            long cameraEndRegionY = cameraPositionRegionY + (Game.instance.Height / 2 / (64 * 2)) + 1; // +1 ekstra?
            cameraEndRegionY = Math.Min(cameraEndRegionY, Tiles.GetLength(0));

            // drawing it
            for (long xindex = cameraStartRegionX; xindex <= cameraEndRegionX; xindex++) {
                for (long yindex = cameraStartRegionY; yindex <= cameraEndRegionY; yindex++) {

                    // Draw TIEL HIER!!!

                    double x, y;
                    x = ((this.x - cameraPosition.x / 32) + (xindex - cameraPosition.x % 32 - cameraPosition.xoffset*cameraPosition.offsetScale))*64*2 + Game.instance.Width / 2 - 64;
                    y = ((this.y - cameraPosition.y / 32) + (yindex - cameraPosition.y % 32 - cameraPosition.yoffset*cameraPosition.offsetScale))*64*2 - Game.instance.Height / 2 + 64;


                    gfx.DrawImage(ImageLoader.Load(CharacterType.characterTypes[1].imageFile), new RectangleF((float)x, -(float)y, 64.0f * 2, 64.0f * 2), new Rectangle(0, 0, 64, 64), GraphicsUnit.Pixel);


                }
            }
        }
    }
}

