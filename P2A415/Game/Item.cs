using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Net.Mime;

namespace RPGame {

    [Serializable]
    public struct EquipSlot {
        public int hand;  
        public int gloves; 
        public int helmet;  
        public int chest; 
        public int belt; 
        public int pants; 
        public int ring;
        public int amulet; 
        public int boots; 

        public static EquipSlot operator +(EquipSlot e1, EquipSlot e2) {

            return new EquipSlot {
                hand = e1.hand + e2.hand,
                gloves = e1.gloves + e2.gloves,
                helmet = e1.helmet + e2.helmet,
                chest = e1.chest + e2.chest,
                belt = e1.belt + e2.belt,
                pants = e1.pants + e2.pants,
                ring = e1.ring + e2.ring,
                amulet = e1.amulet + e2.amulet,
                boots = e1.boots + e2.boots
            };
        }

        public static EquipSlot operator -(EquipSlot e1, EquipSlot e2) {

            return new EquipSlot {
                hand = e1.hand - e2.hand,
                gloves = e1.gloves - e2.gloves,
                helmet = e1.helmet - e2.helmet,
                chest = e1.chest - e2.chest,
                belt = e1.belt - e2.belt,
                pants = e1.pants - e2.pants,
                ring = e1.ring - e2.ring,
                amulet = e1.amulet - e2.amulet,
                boots = e1.boots - e2.boots
            };
        }

        public static EquipSlot operator -(EquipSlot e1) {

            return new EquipSlot {
                hand = -e1.hand,
                gloves = -e1.gloves,
                helmet = -e1.helmet,
                chest = -e1.chest,
                belt = -e1.belt,
                pants = -e1.pants,
                ring = -e1.ring,
                amulet = -e1.amulet,
                boots = -e1.boots
            };
        }
    }

    [Serializable]
    public class Item {

