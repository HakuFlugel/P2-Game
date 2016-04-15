using System;
using System.Drawing;
using System.Collections.Generic;


namespace WinFormsTest {
    public class TileType {
        public static List<TileType> tileTypes = new List<TileType>();

        public bool Moveable = true;
        public Bitmap Image;

        static TileType() {
            Random rand = new Random();
            tileTypes.Add(new TileType(){Moveable = rand.Next(1,5) <= 3});
            tileTypes.Add(new TileType(){Moveable = rand.Next(1,5) <= 3});
            tileTypes.Add(new TileType(){Moveable = rand.Next(1,5) <= 3});
            tileTypes.Add(new TileType(){Moveable = rand.Next(1,5) <= 3});
            tileTypes.Add(new TileType(){Moveable = rand.Next(1,5) <= 3});
            tileTypes.Add(new TileType(){Moveable = rand.Next(1,5) <= 3});
            tileTypes.Add(new TileType(){Moveable = rand.Next(1,5) <= 3});


        }

        public void LoadImage(string imageString) {
            Image = (Bitmap)Bitmap.FromFile(imageString);
        }
    }
}
