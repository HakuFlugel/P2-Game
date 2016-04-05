using System;
using System.Drawing;
using System.Collections.Generic;


namespace WinFormsTest {
    public class TileType {
        public static List<TileType> tileTypes = new List<TileType>();

        public bool Moveable = true;
        public Bitmap Image;

        public void LoadImage(string imageString) {
            Image = (Bitmap)Bitmap.FromFile(imageString);
        }
    }
}
