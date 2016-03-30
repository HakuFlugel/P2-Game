using System;

namespace WinFormsTest {
    public class Region {
        public byte[,] tiles = new byte[32,32];

        public long x, y;

        public Region(long x, long y) {
            this.x = x;
            this.y = y;
        }
    }
}

