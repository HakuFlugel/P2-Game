using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace RPGame {
    public class Items {

        public static Dictionary<int, Items> itemList = new Dictionary<int, Items>();
        public int[] x_y = new int[2];

        public Bitmap itemImageFile;
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
                                        "This ITEM was once an adventure like you, but it took an arrow to the knee",
                                        "Oh My GOD! A dubble ITEM all the way, or something", "Flavor flavor flavor flavor",
                                        "I once had the chance, but moms spagettie got in the way", "Skov is Skov, but Skov is not skov",
                                        "I once had this ITEM like you, but then I took an arrow to the knee", "This ITEM once showed great love towards other items, yet this love, was foolish."};

        public Items MakeItem(Items item, int level) {
            Random rand = new Random();
            Items temp_item;
            Bitmap source = ImageLoader.Load("Content/Items.png");
            string[] splitted = item.itemName.Split(' ');
            switch (splitted[0]) {
                case "Dagger":
                    returnIntFitsPic(0);
                    break;
                case "Sword":
                    returnIntFitsPic(1);
                    break;
                case "Two":
                    returnIntFitsPic(2);
                    break;
                case "Club":
                    returnIntFitsPic(3);
                    break;
                case "Claw":
                    returnIntFitsPic(4);
                    break;
                case "Axe":
                    returnIntFitsPic(5);
                    break;
                case "Shield":
                    returnIntFitsPic(rand.Next(6, 8));
                    break;
                case "Helmet":
                    returnIntFitsPic(rand.Next(9, 11));
                    break;
                case "Chest":
                    returnIntFitsPic(rand.Next(12,14));
                    break;
                case "Gloves":
                    returnIntFitsPic(rand.Next(15, 17));
                    break;
                case "Boots":
                    returnIntFitsPic(rand.Next(18,20));
                    break;
                case "Amulet":
                    returnIntFitsPic(rand.Next(21, 23));
                    break;
                case "Ring":
                    returnIntFitsPic(rand.Next(24,26));
                    break;
                case "Leggings":
                    returnIntFitsPic(rand.Next(/*TODO MAKE LEGGINGS!*/));
                    break;
                default:
                    break;
            }

            item.itemImageFile = source.Clone(new Rectangle(x_y[0],x_y[1],32,32),source.PixelFormat);
                
            temp_item = item;

            string prefix = (level < 20) ? Prefix[rand.Next(0, Prefix.Count() / 4)] :
                                 (level < 50) ? (Prefix[rand.Next(0, Prefix.Count() / 2)]) : (Prefix[rand.Next(0, Prefix.Count())]);
            string suffix = Suffix[rand.Next(0, Suffix.Count())];

            temp_item.itemName = prefix + item.itemName + suffix;

            temp_item.itemDMG = temp_item.itemDMG * Math.Pow(1.07, level);

            temp_item.itemHP = temp_item.itemHP * Math.Pow(1.07, level);

            temp_item.itemHP = temp_item.itemDEF * Math.Pow(1.07, level);
            temp_item.itemLVL = level;

            string temp_flavertext = flavorText[rand.Next(0,flavorText.Count())].Replace("ITEM", temp_item.itemName);

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

        private int[] returnIntFitsPic(int number_on_picture) {
            x_y[0] = (number_on_picture % 3) * 32;

            int number_div = (number_on_picture / 3) * 32;

            x_y[1] = number_div;
            return x_y;
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
