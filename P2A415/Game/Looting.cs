using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RPGame {
    public class Looting {
        public ulong exp = 0;
        public int lvl = 0;
        public int monsterLvl;
        public bool isOpen = false;
        private bool hasDone = false;

        Item[] gained_items = new Item[3];

        private Brush background = new SolidBrush(Color.FromArgb(128, Color.Black));
        private Brush lootBackground = new SolidBrush(Color.FromArgb(128, Color.Black));
        private Brush itemBackground = new SolidBrush(Color.FromArgb(192, Color.WhiteSmoke));
        private Brush itemSelectedBackground = new SolidBrush(Color.FromArgb(192, Color.Orange));

        private Font font = new Font("Bradley Hand ITC", 40, FontStyle.Italic);

        public Looting() {

        }

        public void show(ulong exp, int lvl, int monsterLvl) {
            this.exp = exp;
            this.lvl = lvl;
            this.monsterLvl = monsterLvl;
            isOpen = true;
        }

        public void hide() {
            isOpen = false;
            hasDone = false;
        }

        public Item[] generateLoot(int lvl) {

            List<Item> items = Item.itemTypes;
            Item[] genertedItem = new Item[3];
            Random rand = new Random(Guid.NewGuid().GetHashCode());



            for (int index = 0; index < 3; index++) {
                
                int whatItem = rand.Next(0, 9);                                                     // Whict type of item.
                switch (rand.Next(0, 11)) {                                                         // What lvl of item, and if there even is going to be an item

                    case 1: case 2: case 3: case 4:                                                 // No item   
                                                         
                        break;
                    case 5: case 6: case 7: case 8:                                                 //Basic Item

                        genertedItem[index] = (new Item(items[whatItem], rand.Next(lvl - 10, lvl + 3)));
                        break;
                    case 9: case 10:                                                                //Better item, less chance.
                        genertedItem[index] = (new Item(items[whatItem], rand.Next(lvl - 5, lvl + 6)));
                    break;

                    default:                                                                        //Best item, much less chance.
                        genertedItem[index] = (new Item(items[whatItem], rand.Next(lvl - 1, lvl + 10)));
                        break;
                }
            }
            hasDone = true;
            return genertedItem;
        }

        public void keyInput(KeyEventArgs e) {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Space)
                hide();
        }



        public void draw(Graphics gfx, Game game) {
            int screenWidth = game.ClientSize.Width, screenHeight = game.ClientSize.Height;

            

            const int itemSize = 128;
            const int itemPadding = 10;

            





            float lootWidth = game.ClientSize.Width * 0.50f, lootHeight = game.ClientSize.Height * 0.50f;
            float lootplaceX = game.ClientSize.Width / 2 - lootWidth / 2, lootplaceY = game.ClientSize.Height / 2 - lootHeight / 2;
            

            PointF OutterBoxPoint = new PointF(lootplaceX + lootWidth * 0.1f, lootplaceY + lootHeight / 2);
            SizeF OutterBoxSize = new SizeF(lootplaceX / 3, lootplaceX / 3);
            PointF InnerBoxPoint = new PointF(OutterBoxPoint.X + 5, OutterBoxPoint.Y + 5);
            SizeF InnerBoxSize = new SizeF(lootplaceX / 3 - 10, lootplaceX / 3 - 10);

            float the_addition = OutterBoxSize.Width + lootWidth / 6.5f;

            if(!hasDone) {
                gained_items = generateLoot(monsterLvl);


                if (gained_items != null)
                    foreach (var item in gained_items)
                        Console.WriteLine(game.localPlayer.character.inventory.addItem(item));

            }


            gfx.FillRectangle(background, new RectangleF(new PointF(0,0),new SizeF(game.ClientSize.Width,game.ClientSize.Height)));
            gfx.FillRectangle(lootBackground,new RectangleF(lootplaceX, lootplaceY, lootWidth, lootHeight));
            float setX = lootplaceX + 5, setY = lootplaceY + lootHeight * 0.1f;


            gfx.DrawString("Experience: " + exp,font,Brushes.Wheat,setX, setY);
            if(lvl != 0) {
                SizeF experienceString = gfx.MeasureString("Experience: " + exp, font);
                if (experienceString.Width + gfx.MeasureString("Gained levels: " + lvl, font).Width > lootWidth)
                    setY += experienceString.Height;
                else
                    setX += experienceString.Width + 5;


                gfx.DrawString("Gained levels: " + lvl, font, Brushes.Wheat, setX, setY);
            }

            for (int index = 0; index < 3; index++) {
                RectangleF itemRect = new RectangleF(
                        OutterBoxPoint.X + itemPadding + index * (itemSize + itemPadding),
                        OutterBoxPoint.Y + itemPadding,
                        itemSize, itemSize);

                gfx.FillRectangle(itemBackground, new RectangleF(OutterBoxPoint, OutterBoxSize));

                if (gained_items[index] != null) {
                    RectangleF srcRect = new RectangleF(gained_items[index].imageIndex % 3 * 32, gained_items[index].imageIndex / 3 * 32, 32, 32);
                    gfx.DrawImage(Item.itemImage, new RectangleF(InnerBoxPoint,InnerBoxSize), srcRect, GraphicsUnit.Pixel);
                }
                    
                OutterBoxPoint.X += the_addition;
                InnerBoxPoint.X += the_addition;
            }
        }
    }
}
