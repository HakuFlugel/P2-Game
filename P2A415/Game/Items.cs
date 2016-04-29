using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace WinFormsTest {
    public class Items {

        public static Dictionary<int, Items> itemList = new Dictionary<int, Items>();

        public string itemImageFile;
        public static int itemID;
        public string itemName;
        public double itemHP;
        public double itemLVL;
        public double itemDMG;
        public double itemDEF;

        public struct itemType {
            public int Hands;
            public int Gloves;
            public int Helmet;
            public int Chest;
            public int Belt;
            public int Pants;
            public int Boots;
        }
        public itemType equipSlot;

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
        public string nameToString(int index) {
            return itemList[index].itemName;
        }
        public string hpToString(int index) {
            return "Health: " + itemList[index].itemHP.ToString();
        }
        public string lvlToString(int index) {
            return "Level: " + itemList[index].itemLVL.ToString();
        }
        public string dmgToString(int index) {
            return "Damage: " + itemList[index].itemDMG.ToString();
        }
        public string defToString(int index) {
            return "Defence: " + itemList[index].itemDEF.ToString();
        }
        public string typeToString(int index) {
            return itemList[index].equipSlot.ToString();
        }


    }
}
