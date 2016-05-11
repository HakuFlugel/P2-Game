using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Net.Mime;

namespace RPGame {
    
    public struct EquipSlot {
        public int Hand; //2
        public int Gloves; //1
        public int Helmet; //1
        public int Chest; //1
        public int Belt; //1
        public int Pants; //1
        public int Ring; //2
        public int Amulet; //1
        public int Boots; //1 total = 10

        public static EquipSlot operator +(EquipSlot e1, EquipSlot e2) {

            return new EquipSlot {
                Hand = e1.Hand + e2.Hand,
                Gloves = e1.Gloves + e2.Gloves,
                Helmet = e1.Helmet + e2.Helmet,
                Chest = e1.Chest + e2.Chest,
                Belt = e1.Belt + e2.Belt,
                Pants = e1.Pants + e2.Pants,
                Ring = e1.Ring + e2.Ring,
                Amulet = e1.Amulet + e2.Amulet,
                Boots = e1.Boots + e2.Boots
            };
        }

        public static EquipSlot operator -(EquipSlot e1, EquipSlot e2) {

            return new EquipSlot {
                Hand = e1.Hand - e2.Hand,
                Gloves = e1.Gloves - e2.Gloves,
                Helmet = e1.Helmet - e2.Helmet,
                Chest = e1.Chest - e2.Chest,
                Belt = e1.Belt - e2.Belt,
                Pants = e1.Pants - e2.Pants,
                Ring = e1.Ring - e2.Ring,
                Amulet = e1.Amulet - e2.Amulet,
                Boots = e1.Boots - e2.Boots
            };
        }

        public static EquipSlot operator -(EquipSlot e1) {

            return new EquipSlot {
                Hand = -e1.Hand,
                Gloves = -e1.Gloves,
                Helmet = -e1.Helmet,
                Chest = -e1.Chest,
                Belt = -e1.Belt,
                Pants = -e1.Pants,
                Ring = -e1.Ring,
                Amulet = -e1.Amulet,
                Boots = -e1.Boots
            };
        }
    }

    public class Item {

        private static Random rand = new Random();

        public static List<Item> itemTypes = new List<Item>();

        public static Bitmap itemImage;

        public int imageIndex; // Set to first index of image, in itemTypes list
        public int itemLVL; // Set to total number of images, in itemTypes list
        public string itemName;
        public double itemHP;
        public double itemDMG;
        public double itemDEF;
        public double itemSPEED;
        public double itemPENE;
        public string flavortext;


        public EquipSlot equipSlot;

        static Item() {

            itemImage = ImageLoader.Load("Content/Items.png");

            itemTypes.Add(new Item() {
                imageIndex = 5,
                itemName = "Two-Handed Axe",
                itemHP = 0,
                itemLVL = 1,
                itemDMG = 15,
                itemPENE = 4,
                itemDEF = 0,
                itemSPEED = 0.25,
                equipSlot = new EquipSlot() {
                    Hand = 2
                }
            });
            itemTypes.Add(new Item() {
                imageIndex = 4,
                itemName = "Claw",
                itemHP = 0,
                itemLVL = 1,
                itemDMG = 12,
                itemPENE = 4.55,
                itemSPEED = 0.3,
                itemDEF = 0,
                equipSlot = new EquipSlot() {
                    Hand = 1
                }
            });
            itemTypes.Add(new Item() {
                imageIndex = 2,
                itemName = "Two-Handed Sword",
                itemHP = 0,
                itemLVL = 1,
                itemDMG = 13,
                itemPENE = 1.4,
                itemSPEED = 0.29,
                itemDEF = 0,
                equipSlot = new EquipSlot() {
                    Hand = 2
                }
            });
            itemTypes.Add(new Item() {
                imageIndex = 3,
                itemName = "Club",
                itemHP = 0,
                itemLVL = 1,
                itemDMG = 10,
                itemPENE = 2,
                itemSPEED = 0.375,
                itemDEF = 0,
                equipSlot = new EquipSlot() {
                    Hand = 2
                }
            });

            itemTypes.Add(new Item() {
                imageIndex = 1,
                itemName = "Sword",
                itemHP = 0,
                itemLVL = 1,
                itemDMG = 11,
                itemPENE = 1.75,
                itemSPEED = 0.45,
                itemDEF = 0,
                equipSlot = new EquipSlot() {
                    Hand = 1
                }
            });
            itemTypes.Add(new Item() {
                imageIndex = 0,
                itemName = "Dagger",
                itemHP = 0,
                itemLVL = 1,
                itemDMG = 8,
                itemPENE = 1.5,
                itemSPEED = 0.5,
                itemDEF = 0,
                equipSlot = new EquipSlot() {
                    Hand = 1
                }
            });

            itemTypes.Add(new Item() {
                imageIndex = 6,
                itemName = "Shield",
                itemHP = 0,
                itemLVL = 3,
                itemDMG = 0,
                itemPENE = 0,
                itemSPEED = 0,
                itemDEF = 2,
                equipSlot = new EquipSlot() {
                    Hand = 1
                }
            });

            itemTypes.Add(new Item() {
                imageIndex = 9,
                itemName = "Helmet",
                itemHP = 15,
                itemLVL = 3,
                itemDMG = 0,
                itemPENE = 0,
                itemSPEED = 0,
                itemDEF = 0,
                equipSlot = new EquipSlot() {
                    Helmet = 1
                }
            });

            itemTypes.Add(new Item() {
                imageIndex = 12,
                itemName = "Chest Plate",
                itemHP = 20,
                itemLVL = 3,
                itemDMG = 0,
                itemPENE = 0,
                itemSPEED = 0,
                itemDEF = 2,
                equipSlot = new EquipSlot() {
                    Chest = 1
                }
            });

            itemTypes.Add(new Item() {
                imageIndex = 15,
                itemName = "Gloves",
                itemHP = 0,
                itemLVL = 3,
                itemDMG = 0,
                itemPENE = 0,
                itemSPEED = 0.15,
                itemDEF = 0,
                equipSlot = new EquipSlot() {
                    Gloves = 1,
                    Hand = -1
                }
            });

            itemTypes.Add(new Item() {
                imageIndex = 18,
                itemName = "Boots",
                itemHP = 0,
                itemLVL = 3,
                itemDMG = 0,
                itemPENE = 0,
                itemSPEED = 0,
                itemDEF = 1,
                equipSlot = new EquipSlot() {
                    Boots = 1
                }
            });

            itemTypes.Add(new Item() {
                imageIndex = 21,
                itemName = "Amulet",
                itemHP = 25,
                itemLVL = 3,
                itemDMG = 0,
                itemPENE = 0,
                itemSPEED = 0,
                itemDEF = 0,
                equipSlot = new EquipSlot() {
                    Amulet = 1
                }
            });

            itemTypes.Add(new Item() {
                imageIndex = 24,
                itemName = "Ring",
                itemHP = 15,
                itemLVL = 3,
                itemDMG = 0,
                itemPENE = 0,
                itemSPEED = 0,
                itemDEF = 0,
                equipSlot = new EquipSlot() {
                    Ring = 1
                }
            });
//            itemTypes.Add(new Item() {
//                imageIndex = 4,
//                itemName = "Potion",
//                itemHP = 35,
//                itemLVL = 2,
//                itemDMG = 0,
//                itemPENE = 0,
//                itemSPEED = 0,
//                itemDEF = 0,
//                equipSlot = new EquipSlot() {
//                    Hand = 1
//                }
//            });

        }

