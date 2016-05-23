using System;
using System.Collections.Generic;
using System.Drawing;

namespace RPGame {
    public class Looting {
        public ulong gainedXp = 0;
        public int gainedLvl = 0;
        public int monsterLvl = 0;

        private Brush background = new SolidBrush(Color.FromArgb(128, Color.Black));
        private Brush lootBackground = new SolidBrush(Color.FromArgb(128, Color.Black));
        private Brush itemBackground = new SolidBrush(Color.FromArgb(192, Color.WhiteSmoke));

        Item[] gainedItems = new Item[3];

        private Font font = new Font("Bradley Hand ITC", 40, FontStyle.Italic);

        private static Random rand = new Random();

        public Looting(Game game, int gainedLvl, ulong gainedXp, int monsterLvl) {
            this.gainedXp = gainedXp;
            this.gainedLvl = gainedLvl;
            this.monsterLvl = monsterLvl;


            List<Item> items = Item.itemTypes;

            for (int index = 0; index < 3; index++) {
                int whichDrop = rand.Next(0, 3);
                if (whichDrop == 0) {
                    gainedItems[index] = (new Item(items[rand.Next(0, 5)], rand.Next(monsterLvl - 10, monsterLvl)));
                } else if (whichDrop == 1) {
                    gainedItems[index] = (new Item(items[rand.Next(6, 13)], rand.Next(monsterLvl - 10, monsterLvl)));
                } else {

                    game.localPlayer.character.addExperience(game, (ulong)(Math.Pow(gainedLvl, 1.14) * 1.1 + 5));
                }
                if (gainedItems[index] != null) {
                    if (!game.localPlayer.inventory.addItem(gainedItems[index])) {
                        game.popupMessage = new PopupMessage("Inventory full");
                    }
                }
                if (game.localPlayer.tutorial.firstVictory) {
                    game.popupMessage = new PopupMessage("Loot Tutorial", 
                        new PopupMessage(ImageLoader.Load("Content/LooExplain1.png")));
                    game.localPlayer.tutorial.firstVictory = false;
                }
            }
        }

        public void draw(Graphics gfx, Game game) {
           
            int screenWidth = game.ClientSize.Width, screenHeight = game.ClientSize.Height;

            const int itemSize = 128;
            const int itemPadding = 16;
            
            float lootWidth = game.Width * 0.50f, lootHeight = game.Height * 0.50f;
            float lootplaceX = game.Width / 2 - lootWidth / 2, lootplaceY = game.Height / 2 - lootHeight / 2;

            PointF OutterBoxPoint = new PointF(lootplaceX + lootWidth * 0.1f, lootplaceY + lootHeight / 2);
            SizeF OutterBoxSize = new SizeF(lootplaceX / 3, lootplaceX / 3);
            PointF InnerBoxPoint = new PointF(OutterBoxPoint.X + 5, OutterBoxPoint.Y + 5);
            SizeF InnerBoxSize = new SizeF(lootplaceX / 3 - 10, lootplaceX / 3 - 10);

            SizeF itemSizes = new SizeF(
                (itemSize + itemPadding) * 5 + itemPadding,
                itemSize + 2 * itemPadding);

            string experience = "Experience: " + gainedXp.ToString() + ((gainedLvl != 0 ) ? Environment.NewLine + "Level raised: " + gainedLvl.ToString() : "");
            SizeF experienceSize = gfx.MeasureString(experience, font);

            SizeF lootSize = new SizeF(
                itemSizes.Width + experienceSize.Width + 5 * itemPadding,
                itemSizes.Height + experienceSize.Height + 5 * itemPadding);

            RectangleF lootRect = new RectangleF(
               (screenWidth - lootSize.Width) / 2,
               (screenHeight - lootSize.Height) / 2,
               lootSize.Width, lootSize.Height);

            SizeF allLootSize = new SizeF(
                itemSizes.Width +  Math.Max(5 * itemPadding, experienceSize.Width),
                itemSizes.Height + experienceSize.Height + itemPadding + 2 * itemPadding);

            RectangleF allLootRect = new RectangleF(
               (screenWidth - allLootSize.Width) / 2,
               (screenHeight - lootRect.Height) / 2,
               allLootSize.Width, lootRect.Height);

            RectangleF stringRect = new RectangleF(
                allLootRect.X + itemPadding,
                allLootRect.Y + itemPadding,
                experienceSize.Width,experienceSize.Height
                );

            RectangleF itemGainedRect = new RectangleF(
               (screenWidth - itemSizes.Width) / 2,
               allLootRect.Y + 2 * itemPadding + experienceSize.Height,
               itemSizes.Width, itemSizes.Height);

            gfx.FillRectangle(background, new RectangleF(new PointF(0, 0), new SizeF(game.Width, game.Height)));
            gfx.FillRectangle(lootBackground, allLootRect);
            gfx.FillRectangle(lootBackground, itemGainedRect);
            gfx.DrawString(experience,font,Brushes.Wheat,stringRect);

            for (int index = 0; index < 3; index++) {
                RectangleF itemRect = new RectangleF(
                        itemGainedRect.X + itemPadding + index*2 * (itemSize + itemPadding),
                        itemGainedRect.Y + itemPadding,
                        itemSize, itemSize);

                gfx.FillRectangle(itemBackground, itemRect);

                if (gainedItems[index] != null) {
                    RectangleF srcRect = new RectangleF(gainedItems[index].imageIndex % 3 * 32, gainedItems[index].imageIndex / 3 * 32, 32, 32);
                    gfx.DrawImage(Item.itemImage, itemRect, srcRect, GraphicsUnit.Pixel);
                }
            }
        }
    }
}