        public static Bitmap itemImage;
        private static Random rand = new Random();
        public static List<Item> itemTypes = new List<Item>();

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
                itemDMG = 30,
                itemPENE = 2,
                itemDEF = 0,
                itemSPEED = 0.25,
                equipSlot = new EquipSlot() {
                    hand = 2
                }
            });
            itemTypes.Add(new Item() {
                imageIndex = 4,
                itemName = "Claw",
                itemHP = 0,
                itemLVL = 1,
                itemDMG = 5.75,
                itemPENE = 3.5,
                itemSPEED = 0.2,
                itemDEF = 0,
                equipSlot = new EquipSlot() {
                    hand = 1
                }
            });
            itemTypes.Add(new Item() {
                imageIndex = 2,
                itemName = "Two-Handed Sword",
                itemHP = 0,
                itemLVL = 1,
                itemDMG = 23,
                itemPENE = 4,
                itemSPEED = 0.29,
                itemDEF = 0,
                equipSlot = new EquipSlot() {
                    hand = 2
                }
            });
            itemTypes.Add(new Item() {
                imageIndex = 3,
                itemName = "Club",
                itemHP = 0,
                itemLVL = 1,
                itemDMG = 13,
                itemPENE = 1.25,
                itemSPEED = 0.325,
                itemDEF = 0,
                equipSlot = new EquipSlot() {
                    hand = 1
                }
            });

            itemTypes.Add(new Item() {
                imageIndex = 1,
                itemName = "Sword",
                itemHP = 0,
                itemLVL = 1,
                itemDMG = 10,
                itemPENE = 2,
                itemSPEED = 0.375,
                itemDEF = 0,
                equipSlot = new EquipSlot() {
                    hand = 1
                }
            });
            itemTypes.Add(new Item() {
                imageIndex = 0,
                itemName = "Dagger",
                itemHP = 0,
                itemLVL = 1,
                itemDMG = 7,
                itemPENE = 2.5,
                itemSPEED = 0.45,
                itemDEF = 0,
                equipSlot = new EquipSlot() {
                    hand = 1
                }
            });

            itemTypes.Add(new Item() {
                imageIndex = 6,
                itemName = "Shield",
                itemHP = 10,
                itemLVL = 3,
                itemDMG = 0,
                itemPENE = 0,
                itemSPEED = 0,
                itemDEF = 4,
                equipSlot = new EquipSlot() {
                    hand = 1
                }
            });

            itemTypes.Add(new Item() {
                imageIndex = 9,
                itemName = "Helmet",
                itemHP = 7.5,
                itemLVL = 3,
                itemDMG = 0,
                itemPENE = 0,
                itemSPEED = 0,
                itemDEF = 0.45,
                equipSlot = new EquipSlot() {
                    helmet = 1
                }
            });

            itemTypes.Add(new Item() {
                imageIndex = 12,
                itemName = "Chest Plate",
                itemHP = 12,
                itemLVL = 3,
                itemDMG = 0,
                itemPENE = 0,
                itemSPEED = 0,
                itemDEF = 1.5,
                equipSlot = new EquipSlot() {
                    chest = 1
                }
            });

            itemTypes.Add(new Item() {
                imageIndex = 21,
                itemName = "Pants",
                itemHP = 9,
                itemLVL = 3,
                itemDMG = 0,
                itemPENE = 0,
                itemSPEED = 0,
                itemDEF = 0.75,
                equipSlot = new EquipSlot() {
                    pants = 1
                }
            });

            itemTypes.Add(new Item() {
                imageIndex = 15,
                itemName = "Gloves",
                itemHP = 1,
                itemLVL = 3,
                itemDMG = 5,
                itemPENE = 0,
                itemSPEED = 0.15,
                itemDEF = 0.1,
                equipSlot = new EquipSlot() {
                    gloves = 1,
                    hand = -1
                }
            });

            itemTypes.Add(new Item() {
                imageIndex = 18,
                itemName = "Boots",
                itemHP = 2,
                itemLVL = 3,
                itemDMG = 0,
                itemPENE = 0,
                itemSPEED = 0,
                itemDEF = 0.2,
                equipSlot = new EquipSlot() {
                    boots = 1
                }
            });

            itemTypes.Add(new Item() {
                imageIndex = 24,
                itemName = "Amulet",
                itemHP = 10,
                itemLVL = 3,
                itemDMG = 0,
                itemPENE = 0,
                itemSPEED = 0,
                itemDEF = 0,
                equipSlot = new EquipSlot() {
                    amulet = 1
                }
            });

            itemTypes.Add(new Item() {
                imageIndex = 27,
                itemName = "Ring",
                itemHP = 5,
                itemLVL = 3,
                itemDMG = 0,
                itemPENE = 0,
                itemSPEED = 0,
                itemDEF = 0,
                equipSlot = new EquipSlot() {
                    ring = 1
                }
            });

        }

        private string[] Prefix = { "Harmless ", "Wooden ", "Rusty ", "Iron ", "Blunt ", "Marked ", "Fugly ", "Ugly ", "Steel ", "Singing And Dancing ", "Longshot ", "Lickable ", "Beautifull ", "Outstanding ", "Understandable ", "Deadly ", "Glory ", "Unholy ", "Holy ", "Dark ",  "Golden ", "Fine ", "Godly ", "Bloody ", "Sureal " };

        private string[] Suffix = { " Of Destiny", " Made By God", " Of Slayer", " Slaien", ", Bob", ", Edgar", ", Hem", " Made Outta Gold", " Of What?", " Of Wood", " Of Leather", " Of Gold", ", Joakim", " of Steel", " of Iron", " of Ponies", " of Holiness", " of Unholiness" };
        
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
            this.imageIndex = itemType.imageIndex + rand.Next(0, itemType.itemLVL-1);

            string prefix = (level < 20) ? Prefix[rand.Next(0, Prefix.Count() / 4)] :
                                 (level < 50) ? (Prefix[rand.Next(0, Prefix.Count() / 2)]) : (Prefix[rand.Next(0, Prefix.Count())]);
            string suffix = Suffix[rand.Next(0, Suffix.Count())];

            this.itemName = prefix + itemType.itemName + suffix;

            this.itemDMG = itemType.itemDMG * Math.Pow(1.20, level);
            this.itemHP = itemType.itemHP * Math.Pow(1.20, level);
            this.itemDEF = itemType.itemDEF * Math.Pow(1.17, level);
            this.itemPENE = itemType.itemPENE * Math.Pow(1.17, level);
            this.itemSPEED = itemType.itemSPEED;

            this.itemLVL = level;

            this.equipSlot = itemType.equipSlot;

            this.flavortext = flavorText[rand.Next(0,flavorText.Count())].Replace("ITEMNAME", itemType.itemName);
        }
    }
}
