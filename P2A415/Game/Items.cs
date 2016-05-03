using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace RPGame {
    public class Items {

        public static Dictionary<int, Items> itemList = new Dictionary<int, Items>();

        public string itemImageFile;
        public static int itemID;
        public string itemName;
        public double itemHP;
        public double itemLVL;
        public double itemDMG;
        public double itemDEF;
        public double itemSPEED;
        public double itemPENE;
        public string flavortext;

        public struct itemType {
            public int Hands; //2
            public int Gloves; //1
            public int Helmet; //1
            public int Chest; //1
            public int Belt; //1
            public int Pants; //1
            public int RingSlot; //2
            public int AmuletSlot; //1
            public int Boots; //1 total = 10
        }
        public itemType equipSlot;

        static Items() {

            itemList.Add(itemID++, new Items() {
                itemImageFile = "",
                flavortext = "",
                itemName = "Axe",
                itemHP = 0,
                itemLVL = 1,
                itemDMG = 15,
                itemPENE = 4,
                itemDEF = 0,
                itemSPEED = 0.25,
                equipSlot = new itemType() {
                    Hands = 2
                }
            });
            itemList.Add(itemID++, new Items() {
                itemImageFile = "",
                itemName = "Claw",
                flavortext = "",
                itemHP = 0,
                itemLVL = 1,
                itemDMG = 12,
                itemPENE = 4.55,
                itemSPEED = 0.3,
                itemDEF = 0,
                equipSlot = new itemType() {
                    Hands = 1
                }
            });
            itemList.Add(itemID++, new Items() {
                itemImageFile = "",
                itemName = "Two Handed Sword",
                flavortext = "",
                itemHP = 0,
                itemLVL = 1,
                itemDMG = 13,
                itemPENE = 1.4,
                itemSPEED = 0.29,
                itemDEF = 0,
                equipSlot = new itemType() {
                    Hands = 2
                }
            });
            itemList.Add(itemID++, new Items() {
                itemImageFile = "",
                itemName = "Club",
                flavortext = "",
                itemHP = 0,
                itemLVL = 1,
                itemDMG = 10,
                itemPENE = 2,
                itemSPEED = 0.375,
                itemDEF = 0,
                equipSlot = new itemType() {
                    Hands = 2
                }
            });

            itemList.Add(itemID++, new Items() {
                itemImageFile = "",
                itemName = "Sword",
                flavortext = "",
                itemHP = 0,
                itemLVL = 1,
                itemDMG = 11,
                itemPENE = 1.75,
                itemSPEED = 0.45,
                itemDEF = 0,
                equipSlot = new itemType() {
                    Hands = 1
                }
            });
            itemList.Add(itemID++, new Items() {
                itemImageFile = "",
                itemName = "Dagger",
                flavortext = "",
                itemHP = 0,
                itemLVL = 1,
                itemDMG = 8,
                itemPENE = 1.5,
                itemSPEED = 0.5,
                itemDEF = 0,
                equipSlot = new itemType() {
                    Hands = 1
                }
            });

            itemList.Add(itemID++, new Items() {
                itemImageFile = "",
                itemName = "Shield",
                flavortext = "",
                itemHP = 0,
                itemLVL = 1,
                itemDMG = 0,
                itemPENE = 0,
                itemSPEED = 0,
                itemDEF = 2,
                equipSlot = new itemType() {
                    Hands = 1
                }
            });

            itemList.Add(itemID++, new Items() {
                itemImageFile = "",
                itemName = "Helmet",
                flavortext = "",
                itemHP = 15,
                itemLVL = 1,
                itemDMG = 0,
                itemPENE = 0,
                itemSPEED = 0,
                itemDEF = 0,
                equipSlot = new itemType() {
                    Helmet = 1
                }
            });

            itemList.Add(itemID++, new Items() {
                itemImageFile = "",
                itemName = "Chest Plate",
                flavortext = "",
                itemHP = 20,
                itemLVL = 1,
                itemDMG = 0,
                itemPENE = 0,
                itemSPEED = 0,
                itemDEF = 2,
                equipSlot = new itemType() {
                    Chest = 1
                }
            });

            itemList.Add(itemID++, new Items() {
                itemImageFile = "",
                itemName = "Gloves",
                flavortext = "",
                itemHP = 0,
                itemLVL = 1,
                itemDMG = 0,
                itemPENE = 0,
                itemSPEED = 0.05,
                itemDEF = 0,
                equipSlot = new itemType() {
                    Gloves = 1
                }
            });

            itemList.Add(itemID++, new Items() {
                itemImageFile = "",
                itemName = "Boots",
                flavortext = "",
                itemHP = 0,
                itemLVL = 1,
                itemDMG = 0,
                itemPENE = 0,
                itemSPEED = 0,
                itemDEF = 1,
                equipSlot = new itemType() {
                    Boots = 1
                }
            });

            itemList.Add(itemID++, new Items() {
                itemImageFile = "",
                itemName = "Amulet",
                flavortext = "",
                itemHP = 25,
                itemLVL = 1,
                itemDMG = 0,
                itemPENE = 0,
                itemSPEED = 0,
                itemDEF = 0,
                equipSlot = new itemType() {
                    AmuletSlot = 1
                }
            });

            itemList.Add(itemID++, new Items() {
                itemImageFile = "",
                itemName = "Ring",
                flavortext = "",
                itemHP = 15,
                itemLVL = 1,
                itemDMG = 0,
                itemPENE = 0,
                itemSPEED = 0,
                itemDEF = 0,
                equipSlot = new itemType() {
                    RingSlot = 1
                }
            });
            itemList.Add(itemID++, new Items() {
                itemImageFile = "",
                itemName = "Potion",
                flavortext = "",
                itemHP = 35,
                itemLVL = 1,
                itemDMG = 0,
                itemPENE = 0,
                itemSPEED = 0,
                itemDEF = 0,
                equipSlot = new itemType() {
                    Hands = 1
                }
            });

        }



        private string[] Prefix = { "Wooden ", "Rusty ", "Blunt ", "Marked ", "Fugly ", "Ugly ", "Singing And Dancing ", "Longshot ", "Lickable ", "Beautifull ", "Outstanding ", "Understandable ", "Glory ", "Golden ", "Fine ", "Godly ", "Bloody ", "Sureal " };
        

        private string[] Suffix = { " Of Destiny", " Made By God", " Of Slayer", " Slaien", ", Bob", ", Edgar", ", Hem", " Made Outta Gold", " Of What?", " Of Wood", " Of Leather", " Of Gold", ", Joakim" };
        
        private string[] flavorText = { "This ITEM is a long lost item. Now found, by you!", "Who is rdy for some seal clubbing?",
                                        "I like trains. And this ITEM. But I like trains more.", "ITEM is ready for your use, or missuse.",
                                        "This ITEM was once an adventure like you, but it took an arrow to the knee (Arrow, whats and arrow? -> they no excist in diz world)",
                                        "Oh My GOD! A dubble ITEM all the way, or something", "Flavor flavor flavor flavor",
                                        "I once had the chance, but moms spagettie got in the way", "Skov is Skov, but Skov is not skov",
                                        "I once had this ITEM like you, but then I took an arrow to the knee", "This ITEM once showed great love towards other items, yet this love, was foolish."};

        public Items MakeItem(Items item, int level) {
            Random rand = new Random();
            Items temp_item;
            temp_item = item;
            temp_item.itemName = (level < 20)? Prefix[rand.Next(0,Prefix.Count()/4)] :
                                 (level < 50)? Prefix[rand.Next(0, Prefix.Count() / 2)] : Prefix[rand.Next(0, Prefix.Count())] +
                                 item.itemName + Suffix[rand.Next(0, Suffix.Count())];

            temp_item.itemDMG = temp_item.itemDMG * Math.Pow(1.07, level);

            temp_item.itemHP = temp_item.itemHP * Math.Pow(1.07, level);

            temp_item.itemHP = temp_item.itemDEF * Math.Pow(1.07, level);

            string temp_flavertext = flavortext.Replace("ITEM", temp_item.itemName);

            temp_item.flavortext = temp_flavertext;


            temp_item.itemLVL = level;

            return temp_item;
        }

        public Items MakePotion(Items item, int playerHP) {
            Items temp_item;

            temp_item = item;

            temp_item.itemName = "Health " + item.itemName;

            temp_item.itemHP = (playerHP / 100) * 20;

            return temp_item;
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
