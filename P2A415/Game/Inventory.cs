using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RPGame {

    public class Inventory {

        int test_int = 2;

        private int selectedRow = 0;
        private int selectedColumn = 0;

        public bool isOpen { get; private set; } = false;

        private SolidBrush background = new SolidBrush(Color.FromArgb(128,Color.Black));
        //private Bitmap EqippedImage;

        private Item[][,] inventory = new Item[2][,];

        private EquipSlot equipSlots = new EquipSlot() {
            Hand = 2,
            Gloves = 1,
            Helmet = 1,
            Chest = 1,
            Belt = 1,
            Pants = 1,
            Ring = 2,
            Amulet = 1,
            Boots = 1
        };

        int activeContainer = 0;

        private class HighlightedItem {
            public HighlightedItem(int container, int row, int column) {
                this.container = container;
                this.row = row;
                this.column = column;
            }
            public int row, column;
            public int container;
        }

        private HighlightedItem highlightedItem;

        public Inventory() {
            inventory[0] = new Item[8, 8];
            inventory[1] = new Item[4, 4];
        }

        public void keyInput(KeyEventArgs e) {
            switch (e.KeyCode) {

            case Keys.Delete:
            case Keys.Back:
                //DeleteItem(); // TODO: evt. sælge
                break;

//                case Keys.Space:
//                    Items item = new Items();
//                    
//
//                    GetItem(item.MakeItem(new Items() {
//                        itemName = "Two Handed Sword",
//                        itemHP = 1,
//                        itemLVL = 1,
//                        itemDMG = 1,
//                        itemDEF = 0,
//                        equipSlot = new Items.itemType {
//                            Hands = 2
//                        }
//                    }, test_int++));
//                    GetItem(item.MakeItem(new Items() {
//                        itemName = "Chest Plate",
//                        itemHP = 1,
//                        itemLVL = 1,
//                        itemDMG = 1,
//                        itemDEF = 0,
//                        equipSlot = new Items.itemType {
//                            Chest = 1
//                        }
//                    }, test_int++));
//                    GetItem(item.MakeItem(new Items() {
//                        itemName = "Boots",
//                        itemHP = 1,
//                        itemLVL = 1,
//                        itemDMG = 1,
//                        itemDEF = 0,
//                        equipSlot = new Items.itemType {
//                            Boots = 1
//                        }
//                    }, test_int++));
//                    break;

            case Keys.W:
            case Keys.Up:
                    
                if (--selectedRow < 0) {
                    selectedRow = 0;
                }
                    
                break;

            case Keys.S:
            case Keys.Down:

                if (++selectedRow > inventory[activeContainer].GetUpperBound(0)) {
                    selectedRow = inventory[activeContainer].GetUpperBound(0);
                }

                break;

            case Keys.A:
            case Keys.Left:

                if (++selectedColumn < 0) {

                    if (activeContainer == 1) {
                        activeContainer = 0;
                        selectedRow = selectedRow * inventory[activeContainer].GetLength(0) / inventory[!activeContainer].GetLength(0);
                        selectedColumn = inventory[activeContainer].GetLength(1);
                    } else
                        selectedColumn = 0;
                }
                    //selected += (activeContainer.Count/8)*8;
//                    if (--selectedX < 0)
//                        selectedX = 0;
                break;

            case Keys.D:
            case Keys.Right:
                if (--selectedColumn > inventory[activeContainer].GetLength(1)) {

                    if (activeContainer == 0) {
                        activeContainer = 1;
                        selectedRow = selectedRow * inventory[activeContainer].GetLength(0) / inventory[!activeContainer].GetLength(0);
                        selectedColumn = 0;
                    } else
                        selectedColumn = inventory[activeContainer].GetLength(1);
                }
//                    if (++selectedX > 10)
//                        selectedX = 10;
//                    if (selectedX > 7)
//                        selectedY = (selectedY > 3 ? 3 : selectedY);
                break;

            
            case Keys.Enter:
                // TODO: escape to deselect
                if (highlightedItem == null) {
                    highlightedItem = new HighlightedItem(activeContainer, selectedRow, selectedColumn);
                } else {
                    if (highlightedItem.container == activeContainer && highlightedItem.row == selectedRow && highlightedItem.column == selectedColumn) {
                        highlightedItem = null;
                    } else {
                        moveItem(highlightedItem, new HighlightedItem(activeContainer, selectedRow, selectedColumn));
                    }
                }

                Console.WriteLine("Enter is pressed");
//                Console.WriteLine(tryEquip()); 
                break;
                //TODO: jump to other key
            default:
                break;
            }
            //Console.WriteLine("selectedx : " + selectedX + "   selectedy : " + selectedY);
        }

        public void moveItem(HighlightedItem source, HighlightedItem target) {

            Item sourceItem = inventory[source.container][source.row,source.column];
            Item targetItem = inventory[target.container][target.row, target.column];


            if (source.container != target.container) {

                EquipSlot deltaEquipSlots = sourceItem.equipSlot - (targetItem != null) ? targetItem.equipSlot : 0;
                if (target.container == 0) { // Move from equipped to carried
                    deltaEquipSlots = -deltaEquipSlots;
                }
                    
                EquipSlot resultingEquipSlots = equipSlots - deltaEquipSlots;

                if (resultingEquipSlots.Amulet < 0 || resultingEquipSlots.Belt < 0 || resultingEquipSlots.Boots < 0 ||
                    resultingEquipSlots.Chest < 0 || resultingEquipSlots.Gloves < 0 || resultingEquipSlots.Hand < 0 ||
                    resultingEquipSlots.Helmet < 0 || resultingEquipSlots.Pants < 0 || resultingEquipSlots.Ring < 0) {
                    return;
                }

                equipSlots = resultingEquipSlots;
            }

            inventory[source.container][source.row,source.column] = targetItem;
            inventory[target.container][target.row, target.column] = sourceItem;
        }
 
        public void toggle(Game game) {
            if (!isOpen)
                game.localPlayer.character.calculateStats();
            isOpen = !isOpen;
            selectedColumn = 0;
            selectedRow = 0;  
        }

        public void draw(Graphics gfx, Game game) {
            const 
        }
//            
//        }
//
//        public void draw(Graphics gfx, Game game) {
//
//            DrawCarried(gfx, game);
//
//
//
//            Font namefont = new Font("Arial", 15, FontStyle.Bold), 
//                 lvlfont = new Font("Arial", 10, FontStyle.Regular), 
//                 statsfont = new Font("Arial", 12, FontStyle.Regular), 
//                 flavortextFont = new Font("Arial", 12, FontStyle.Italic);
//            
//            float textPositionX = 0, textPositionY = 0;
//
//            Carried_sorted = carried.OrderBy(ch => ch.itemName).ToList();
//
//            int carriedCount = carried.Count;
//            
//            int xdex = 0;
//            int y=0, x=0;
//            for (int ypp = 0; ypp < 8; ypp++) 
//                for (int xpp = 0; xpp < 8; xpp++)
//                    totalCarried[ypp][xpp].setItem(null);
//
//            for (int ypp = 0; ypp < 4; ypp++)
//                for (int xpp = 0; xpp < 3; xpp++)
//                    totalEquipped[ypp][xpp].setItem(null);
//
//            foreach (var item in Carried_sorted) {
//                totalCarried[y][x].setItem(item);
//                x++;
//                if (x == 8) { x = 0; y++; }
//            }
//
//            for(int ypp = 0; ypp < 8; ypp++) 
//                for(int xpp = 0; xpp < 8; xpp++) 
//                    if(totalCarried[ypp][xpp].item != null)
//                        gfx.DrawImage(totalCarried[ypp][xpp].item.itemImageFile, new RectangleF(totalCarried[ypp][xpp].X, totalCarried[ypp][xpp].Y, 60, 60));
//             
//            x = 0; y = 0;
//            foreach (var item in equipped) {
//                totalEquipped[y][x].setItem(item);
//                x++;
//                if(x == 3) { x = 0; y++; }
//            }
//            for(int ypp = 0; ypp < 4; ypp++)
//                for(int xpp = 0; xpp < 3; xpp++)
//                    if(totalEquipped[ypp][xpp].item != null)
//                        gfx.DrawImage(totalEquipped[ypp][xpp].item.itemImageFile, new RectangleF(totalEquipped[ypp][xpp].X, totalEquipped[ypp][xpp].Y, 60, 60));
//
//
//            if ((selectedColumn < 7 && totalCarried[selectedRow][selectedColumn].item != null) || (selectedColumn >= 8 && totalEquipped[selectedRow][selectedColumn - 8].item != null)) {
//                Items item = selectedColumn < 7 ? totalCarried[selectedRow][selectedColumn].item : totalEquipped[selectedRow][selectedColumn - 8].item;
//                int index = xdex - 1;
//                float X = selectedColumn < 7 ? totalCarried[selectedRow][selectedColumn].X : totalEquipped[selectedRow][selectedColumn - 8].X,
//                      Y = selectedColumn < 7 ? totalCarried[selectedRow][selectedColumn].Y : totalEquipped[selectedRow][selectedColumn - 8].Y;
//                string flavortext = "";
//                if (item.flavortext != null) {
//                    flavortext = item.flavortext;
//                }
//
//                string name = item.itemName,
//                    lvl = "Level: " + item.itemLVL.ToString(),
//                    stats = "Health: " + item.itemHP.ToString() + Environment.NewLine +
//                    "Damage: " + item.itemDMG.ToString() + Environment.NewLine +
//                    "Defence: " + item.itemDEF.ToString() + Environment.NewLine + 
//                    "Speed: " + item.itemSPEED.ToString() + Environment.NewLine + 
//                    "Penetration: " + item.itemPENE.ToString() + Environment.NewLine;
//                
//
//                textPositionX = X + 65;
//                textPositionY = Y + 65;
//                SizeF sizeofFlavor = gfx.MeasureString(flavortext, flavortextFont, 190);
//
//                int heightOfItAll = (int)(gfx.MeasureString(name, namefont,190).Height +
//                                    gfx.MeasureString(lvl, lvlfont,190).Height +
//                                    gfx.MeasureString(stats, statsfont,190).Height + 10);
//                
//
//                gfx.FillRectangle(new SolidBrush(Color.DarkSalmon), new RectangleF(textPositionX-5,textPositionY-5, 200, heightOfItAll + sizeofFlavor.Height));
//
//                gfx.DrawString(name, namefont, Brushes.WhiteSmoke,
//                    new RectangleF(new PointF(textPositionX, textPositionY),
//                    new SizeF( gfx.MeasureString(name, namefont,190))));
//
//                gfx.DrawString(lvl, lvlfont, Brushes.WhiteSmoke,
//                    textPositionX + 5, textPositionY += (int)gfx.MeasureString(name, namefont, 190).Height);
//
//                gfx.DrawString(stats, statsfont, Brushes.WhiteSmoke,
//                    textPositionX + 5, textPositionY += (int)gfx.MeasureString(lvl, lvlfont, 190).Height);
//
//                gfx.DrawString(flavortext, flavortextFont, Brushes.WhiteSmoke,
//                    new RectangleF(new PointF(textPositionX + 5, textPositionY + gfx.MeasureString(stats, statsfont, 190).Height),
//                    new SizeF(sizeofFlavor)));
//
//            }
//        }
//
//        public void DrawCarried(Graphics gfx, Game game) {
//
//            int width = (int)(game.Width / 1.2), height = (int)(game.Height / 1.2);
//            int placex = game.Width / 2 - width / 2;
//            int placey = game.Height / 2 - height / 2;
//
//            Font font = new Font("Bradley Hand ITC", 40, FontStyle.Italic);
//            gfx.FillRectangle(background, new Rectangle(0,0,game.Width,game.Height));
//            gfx.FillRectangle(new SolidBrush(Color.DarkGray), new Rectangle(placex, placey, width, height));
//            
//            float xdex = placex + 72, ydex = placey + 70, outterboxWid = 60, outterboxHei = 60;
//
//            gfx.DrawImage(EqippedImage, new Rectangle(placex +width-850, placey-20, width-200, height), new Rectangle(0, 0, 64, 64), GraphicsUnit.Pixel);
//            
//
//            gfx.DrawString("Inventory", font, Brushes.Black,new Point(placex + 2,placey + 5));
//
//            ydex = placey + height * 0.5f - 200;
//
//            for (int indey = 0; indey < 4; indey++) {
//                xdex = placex + width * 0.69f;
//                
//
//                for(int index = 0; index < 3; index++) {
//                    totalEquipped[indey][index].X = xdex + 2;
//                    totalEquipped[indey][index].Y = ydex + 2;
//
//                    if(indey == selectedRow && index == selectedColumn-8) {
//                        drawSelected(gfx, xdex, ydex, outterboxWid, outterboxHei);
//                    } else {
//                        drawNotSelected(gfx, xdex, ydex, outterboxWid, outterboxHei);
//                    }
//                    xdex += 70;
//                   
//                }
//
//                ydex += 70;
//            }
//
//            xdex = placex + 72; ydex = placey + 70; outterboxWid = 60; outterboxHei = 60;
//
//            for (int indey = 0; indey < 8; indey++) {
//                xdex = placex + 72;
//                for (int index = 0; index < 8; index++) {
//
//                    totalCarried[indey][index].X = xdex + 2;
//                    totalCarried[indey][index].Y = ydex + 2;
//
//                    if (indey == selectedRow && index == selectedColumn) {
//                        drawSelected(gfx, xdex, ydex, outterboxWid, outterboxHei);
//                    } else {
//                        drawNotSelected(gfx, xdex, ydex, outterboxWid, outterboxHei);
//                    }
//                    xdex += 70;
//                    
//                }
//                ydex += 70;
//                
//            }
//        }
//
//        public void drawNotSelected(Graphics gfx, float xdex, float ydex, float outterboxWid, float outterboxHei) {
//            gfx.FillRectangle(new SolidBrush(Color.Black), new RectangleF(xdex, ydex, outterboxWid, outterboxHei));
//            gfx.FillRectangle(new SolidBrush(Color.GhostWhite), new RectangleF(xdex + 2, ydex + 2, outterboxWid - 4, outterboxWid - 4));
//        }
//        
//        public void drawSelected(Graphics gfx, float xdex, float ydex, float outterboxWid, float outterboxHei) {
//            gfx.FillRectangle(new SolidBrush(Color.Orange), new RectangleF(xdex-2, ydex-2, outterboxWid + 6, outterboxHei + 6));
//            gfx.FillRectangle(new SolidBrush(Color.GhostWhite), new RectangleF(xdex + 2, ydex + 2, outterboxWid - 4, outterboxWid - 4));
//        }
//        
//        public void calStats (out double[] array) {
//            double attack = 0, defence = 0, speed = 0, penetration = 0, hp = 0;
//
//            foreach (var item in equipped) {
//                if(item != null) {
//                    attack += item.itemDMG;
//                    defence += item.itemDEF;
//                    speed += item.itemSPEED;
//                    penetration += item.itemSPEED;
//                    hp += item.itemHP;
//                }
//            }
//            array = new double[5];
//            array[0] = hp;
//            array[1] = defence;
//            array[2] = attack;
//            array[3] = penetration;
//            array[4] = speed;
//
//        }
    }
}
