using System;

namespace Game {
    public class Region {

        public int[] position = new int[2];
        private Region[] neighbors = new Region[4];

        private uint[,] Tiles = new uint[32,32]; // TODO: mindre type?

        public Region() {


        }
    }
}

