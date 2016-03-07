using System;

namespace Game {

    public struct Stats {
        double hp;// = 100.0;
        double attack;// = 1.0;
        double defence;// = 1.0;

        ulong level;// = 0;
        ulong exp;// = 0.0;
    }

    public struct Position {
        long x;// = 0;
        long y;// = 0;

        float xoffset;// = 0.0;
        float yoffset;// = 0.0;

        public Position (long x, long y) {
            this.x = x;
            this.y = y;

            this.xoffset = 0;
            this.yoffset = 0;
        }
    }


    public class Character {

        public Position position;
        public Stats stats = new Stats();


        public Character(long x, long y) : this() {
            
        }
        public Character() {
            
        }
    }
}

