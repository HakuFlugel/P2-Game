using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RPGame {

    public class Inventory {

        private Player player;
            
        private int selectedRow = 0;
        private int selectedColumn = 0;

        public bool isOpen { get; private set; } = false;

        private Brush background = new SolidBrush(Color.FromArgb(128,Color.Black));
        private Brush invBackground = new SolidBrush(Color.FromArgb(128, Color.Black));
        private Brush itemBackground = new SolidBrush(Color.FromArgb(192, Color.WhiteSmoke));
        private Brush itemSelectedBackground = new SolidBrush(Color.FromArgb(192, Color.Orange));
        private Brush itemHighlightedBackground = new SolidBrush(Color.FromArgb(192, Color.Violet));
                
        Font titleFont = new Font("Bradley Hand ITC", 40, FontStyle.Italic); 
               
        Font nameFont = new Font("Arial", 15, FontStyle.Bold);
        Font lvlFont = new Font("Arial", 10, FontStyle.Regular);
        Font statsFont = new Font("Arial", 12, FontStyle.Regular); 
        Font flavortextFont = new Font("Arial", 12, FontStyle.Italic);
                
        //private Bitmap EqippedImage;
        
        public Item[][,] inventory { get; private set; } = new Item[2][,];

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

        public class HighlightedItem {
            public HighlightedItem(int container, int row, int column) {
                this.container = container;
                this.row = row;
                this.column = column;
            }
            public int row, column;
            public int container;
        }

        private HighlightedItem highlightedItem;
       
        public Inventory(Player player) {
            this.player = player;

            inventory[0] = new Item[8, 8];
            inventory[1] = new Item[4, 4];

            // TODO: debug code below
            inventory[0][0, 0] = new Item(Item.itemTypes[0], 42);
            inventory[0][1, 0] = new Item(Item.itemTypes[1], 42);
            inventory[0][2, 0] = new Item(Item.itemTypes[2], 42);
            inventory[0][3, 0] = new Item(Item.itemTypes[3], 42);
            inventory[0][4, 0] = new Item(Item.itemTypes[4], 42);
            inventory[0][5, 0] = new Item(Item.itemTypes[5], 42);


            inventory[0][0, 1] = new Item(Item.itemTypes[6], 42);
            inventory[0][0, 2] = new Item(Item.itemTypes[7], 42);
            inventory[0][0, 3] = new Item(Item.itemTypes[8], 42);
            inventory[0][0, 4] = new Item(Item.itemTypes[9], 42);
            inventory[0][0, 5] = new Item(Item.itemTypes[10], 42);
            inventory[0][0, 6] = new Item(Item.itemTypes[11], 42);
            inventory[0][0, 7] = new Item(Item.itemTypes[12], 42);

            }

        public void keyInput(KeyEventArgs e) {
            switch (e.KeyCode) {

                case Keys.Delete:
                case Keys.Back:

                Item item = inventory[activeContainer][selectedRow, selectedColumn];
                    
                if (item != null) {
                    player.character.addExperience((ulong)(Math.Pow(item.itemLVL, 1.14) * 1.1 + 5));
                    inventory[activeContainer][selectedRow, selectedColumn] = null;
                        }

                    break;


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

                if (--selectedColumn < 0) {

                    if (activeContainer == 1) {
                        activeContainer = 0;
                        selectedRow = selectedRow * inventory[activeContainer].GetLength(0) / inventory[activeContainer == 0 ? 1 : 0].GetLength(0);
                        selectedColumn = inventory[activeContainer].GetUpperBound(1);
                    } else
                        selectedColumn = 0;
                }
                    //selected += (activeContainer.Count/8)*8;
//                    if (--selectedX < 0)
//                        selectedX = 0;
                    break;

                case Keys.D:
                case Keys.Right:
                if (++selectedColumn > inventory[activeContainer].GetUpperBound(1)) {

                    if (activeContainer == 0) {
                        activeContainer = 1;
                        selectedRow = selectedRow * inventory[activeContainer].GetLength(0) / inventory[activeContainer == 0 ? 1 : 0].GetLength(0);
                        selectedColumn = 0;
                    } else
                        selectedColumn = inventory[activeContainer].GetUpperBound(1);
                }
//                    if (++selectedX > 10)
//                        selectedX = 10;
//                    if (selectedX > 7)
//                        selectedY = (selectedY > 3 ? 3 : selectedY);
                break;
            case Keys.Escape:
                highlightedItem = null;
                    break;
                case Keys.Enter:
            case Keys.Space:
                if (highlightedItem == null) {
                    highlightedItem = new HighlightedItem(activeContainer, selectedRow, selectedColumn);
                } else {
                    // TODO: er det her tjek nødvendigt? man kunne bare lade den bytte et item med sig selv
                    if (highlightedItem.container == activeContainer && highlightedItem.row == selectedRow && highlightedItem.column == selectedColumn) {
                        highlightedItem = null;
                    } else {
                        moveItem(highlightedItem, new HighlightedItem(activeContainer, selectedRow, selectedColumn));
                        highlightedItem = null;
                    }
                }
                    break;
                //TODO: jump to other key
                default:
                    break;
            }
                }
                
        public void moveItem(HighlightedItem source, HighlightedItem target) {

            Item sourceItem = inventory[source.container][source.row,source.column];
            Item targetItem = inventory[target.container][target.row, target.column];
                    
            if (sourceItem == null && targetItem == null) {
                return;
        }

            if (source.container != target.container) {
                EquipSlot deltaEquipSlots = (sourceItem != null ? sourceItem.equipSlot : new EquipSlot()) - (targetItem != null ? targetItem.equipSlot : new EquipSlot());
                if (target.container == 0) {                // Move from equipped to carried
                    deltaEquipSlots = -deltaEquipSlots;

            }
            
                EquipSlot resultingEquipSlots = equipSlots - deltaEquipSlots;

                if (resultingEquipSlots.Amulet < 0 || resultingEquipSlots.Belt < 0 || resultingEquipSlots.Boots < 0 ||
                    resultingEquipSlots.Chest < 0 || resultingEquipSlots.Gloves < 0 || resultingEquipSlots.Hand < 0 ||
                    resultingEquipSlots.Helmet < 0 || resultingEquipSlots.Pants < 0 || resultingEquipSlots.Ring < 0) {
                    Console.WriteLine("Not enough equip slots");
                    return;
                    }

                equipSlots = resultingEquipSlots;
                }

            inventory[source.container][source.row,source.column] = targetItem;
            inventory[target.container][target.row, target.column] = sourceItem;
                
            player.character.calculateStats();
                }
                
        public void toggle(Game game) {
//            if (!isOpen)
//                game.localPlayer.character.calculateStats();
            isOpen = !isOpen;
            activeContainer = 0;
            selectedColumn = 0;
            selectedRow = 0;  
            highlightedItem = null;
        }

        public bool addItem(Item gainedItem) {

            for (int y = 0; y < inventory[0].GetLength(0); y++)
                for (int x = 0; x < inventory[0].GetLength(1); x++) {
                    Item item = inventory[0][y, x];
                    if (item == null) {
                        item = gainedItem;
            return true;
        }
        }
            return false;
        }

        public void draw(Graphics gfx, Game game) {
                

             
            
            int screenWidth = game.ClientSize.Width, screenHeight = game.ClientSize.Height;

            const int inventoryPadding = 16;

            const int itemSize = 64;
            const int itemPadding = 8;

            SizeF carriedSize = new SizeF(
                (itemSize + itemPadding) * inventory[0].GetLength(1) + itemPadding,
                (itemSize + itemPadding) * inventory[0].GetLength(0) + itemPadding);
            
            SizeF equippedSize = new SizeF(
                (itemSize + itemPadding) * inventory[1].GetLength(1) + itemPadding,
                (itemSize + itemPadding) * inventory[1].GetLength(0) + itemPadding);

            const string titleText = "Equipped";
            SizeF titleSize = gfx.MeasureString(titleText, titleFont);

            SizeF inventorySize = new SizeF(
                carriedSize.Width + Math.Max(equippedSize.Width, titleSize.Width) + 3 * inventoryPadding,
                Math.Max(carriedSize.Height, equippedSize.Height + titleSize.Height + inventoryPadding) + 2 * inventoryPadding );

            RectangleF inventoryRect = new RectangleF(
               (screenWidth - inventorySize.Width) / 2,
               (screenHeight - inventorySize.Height) / 2,
               inventorySize.Width, inventorySize.Height);

            RectangleF carriedRect = new RectangleF(
                inventoryRect.X + inventoryPadding,
                inventoryRect.Y + inventoryPadding,
                carriedSize.Width, carriedSize.Height);

            RectangleF titleRect = new RectangleF(
                carriedRect.X + carriedRect.Width + inventoryPadding,
                inventoryRect.Y + inventoryPadding,
                titleSize.Width, titleSize.Height);

            RectangleF equippedRect = new RectangleF(
                carriedRect.X + carriedRect.Width + inventoryPadding,
                titleRect.Y + titleRect.Height + inventoryPadding,
                equippedSize.Width, equippedSize.Height);

            gfx.FillRectangle(background, inventoryRect);

            gfx.FillRectangle(invBackground, carriedRect);
            gfx.FillRectangle(invBackground, equippedRect);

            gfx.DrawString(titleText, titleFont, Brushes.WhiteSmoke, titleRect);
            
            for (int y = 0; y < inventory[0].GetLength(0); y++) {               //Draw carried
                for (int x = 0; x < inventory[0].GetLength(1); x++) {

                    RectangleF itemRect = new RectangleF(
                        carriedRect.X + itemPadding + x * (itemSize + itemPadding),
                        carriedRect.Y + itemPadding + y * (itemSize + itemPadding),
                        itemSize, itemSize);

                    if (highlightedItem != null && highlightedItem.container == 0 && highlightedItem.column == x && highlightedItem.row == y) {
                        gfx.FillRectangle(itemHighlightedBackground, itemRect);
                    } else if (activeContainer == 0 && selectedRow == y && selectedColumn == x) {
                        gfx.FillRectangle(itemSelectedBackground, itemRect);
                    } else {
                        gfx.FillRectangle(itemBackground, itemRect);
                    }
            
                    gfx.DrawRectangle(Pens.Black, itemRect.X, itemRect.Y, itemRect.Width, itemRect.Height);

                    Item item = inventory[0][y, x];

                    if (item != null) {
                        RectangleF srcRect = new RectangleF(item.imageIndex % 3 * 32, item.imageIndex / 3 * 32, 32, 32);
                        gfx.DrawImage(Item.itemImage, itemRect, srcRect, GraphicsUnit.Pixel);
            }
            }
                }

            for (int y = 0; y < inventory[1].GetLength(0); y++) {               //Draw Equipped
                for (int x = 0; x < inventory[1].GetLength(1); x++) {
                
                    RectangleF itemRect = new RectangleF(
                        equippedRect.X + itemPadding + x * (itemSize + itemPadding),
                        equippedRect.Y + itemPadding + y * (itemSize + itemPadding),
                        itemSize, itemSize);

                    if (highlightedItem != null && highlightedItem.container == 1 && highlightedItem.column == x && highlightedItem.row == y) {
                        gfx.FillRectangle(itemHighlightedBackground, itemRect);
                    } else if (activeContainer == 1 && selectedRow == y && selectedColumn == x) {
                        gfx.FillRectangle(itemSelectedBackground, itemRect);
                    } else {
                        gfx.FillRectangle(itemBackground, itemRect);
                    }

                    gfx.DrawRectangle(Pens.Black, itemRect.X, itemRect.Y, itemRect.Width, itemRect.Height);
                
                    Item item = inventory[1][y, x];

                    if (item != null) {
                        RectangleF srcRect = new RectangleF(item.imageIndex % 3 * 32, item.imageIndex / 3 * 32, 32, 32);
                        gfx.DrawImage(Item.itemImage, itemRect, srcRect, GraphicsUnit.Pixel);
                    }

            }
        }

            Item selectedItem = inventory[activeContainer][selectedRow, selectedColumn];

            if (selectedItem != null) {

            
                string name = selectedItem.itemName;
                string lvl = "Level: " + selectedItem.itemLVL.ToString();

                //TODO: formatting
                string stats = "";
                if (selectedItem.itemHP != 0) stats += $"Health: {selectedItem.itemHP.ToString()}\n";
                if (selectedItem.itemDEF != 0) stats += $"Armor: {selectedItem.itemDEF.ToString()}\n";
                if (selectedItem.itemDMG != 0) stats += $"Damage: {selectedItem.itemDMG.ToString()}\n";
                if (selectedItem.itemPENE != 0) stats += $"Armor Penetration: {selectedItem.itemPENE.ToString()}\n";
                if (selectedItem.itemSPEED != 0) stats += $"Slow: {selectedItem.itemSPEED.ToString()}\n";
            
                string flavortext = selectedItem.flavortext;

                //TODO: with 190, 200 som variabel. og +10...5...
                SizeF sizeName = gfx.MeasureString(name, nameFont, 320);
                SizeF sizeLevel = gfx.MeasureString(lvl, lvlFont, 320);
                SizeF sizeStats = gfx.MeasureString(stats, statsFont, 320);
                SizeF sizeFlavor = gfx.MeasureString(flavortext, flavortextFont, 320);


                RectangleF selectedRect;

                if (activeContainer == 0) {
                    selectedRect = new RectangleF(
                        carriedRect.X + itemPadding + selectedColumn * (itemSize + itemPadding),
                        carriedRect.Y + itemPadding + selectedRow * (itemSize + itemPadding),
                        itemSize, itemSize);
                    } else {
                    selectedRect = new RectangleF(
                        equippedRect.X + itemPadding + selectedColumn * (itemSize + itemPadding),
                        equippedRect.Y + itemPadding + selectedRow * (itemSize + itemPadding),
                        itemSize, itemSize);
            }

                RectangleF tooltipRect = new RectangleF(
                    selectedRect.X + selectedRect.Width, selectedRect.Y + selectedRect.Height,
                    330, sizeName.Height + sizeLevel.Height + sizeStats.Height + sizeFlavor.Height + 10
                );

                gfx.FillRectangle(background, tooltipRect);
                    
                RectangleF nameRect = new RectangleF(tooltipRect.X + 5, tooltipRect.Y+5, sizeName.Width, sizeName.Height);
                RectangleF levelRect = new RectangleF(tooltipRect.X + 5, nameRect.Y + nameRect.Height, sizeLevel.Width, sizeLevel.Height);
                RectangleF statsRect = new RectangleF(tooltipRect.X + 5, levelRect.Y + levelRect.Height, sizeStats.Width, sizeStats.Height);
                RectangleF flavorRect = new RectangleF(tooltipRect.X + 5, statsRect.Y + statsRect.Height, sizeFlavor.Width, sizeFlavor.Height);
                
                gfx.DrawString(name, nameFont, Brushes.WhiteSmoke, nameRect);
                gfx.DrawString(lvl, lvlFont, Brushes.WhiteSmoke, levelRect);
                gfx.DrawString(stats, statsFont, Brushes.WhiteSmoke, statsRect);
                gfx.DrawString(flavortext, flavortextFont, Brushes.WhiteSmoke, flavorRect);

        }
        
        }
        
        public double[] calculateStats() {
            double attack = 0, defence = 0, speed = 0, penetration = 0, hp = 0;

            foreach (Item item in inventory[1]) {
                if(item != null) {
                    attack += item.itemDMG;
                    defence += item.itemDEF;
                    speed += item.itemSPEED;
                    penetration += item.itemSPEED;
                    hp += item.itemHP;
                }
            }


            return new double[5]{hp, defence, attack, penetration, speed};

        }

    }
}
