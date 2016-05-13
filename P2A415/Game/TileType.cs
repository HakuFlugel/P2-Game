using System;
using System.Drawing;
using System.Collections.Generic;



namespace RPGame {
    public class TileType {
        public static List<TileType> tileTypes = new List<TileType>();

        public bool Moveable = true;
        public static Bitmap Image;
        public static string pictureLocation = "Content/all_bioms.png";
        public int imageIndex;

        static TileType() {
            Image = ImageLoader.Load(pictureLocation);

            for(int index = 0; index < 4; index++) {

                tileTypes.Add(new TileType() { Moveable = false });
                tileTypes.Add(new TileType() { Moveable = false });
                tileTypes.Add(new TileType() { Moveable = false });

                for (int i = 0; i < 16; i++) {
                    tileTypes.Add(new TileType() { Moveable = true });
                }
                tileTypes.Add(new TileType() { Moveable = false });
                tileTypes.Add(new TileType() { Moveable = true }); // Town


            }


            int tile_length = tileTypes.Count;

            for (int index = 0; index < tile_length; index ++) {

                tileTypes[index].imageIndex = index;
            }
        }
    }
}
