using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPGame {
    [Serializable]
    public class Statistics {

        public int encounters { get; set; }
        public int kills { get; set; }
        public int deaths { get; set; }

        public int questions { get {return correct + wrong;}}
        public int correct { get; set; }
        public int wrong { get; set; }

        public int distance { get; set; }

        private int _monsterHighestLevel;
        public int monsterHighestLevel {
            get { return _monsterHighestLevel; }
            set {
                if (_monsterHighestLevel < value) {
                    _monsterHighestLevel = value;
                }
            }
        }
        private int _highestLevel;
        public int highestLevel {
            get { return _highestLevel; }
            set {
                if (_highestLevel < value) {
                    _highestLevel = value;
                }
            }
        }
        public int townVisits { get; set; }
    }
}
