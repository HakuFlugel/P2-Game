using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

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
                Helmet = e1.Hand + e2.Helmet,
                Chest = e1.Chest + e2.Chest,
                Belt = e1.Chest + e2.Chest,
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
                Helmet = e1.Hand - e2.Helmet,
                Chest = e1.Chest - e2.Chest,
                Belt = e1.Chest - e2.Chest,
                Belt = e1.Belt - e2.Belt,
                Pants = e1.Pants - e2.Pants,
                Ring = e1.Ring - e2.Ring,
                Amulet = e1.Amulet - e2.Amulet,
                Boots = e1.Boots - e2.Boots
            };
        }
    }

    public class Item {

        public static Dictionary<int, Item> itemTypes = new Dictionary<int, Item>();
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


        public EquipSlot equipSlot;

        static Item() {

            itemTypes.Add(new Item() {
                itemName = "Axe",
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
                itemName = "Two Handed Sword",
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
                itemName = "Shield",
                itemHP = 0,
                itemLVL = 1,
                itemDMG = 0,
                itemPENE = 0,
                itemSPEED = 0,
                itemDEF = 2,
                equipSlot = new EquipSlot() {
                    Hand = 1
                }
            });

            itemTypes.Add(new Item() {
                itemName = "Helmet",
                itemHP = 15,
                itemLVL = 1,
                itemDMG = 0,
                itemPENE = 0,
                itemSPEED = 0,
                itemDEF = 0,
                equipSlot = new EquipSlot() {
                    Helmet = 1
                }
            });

            itemTypes.Add(new Item() {
                itemName = "Chest Plate",
                itemHP = 20,
                itemLVL = 1,
                itemDMG = 0,
                itemPENE = 0,
                itemSPEED = 0,
                itemDEF = 2,
                equipSlot = new EquipSlot() {
                    Chest = 1
                }
            });

            itemTypes.Add(new Item() {
                itemName = "Gloves",
                itemHP = 0,
                itemLVL = 1,
                itemDMG = 0,
                itemPENE = 0,
                itemSPEED = 0.05,
                itemDEF = 0,
                equipSlot = new EquipSlot() {
                    Gloves = 1
                }
            });

            itemTypes.Add(new Item() {
                 
                itemName = "Boots",
                itemHP = 0,
                itemLVL = 1,
                itemDMG = 0,
                itemPENE = 0,
                itemSPEED = 0,
                itemDEF = 1,
                equipSlot = new EquipSlot() {
                    Boots = 1
                }
            });

            itemTypes.Add(new Item() {
                 
                itemName = "Amulet",
                itemHP = 25,
                itemLVL = 1,
                itemDMG = 0,
                itemPENE = 0,
                itemSPEED = 0,
                itemDEF = 0,
                equipSlot = new EquipSlot() {
                    Amulet = 1
                }
            });

            itemTypes.Add(new Item() {
                 
                itemName = "Ring",
                itemHP = 15,
                itemLVL = 1,
                itemDMG = 0,
                itemPENE = 0,
                itemSPEED = 0,
                itemDEF = 0,
                equipSlot = new EquipSlot() {
                    Ring = 1
                }
            });
            itemTypes.Add(new Item() {
                 
                itemName = "Potion",
                itemHP = 35,
                itemLVL = 1,
                itemDMG = 0,
                itemPENE = 0,
                itemSPEED = 0,
                itemDEF = 0,
                equipSlot = new EquipSlot() {
                    Hand = 1
                }
            });

        }

        private string[] Prefix = { "Wooden ", "Rusty ", "Blunt ", "Marked ", "Fugly ", "Ugly ", "Singing And Dancing ", "Longshot ", "Lickable ", "Beautifull ", "Outstanding ", "Understandable ", "Glory ", "Golden ", "Fine ", "Godly ", "Bloody ", "Sureal " };

        private string[] Suffix = { " Of Destiny", " Made By God", " Of Slayer", " Slaien", ", Bob", ", Edgar", ", Hem", " Made Outta Gold", " Of What?", " Of Wood", " Of Leather", " Of Gold", ", Joakim" };
        
        private string[] flavorText = { "This ITEM is a long lost item. Now found, by you!", "Who is ready for some seal clubbing?",
                                        "I like trains. And this ITEM. But I like trains more.", "ITEM is ready for your use, or missuse.",
                                        "This ITEM was once an adventure like you, but it took an arrow to the knee",
                                        "Oh My GOD! A double ITEM all the way, or something", "Flavor flavor flavor flavor",
                                        "I once had the chance, but moms spaghetti got in the way", "Skov is Skov, but Skov is not skov",
                                        "I once had this ITEM like you, but then I took an arrow to the knee", "This ITEM once showed great love towards other items, yet this love, was foolish."};

        public Item MakeItem(Item item, int level) {
            Random rand = new Random(); // TODO
            Item temp_item; // TODO
            Bitmap source = ImageLoader.Load("Content/Items.png"); //TODO
            string[] splitted = item.itemName.Split(' '); //TODO
            //TODO: fjern switch, sæt fra static constructor istedet...
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

            temp_item = item;

            string prefix = (level < 20) ? Prefix[rand.Next(0, Prefix.Count() / 4)] :
                                 (level < 50) ? (Prefix[rand.Next(0, Prefix.Count() / 2)]) : (Prefix[rand.Next(0, Prefix.Count())]);
            string suffix = Suffix[rand.Next(0, Suffix.Count())];

            temp_item.itemName = prefix + item.itemName + suffix;

            //TODO: hvor bliver de sat???
            temp_item.itemDMG = temp_item.itemDMG * Math.Pow(1.07, level);
            temp_item.itemHP = temp_item.itemHP * Math.Pow(1.07, level);
            temp_item.itemDEF = temp_item.itemDEF * Math.Pow(1.05, level);
            temp_item.itemPENE = temp_item.itemPENE * Math.Pow(1.05, level);

            temp_item.itemLVL = level;

            string temp_flavortext = flavorText[rand.Next(0,flavorText.Count())].Replace("ITEM", temp_item.itemName);

            temp_item.flavortext = temp_flavortext;


            temp_item.itemLVL = level;

            return temp_item;
        }

        
        public Item MakePotion(Item item, int playerHP) {
            Item temp_item;

            temp_item = item;

            temp_item.itemName = "Health " + item.itemName;

            temp_item.itemHP = (playerHP / 100) * 20;

            return temp_item;
        }

        //TODO: wtf?
        private int[] returnIntFitsPic(int number_on_picture) {
            x_y[0] = (number_on_picture % 3) * 32;

            int number_div = (number_on_picture / 3) * 32;

            x_y[1] = number_div;
            return x_y;
        }
        
        public string hpToString(int index) {
            return "Health: " + itemTypes[index].itemHP.ToString();
        }
        public string lvlToString(int index) {
            return "Level: " + itemTypes[index].itemLVL.ToString();
        }
        public string dmgToString(int index) {
            return "Damage: " + itemTypes[index].itemDMG.ToString();
        }
        public string defToString(int index) {
            return "Defence: " + itemTypes[index].itemDEF.ToString();
        }
        public string typeToString(int index) {
            return itemTypes[index].equipSlot.ToString();
        }


    }
}
