using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

namespace RPGame {

    public class Inventory {

        private Player player;

        public bool isOpen { get; private set; } = false;

        private int selectedRow = 0;
        private int selectedColumn = 0;
        int activeContainer = 0;

        private Brush background = new SolidBrush(Color.FromArgb(128,Color.Black));
        private Brush inventoryBackground = new SolidBrush(Color.FromArgb(128, Color.Black));
        private Brush itemBackground = new SolidBrush(Color.FromArgb(192, Color.WhiteSmoke));
        private Brush itemSelectedBackground = new SolidBrush(Color.FromArgb(192, Color.Orange));
        private Brush itemHighlightedBackground = new SolidBrush(Color.FromArgb(192, Color.Violet));
       
        Font titleFont = new Font("Arial", 40, FontStyle.Bold);
        Font bigFont = new Font("Arial", 24, FontStyle.Bold);
        Font nameFont = new Font("Arial", 16, FontStyle.Bold);
        Font lvlFont = new Font("Arial", 10, FontStyle.Regular);
        Font statsFont = new Font("Arial", 12, FontStyle.Regular); 
        Font bigStatsFont = new Font("Arial", 16, FontStyle.Regular); 
        Font flavortextFont = new Font("Arial", 12, FontStyle.Italic);

        public Item[][,] content = new Item[2][,];

        public EquipSlot equipSlots = new EquipSlot() {
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

            content[0] = new Item[8, 8];
            content[1] = new Item[4, 4];
        }

