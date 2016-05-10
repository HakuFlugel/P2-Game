using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPGame {
    public class Statistics {

        public static int Encounters { get; set; }
        public static int Kills { get; set; }
        public static int Deaths { get; set; }

        public static int Questions { get {return Correct + Wrong;}}
        public static int Correct { get; set; }
        public static int Wrong { get; set; }

        public static int Distance { get; set; }

        private static int _highestLevel;
        public static int HighestLevel {
            get { return _highestLevel; }
            set {
                if (_highestLevel < value) {
                    _highestLevel = value;
                }
            }
        }
        public static int TownVisit { get; set; }
    }
}
