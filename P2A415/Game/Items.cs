using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace WinFormsTest {
    public class Items {
        public Bitmap Image;

        public static Dictionary<int, Items> itemList = new Dictionary<int, Items>();

        public string itemImageFile;
        public static int itemID;
        public string itemName;
        public double itemHP;
        public double itemLVL;
        public double itemDMG;
        public double itemDEF;

        private struct itemType {
            public int Hands;
            public int Gloves;
            public int Helmet;
            public int Chest;
            public int Belt;
            public int Pants;
            public int Boots;
        }
        private itemType equipSlot;

        static Items() {
            itemList.Add(itemID++, new Items() {
                itemImageFile = "",
                itemName = "Sword of Slays",
                itemHP = 1,
                itemLVL = 1,
                itemDMG = 1,
                itemDEF = 0,
                equipSlot = new itemType() {
                    Hands = 1
                }
            });
            itemList.Add(itemID++, new Items() {
                itemImageFile = "",
                itemName = "God's Gloves",
                itemHP = 1,
                itemLVL = 2,
                itemDMG = 1,
                itemDEF = 10,
                equipSlot = new itemType() {
                    Gloves = 1,
                    Hands = -1
                }
            });
            itemList.Add(itemID++, new Items() {
                itemImageFile = "",
                itemName = "Chestplate of big boobies",
                itemHP = 1,
                itemLVL = 3,
                itemDMG = 3,
                itemDEF = 2,
                equipSlot = new itemType() {
                    Chest = 1
                }
            });
            itemList.Add(itemID++, new Items() {
                itemImageFile = "",
                itemName = "Tracer's leggings",
                itemHP = 1,
                itemLVL = 4,
                itemDMG = 10,
                itemDEF = 0,
                equipSlot = new itemType() {
                    Pants = 1
                }
            });
        }

    }
}