        private string[] Prefix = { "Wooden ", "Rusty ", "Blunt ", "Marked ", "Fugly ", "Ugly ", "Singing And Dancing ", "Longshot ", "Lickable ", "Beautifull ", "Outstanding ", "Understandable ", "Glory ", "Golden ", "Fine ", "Godly ", "Bloody ", "Sureal " };

        private string[] Suffix = { " Of Destiny", " Made By God", " Of Slayer", " Slaien", ", Bob", ", Edgar", ", Hem", " Made Outta Gold", " Of What?", " Of Wood", " Of Leather", " Of Gold", ", Joakim" };
        
        private string[] flavorText = {
            "This ITEMNAME is a long lost item. Now found, by you!", "Who is ready for some seal clubbing?",
            "I like trains. And this ITEMNAME. But I like trains more.", "ITEMNAME is ready for your use, or missuse.",
            "This ITEMNAME was once an adventurer like you, but it took an arrow to the knee",
            "Oh My GOD! A double ITEMNAME all the way, or something", "Flavor flavor flavor flavor",
            "I once had the chance, but moms spaghetti got in the way", "Skov is Skov, but Skov is not skov",
            "I once had this ITEMNAME like you, but then I took an arrow to the knee", "This ITEMNAME once showed great love towards other items, yet this love, was foolish."
        };

        private Item() {
        }

        public Item(Item itemType, int level) {
            //Random rand = new Random(Guid.NewGuid().GetHashCode());

            this.imageIndex = itemType.imageIndex + rand.Next(0, itemType.itemLVL-1);

            string prefix = (level < 20) ? Prefix[rand.Next(0, Prefix.Count() / 4)] :
                                 (level < 50) ? (Prefix[rand.Next(0, Prefix.Count() / 2)]) : (Prefix[rand.Next(0, Prefix.Count())]);
            string suffix = Suffix[rand.Next(0, Suffix.Count())];

            this.itemName = prefix + itemType.itemName + suffix;

            this.itemDMG = itemType.itemDMG * Math.Pow(1.10, level);
            this.itemHP = itemType.itemHP * Math.Pow(1.10, level);
            this.itemDEF = itemType.itemDEF * Math.Pow(1.07, level);
            this.itemPENE = itemType.itemPENE * Math.Pow(1.07, level);
            this.itemSPEED = itemType.itemSPEED;

            this.itemLVL = level;

            this.equipSlot = itemType.equipSlot;

            this.flavortext = flavorText[rand.Next(0,flavorText.Count())].Replace("ITEMNAME", itemType.itemName);
        }

        
//        public Item MakePotion(Item item, int playerHP) {
//            Item temp_item;
//
//            temp_item = item;
//
//            temp_item.itemName = "Health " + item.itemName;
//
//            temp_item.itemHP = (playerHP / 100) * 20;
//
//            return temp_item;
//        }

        
//        public string hpToString(int index) {
//            return "Health: " + itemTypes[index].itemHP.ToString();
//        }
//        public string lvlToString(int index) {
//            return "Level: " + itemTypes[index].itemLVL.ToString();
//        }
//        public string dmgToString(int index) {
//            return "Damage: " + itemTypes[index].itemDMG.ToString();
//        }
//        public string defToString(int index) {
//            return "Defence: " + itemTypes[index].itemDEF.ToString();
//        }
//        public string typeToString(int index) {
//            return itemTypes[index].equipSlot.ToString();
//        }


    }
}
