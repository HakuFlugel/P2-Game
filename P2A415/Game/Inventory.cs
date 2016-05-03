using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RPGame {


    public class Inventory {

        private class itemindex {
            public int index;
            public int indey;
            public float X;
            public float Y;
            public Items item { get; private set; }
            
            public itemindex(int indey, int index) {
                this.index = index;
                this.indey = indey;
            }

            public void setItem(Items item) {

                
                this.item = item;
               
            }
                
        }

        private Game game;
        private bool isEquipped = false;

        static List<RectangleF> itemsInInventry = new List<RectangleF>();
        private itemindex[][] totalCarried = new itemindex[8][];
        private itemindex[][] totalEquipped = new itemindex[4][];

        private int selectedX = 0;
        private int selectedY = 0;

        public bool isOpen { get; private set; } = false;

        private Bitmap menuImage;
        private Bitmap buttonImage;
        

        private int totalNrOfInventory = (8*8) + 8; //64 items in inventory + 8 equipped.


        List<Items> Carried_sorted;
        static List<Items> Carried = new List<Items>();
        static List<Items> Eqipped = new List<Items>();

        ToolTip bob = new ToolTip();

        public Inventory(Game game) {
            int lengthCarried = totalCarried.Length;
            int lengthEquipped = totalEquipped.Length;

            for (int i = 0; i < lengthCarried; i++)
                totalCarried[i] = new itemindex[9];
            for (int i = 0; i < lengthEquipped; i++)
                totalEquipped[i] = new itemindex[3];

            this.game = game;
            menuImage = ImageLoader.Load("Content/TransBlackground.png");

            for(int indey = 0; indey < 8; indey++) {
                for (int index = 0; index < 8; index++)
                    totalCarried[indey][index] = new itemindex(indey, index);

            }

            for(int indey = 0; indey < 4;indey++) {
                for (int index = 0; index < 3; index++)
                    totalEquipped[indey][index] = new itemindex(indey, index);
            }

        }

        public void keyInput(KeyEventArgs e) {
            switch (e.KeyCode) {

                case Keys.W:
                case Keys.Up:
                    if (--selectedY < 0) {
                        selectedY = 0;
                    }
                    break;

                case Keys.S:
                case Keys.Down:
                    if (++selectedY > 7) {
                        selectedY = 7;
                    }
                    break;

                case Keys.A:
                case Keys.Left:
                    if (--selectedX < 0)
                        selectedX = 0;
                    break;

                case Keys.D:
                case Keys.Right:
                    if (++selectedX > 8)
                        selectedX = 8;
                    break;

                
                case Keys.Enter:
                    Console.WriteLine("Enter is pressed");
                    Console.WriteLine(tryEquip()); 
                    break;
                case Keys.Control:
                    Toggle_menu();
                    break;

                default:
                    break;
            }
            Console.WriteLine("selectedx : " + selectedX + "   selectedy : " + selectedY);
        }
 
        public int tryEquip() {

            if(isEquipped) {
                if (selectedX == 8)
                    if (AreYouSure("unequip")) {
                        if (totalCarried[selectedY][selectedX].item != null) {
                            GetItem(totalCarried[selectedY][selectedX].item);

                            Eqipped.Remove(totalCarried[selectedY][selectedX].item);
                        }


                        return 0;
                    }
            }else {

                int itterate = 0;
                foreach (var item in Eqipped) {
                    if (item != null && totalCarried[selectedY][selectedX].item != null && totalCarried[selectedY][selectedX].item.equipSlot.Equals(item.equipSlot)) {
                        Eqipped[itterate] = totalCarried[selectedY][selectedX].item;

                        Carried.Remove(totalCarried[selectedY][selectedX].item);
                        totalCarried[selectedY][selectedX].setItem(null);

                        return 1;
                    }
                    itterate++;
                }
                if (totalCarried[selectedY][selectedX].item != null) {
                    itterate = 0;
                    Eqipped.Add(totalCarried[selectedY][selectedX].item);
                    Carried.Remove(totalCarried[selectedY][selectedX].item);
                    totalCarried[selectedY][selectedX].setItem(null);
                    return 2;
                }

            }

            

                  
            
            return -10;
        }

        public void toggle() {
            isOpen = !isOpen;
            selectedX = 0;
            selectedY = 0;  
        }

        public bool AreYouSure(string text) {


            return true;
        }

        public void GetItem(Items item) {

            Carried.Add(item);


            for(int index = 0; index < 8; index++) 

                for(int indey = 0; indey < 8; indey++) 

                    if (totalCarried[index][indey].item == null) {

                        totalCarried[index][indey].setItem(item);

                        return;
                    }
        }

        private void Toggle_menu() {
            isEquipped = !isEquipped;
            selectedX = 0;
            selectedY = 0;
        }

        public void draw(Graphics gfx) {


            DrawCarried(gfx);



            Font namefont = new Font("Arial", 15, FontStyle.Bold), 
                 lvlfont = new Font("Arial", 10, FontStyle.Regular), 
                 statsfont = new Font("Arial", 12, FontStyle.Regular), 
                 flavortextFont = new Font("Arial", 12, FontStyle.Italic);
            
            float textPositionX = 0, textPositionY = 0;

            Carried_sorted = Carried.OrderBy(ch => ch.itemName).ToList();

            int carriedCount = Carried.Count;
            //for (int index = 0; index < carriedCount; index++) {
            //    gfx.DrawImage(ImageLoader.Load(Carried_sorted[index].itemImageFile),
            //    new RectangleF(itemsInInventry[index].X, itemsInInventry[index].Y, itemsInInventry[index].Width, itemsInInventry[index].Height),
            //    new Rectangle(32*index, 0, 64, 64), GraphicsUnit.Pixel);
            //}
            int xdex = 0;
            int y=0, x=0;
            for (int ypp = 0; ypp < 8; ypp++) 
                for (int xpp = 0; xpp < 8; xpp++)
                    totalCarried[ypp][xpp].setItem(null);

            foreach (var item in Carried_sorted) {
                totalCarried[y][x].setItem(item);
                
                x++;
                if (x == 8) { x = 0; y++; }
            }
            y = 0;
            

            if(totalCarried[selectedY][selectedX].item != null) {
                Items item = totalCarried[selectedY][selectedX].item;
                int index = xdex - 1;

                string name = item.itemName,
                    lvl = "Level: " + item.itemLVL.ToString(),
                    stats = "Health: " + item.itemHP.ToString() + Environment.NewLine +
                    "Damage: " + item.itemDMG.ToString() + Environment.NewLine +
                    "Defence: " + item.itemDEF.ToString();
                string flavortext = "";
                if (item.flavortext != null) {
                    flavortext = item.flavortext;
                }

                textPositionX = totalCarried[selectedY][selectedX].X + 65;
                textPositionY = totalCarried[selectedY][selectedX].Y + 65;


                int heightOfItAll = (int)(gfx.MeasureString(name, namefont).Height +
                                    gfx.MeasureString(lvl, lvlfont).Height +
                                    gfx.MeasureString(stats, statsfont).Height +
                                    gfx.MeasureString(flavortext, flavortextFont).Height + 10);

                gfx.FillRectangle(new SolidBrush(Color.DarkSalmon), new RectangleF(textPositionX-5,textPositionY-5, 200, heightOfItAll));


               


                gfx.DrawString(name, namefont, Brushes.WhiteSmoke,
                    new RectangleF(new PointF(textPositionX, textPositionY),
                    new SizeF(190, gfx.MeasureString(item.itemName, namefont).Height)));

                gfx.DrawString(lvl, lvlfont, Brushes.WhiteSmoke,
                    textPositionX + 5, textPositionY += (int)gfx.MeasureString(item.itemName, namefont, 190).Height + 5);

                gfx.DrawString(stats, statsfont, Brushes.WhiteSmoke,
                    textPositionX + 5, textPositionY += (int)gfx.MeasureString(item.itemLVL.ToString(), lvlfont, 190).Height);


                gfx.DrawString(flavortext, flavortextFont, Brushes.WhiteSmoke,
                    new RectangleF(new PointF(textPositionX += (int)gfx.MeasureString(item.itemDEF.ToString(), statsfont, 190).Height + 5, textPositionY + 2),
                    new SizeF(190, gfx.MeasureString(item.itemName, namefont).Height)));

            }
        }


        public void DrawCarried(Graphics gfx) {

            int width = (int)(game.Width / 1.2), height = (int)(game.Height / 1.2);
            int placex = game.Width / 2 - width / 2;
            int placey = game.Height / 2 - height / 2;

            Font font = new Font("Bradley Hand ITC", 40, FontStyle.Italic);
            gfx.DrawImage(menuImage, new Rectangle(0,0,game.Width,game.Height), new Rectangle(0,0,1,1), GraphicsUnit.Pixel);
            gfx.FillRectangle(new SolidBrush(Color.DarkGray), new Rectangle(placex, placey, width, height));
            
            float xdex = placex + 72, ydex = placey + 70, outterboxWid = 60, outterboxHei = 60,tempxdex = 0, tempydex = 0;
            SizeF stringSize = gfx.MeasureString("Equipped", font);
            
            gfx.DrawString("Inventory", font, Brushes.Black,new Point(placex + 2,placey + 5));
            gfx.DrawString("Equipped", font, Brushes.Black, new PointF(width - stringSize.Width - 5, placey + 5));

            float equippedSlots = height + outterboxHei;

            for (int indey = 0; indey < 8; indey++) {

                for (int index = 0; index < 8; index++) {
                    if(index % 8 == 0 && index != 0) {
                        xdex = placex + 72;
                        ydex += 70;
                    }
                    

                    totalCarried[indey][index].X = xdex + 2;
                    totalCarried[indey][index].Y = ydex + 2;

                    if (indey == selectedY && index == selectedX) {
                        gfx.FillRectangle(new SolidBrush(Color.Orange), new RectangleF(xdex, ydex, outterboxWid + 5, outterboxHei + 5));
                        gfx.FillRectangle(new SolidBrush(Color.GhostWhite), new RectangleF(xdex + 2, ydex + 2, outterboxWid - 4, outterboxWid - 4));
                    } else {
                        gfx.FillRectangle(new SolidBrush(Color.Black), new RectangleF(xdex, ydex, outterboxWid, outterboxHei));
                        gfx.FillRectangle(new SolidBrush(Color.GhostWhite), new RectangleF(xdex + 2, ydex + 2, outterboxWid - 4, outterboxWid - 4));
                    }
                    xdex += 70;
                    
                }
            }


        }
        
        
        public void DrawEqipped(Graphics gfx) {

        }

    }
}
