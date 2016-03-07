using System;

namespace Game {

    public struct Currency {
        ulong triangles; // Geometri 
        ulong squares; // Algebra
        ulong pentagons; // Statistik

        ulong hexagons; // Combined tringle+square+pentagon
    }

    public class Player {
        public Character character;

        public Currency currency;

        public Player(long x = 0, long y = 0) {
            character = new Character(x, y);
        }
    }
}