        public void keyInput(Game game, KeyEventArgs e) {
            switch (e.KeyCode) {

            case Keys.Delete:
            case Keys.Back:
                Item item = content[activeContainer][selectedRow, selectedColumn];
                    
                if (item != null) {
                    player.character.addExperience(game, (ulong)(Math.Pow(item.itemLVL, 1.14) * 1.1 + 5));
                    content[activeContainer][selectedRow, selectedColumn] = null;
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
                if (++selectedRow > content[activeContainer].GetUpperBound(0)) {
                    selectedRow = content[activeContainer].GetUpperBound(0);
                }
                break;

            case Keys.A:
            case Keys.Left:
                if (--selectedColumn < 0) {

                    if (activeContainer == 1) {
                        activeContainer = 0;
                        selectedRow = selectedRow * content[activeContainer].GetLength(0) / content[activeContainer == 0 ? 1 : 0].GetLength(0);
                        selectedColumn = content[activeContainer].GetUpperBound(1);
                    } else
                        selectedColumn = 0;
                }
                break;

            case Keys.D:
            case Keys.Right:
                if (++selectedColumn > content[activeContainer].GetUpperBound(1)) {

                    if (activeContainer == 0) {
                        activeContainer = 1;
                        selectedRow = selectedRow * content[activeContainer].GetLength(0) / content[activeContainer == 0 ? 1 : 0].GetLength(0);
                        selectedColumn = 0;
                    } else
                        selectedColumn = content[activeContainer].GetUpperBound(1);
                }
                break;

            case Keys.Escape:
                highlightedItem = null;
                break;

            case Keys.Enter:
            case Keys.Space:
                if (highlightedItem == null) {
                    highlightedItem = new HighlightedItem(activeContainer, selectedRow, selectedColumn);
                } else {
                    if (highlightedItem.container == activeContainer && highlightedItem.row == selectedRow && highlightedItem.column == selectedColumn) {
                        highlightedItem = null;
                    } else {
                        moveItem(highlightedItem, new HighlightedItem(activeContainer, selectedRow, selectedColumn));
                        highlightedItem = null;
                    }
                }
                break;

            case Keys.Tab:
                int prevContainer = activeContainer;
                activeContainer = activeContainer == 0 ? 1 : 0;

                selectedRow = selectedRow * content[activeContainer].GetLength(0) / content[prevContainer].GetLength(0);
                selectedColumn = selectedColumn * content[activeContainer].GetLength(1) / content[prevContainer].GetLength(1);
                break;

            default:
                break;
            }
        }
 
        public void moveItem(HighlightedItem source, HighlightedItem target) {
            Item sourceItem = content[source.container][source.row,source.column];
            Item targetItem = content[target.container][target.row, target.column];

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

            content[source.container][source.row,source.column] = targetItem;
            content[target.container][target.row, target.column] = sourceItem;
            
            player.character.calculateStats();
        }

        public void toggle(Game game) {
            isOpen = !isOpen;
            activeContainer = 0;
            selectedColumn = 0;
            selectedRow = 0;  
            highlightedItem = null;
        }

        public bool addItem(Item gainedItem) {
            for (int y = 0; y < content[0].GetLength(0); y++)
                for (int x = 0; x < content[0].GetLength(1); x++)
                    if (content[0][y, x] == null) {
                        content[0][y, x] = gainedItem;
                        return true;
                    }

            return false;
        }

        public void draw(Graphics gfx, Game game) {    
            if (!isOpen) {
                return;
            }

            int screenWidth = game.ClientSize.Width, screenHeight = game.ClientSize.Height;

            const int inventoryPadding = 16;

            const int itemSize = 64;
            const int itemPadding = 8;

            SizeF carriedSize = new SizeF(
                (itemSize + itemPadding) * content[0].GetLength(1) + itemPadding,
                (itemSize + itemPadding) * content[0].GetLength(0) + itemPadding);
            
            SizeF equippedSize = new SizeF(
                (itemSize + itemPadding) * content[1].GetLength(1) + itemPadding,
                (itemSize + itemPadding) * content[1].GetLength(0) + itemPadding);

            const string titleText = "Equipped";
            SizeF titleSize = gfx.MeasureString(titleText, titleFont);

            const string statTitleText = "Player Stats";
            SizeF statTitleSize = gfx.MeasureString(statTitleText, bigFont);

            string playerdmg = player.character.stats.attack.ToString("0.00");
            string playerpen = player.character.stats.armorPen.ToString("0.00");
            string playerhp = player.character.stats.curHP.ToString("0") + "/" + player.character.stats.maxHP.ToString("0");
            string playerdef = player.character.stats.defence.ToString("0");
            string playerstats = player.character.stats.attackSpeed.ToString("0.000");

            string statsText = $@"Damage: {playerdmg}
Penetration: {playerpen}
Health: {playerhp}
Defence: {playerdef}
Slow: {playerstats}";

//            SizeF statSize = gfx.MeasureString(statsText, statsFont);

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

            RectangleF statTitleRect = new RectangleF(
                carriedRect.X + carriedRect.Width + inventoryPadding,
                equippedRect.Y + equippedRect.Height + inventoryPadding,
                statTitleSize.Width, statTitleSize.Height);

            RectangleF statRect = new RectangleF(
                statTitleRect.X,
                statTitleRect.Y + statTitleRect.Height + inventoryPadding,
                equippedRect.Width,
                inventoryRect.Height - titleRect.Height - equippedRect.Height - statTitleRect.Height - inventoryPadding * 5);

            RectangleF statTextRect = new RectangleF(
                statRect.X + itemPadding,
                statRect.Y + itemPadding,
                statRect.Width - itemPadding*2,
                statRect.Height - itemPadding*2);
            
            gfx.FillRectangle(background, inventoryRect);
            gfx.FillRectangle(inventoryBackground, carriedRect);
            gfx.FillRectangle(inventoryBackground, equippedRect);
            gfx.FillRectangle(inventoryBackground, statRect);

            gfx.DrawString(titleText, titleFont, Brushes.WhiteSmoke, titleRect);
            gfx.DrawString(statTitleText, bigFont, Brushes.WhiteSmoke, statTitleRect);
            gfx.DrawString(statsText, bigStatsFont, Brushes.WhiteSmoke, statTextRect);

            for (int y = 0; y < content[0].GetLength(0); y++) {               //Draw carried
                for (int x = 0; x < content[0].GetLength(1); x++) {

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

                    Item item = content[0][y, x];

                    if (item != null) {
                        RectangleF srcRect = new RectangleF(item.imageIndex % 3 * 32, item.imageIndex / 3 * 32, 32, 32);
                        gfx.DrawImage(Item.itemImage, itemRect, srcRect, GraphicsUnit.Pixel);
                    }
                }
            }

            for (int y = 0; y < content[1].GetLength(0); y++) {               //Draw Equipped
                for (int x = 0; x < content[1].GetLength(1); x++) {

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

                    Item item = content[1][y, x];

                    if (item != null) {
                        RectangleF srcRect = new RectangleF(item.imageIndex % 3 * 32, item.imageIndex / 3 * 32, 32, 32);
                        gfx.DrawImage(Item.itemImage, itemRect, srcRect, GraphicsUnit.Pixel);
                    }
                }
            }

            Item selectedItem = content[activeContainer][selectedRow, selectedColumn];
            
            if (selectedItem != null) {
                string name = selectedItem.itemName;
                string lvl = "Level: " + selectedItem.itemLVL.ToString();

                string stats = "";
                if (selectedItem.itemHP != 0) stats += $"Health: {selectedItem.itemHP.ToString("0.00")}\n";
                if (selectedItem.itemDEF != 0) stats += $"Armor: {selectedItem.itemDEF.ToString("0.00")}\n";
                if (selectedItem.itemDMG != 0) stats += $"Damage: {selectedItem.itemDMG.ToString("0.00")}\n";
                if (selectedItem.itemPENE != 0) stats += $"Armor Penetration: {selectedItem.itemPENE.ToString("0.00")}\n";
                if (selectedItem.itemSPEED != 0) stats += $"Slow: {selectedItem.itemSPEED.ToString("0.00")}\n";

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
                SizeF tooltipSize = new SizeF(
                    330,
                    sizeName.Height + sizeLevel.Height + sizeStats.Height + sizeFlavor.Height + 10
                    );
                PointF tooltipPoint = new PointF(
                    Math.Min(selectedRect.X + selectedRect.Width, screenWidth - tooltipSize.Width),
                    Math.Min(selectedRect.Y + selectedRect.Height, screenHeight - tooltipSize.Height)
                    );
                
                RectangleF tooltipRect = new RectangleF(tooltipPoint, tooltipSize);

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

            foreach (Item item in content[1]) {
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
