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

        static List<RectangleF> itemsInInventry = new List<RectangleF>();
        private itemindex[][] totalItems = new itemindex[8][];

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
            int length = totalItems.Length;
            for (int i = 0; i < length; i++)
                totalItems[i] = new itemindex[9];

            this.game = game;
            menuImage = ImageLoader.Load("Content/TransBlackground.png");

            for(int indey = 0; indey < 8; indey++) {
                for (int index = 0; index < 9; index++)
                    totalItems[indey][index] = new itemindex(indey, index);

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

                default:
                    break;
            }
            Console.WriteLine("selectedx : " + selectedX + "   selectedy : " + selectedY);
        }
 
        public int tryEquip() {

            if (selectedX == 8)
                if (AreYouSure("unequip")) {
                    if(totalItems[selectedY][selectedX].item != null) {
                        GetItem(totalItems[selectedY][selectedX].item);

                        Eqipped.Remove(totalItems[selectedY][selectedX].item);
                    }
                    

                    return 0;
                } 

            int itterate = 0;
            foreach(var item in Eqipped) {
                if (item != null && totalItems[selectedY][selectedX].item != null && totalItems[selectedY][selectedX].item.equipSlot.Equals(item.equipSlot)) {
                    Eqipped[itterate] = totalItems[selectedY][selectedX].item;

                    Carried.Remove(totalItems[selectedY][selectedX].item);
                    totalItems[selectedY][selectedX].setItem(null);
                    
                    return 1;
                }
                itterate++;
            }
            if(totalItems[selectedY][selectedX].item != null) {
                itterate = 0;
                Eqipped.Add(totalItems[selectedY][selectedX].item);
                Carried.Remove(totalItems[selectedY][selectedX].item);
                totalItems[selectedY][selectedX].setItem(null);
                return 2;
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

                    if (totalItems[index][indey].item == null) {

                        totalItems[index][indey].setItem(item);

                        return;
                    }
        }

        public void draw(Graphics gfx) {

            DrawInvi(gfx);

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
                for (int xpp = 0; xpp < 9; xpp++)
                    totalItems[ypp][xpp].setItem(null);

            foreach (var item in Carried_sorted) {
                totalItems[y][x].setItem(item);
                
                x++;
                if (x == 8) { x = 0; y++; }
            }
            y = 0;
            foreach(var item in Eqipped) {
                totalItems[y][8].setItem(item);
                y++;
            }

            if(totalItems[selectedY][selectedX].item != null) {
                Items item = totalItems[selectedY][selectedX].item;
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

                textPositionX = totalItems[selectedY][selectedX].X + 65;
                textPositionY = totalItems[selectedY][selectedX].Y + 65;


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


        public void DrawInvi(Graphics gfx) {
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
                for (int index = 0; index < 9; index++) {
                    
                    if(index == 8) {
                        tempxdex = xdex;
                        tempydex = ydex;
                        xdex = width - outterboxWid - 5;
                        
                    }

                    totalItems[indey][index].X = xdex + 2;
                    totalItems[indey][index].Y = ydex + 2;

                    if (indey == selectedY && index == selectedX) {
                        gfx.FillRectangle(new SolidBrush(Color.Orange), new RectangleF(xdex, ydex, outterboxWid+5, outterboxHei+5));
                        gfx.FillRectangle(new SolidBrush(Color.GhostWhite), new RectangleF(xdex + 2, ydex + 2, outterboxWid - 4, outterboxWid - 4));
                    } else {
                        gfx.FillRectangle(new SolidBrush(Color.Black), new RectangleF(xdex, ydex, outterboxWid, outterboxHei));
                        gfx.FillRectangle(new SolidBrush(Color.GhostWhite), new RectangleF(xdex + 2, ydex + 2, outterboxWid - 4, outterboxWid - 4));
                    }
                    xdex += 70;
                    if (index == 8) {
                        xdex = placex + 72;
                        ydex = tempydex + height / 6 - 40;
                    }
                    
                }
                    

            }



            //for (int index = 0; index < 8; index++) {
            //    gfx.FillRectangle(new SolidBrush(Color.Black), new RectangleF(width-outterboxWid, equippedSlots -= 70, outterboxWid,outterboxHei));
            //    gfx.FillRectangle(new SolidBrush(Color.GhostWhite), new RectangleF(width - outterboxWid + 2, equippedSlots + 2, outterboxWid - 4, outterboxWid - 4));
            //}


            //for (int index = 0; index < 32;index++) {
            //    if(index % 8 == 0) { xdex = placex + 2; ydex += height / 4 - 40; }
            //    gfx.FillRectangle(new SolidBrush(Color.Black), new RectangleF(xdex ,ydex , outterboxWid, outterboxHei));
            //    gfx.FillRectangle(new SolidBrush(Color.GhostWhite), new RectangleF(xdex + 2, ydex + 2, outterboxWid - 4, outterboxWid - 4));

            //    itemsInInventry.Add(new RectangleF(xdex, ydex, 51, 51));

            //    xdex += 70;
            //}
            
        }


        //private string StringHandler(string str) {

        //    string[] split_string = str.Split(' ');
        //    int nrOfStrings = split_string.Count();

        //    StringBuilder sb = new StringBuilder();

        //    if (nrOfStrings > 4)
        //        throw new Exception("Too long name");

        //    if (nrOfStrings == 1) {

        //        font.Size -= 1;

        //    } else {
        //        Optimal_string_editor(split_string, nrOfStrings);

        //        foreach(string strinb in split_string) {
                    
        //            sb.Append(strinb);
        //        }
                
        //    }
        //    return sb.ToString();
        //}

        //private void Optimal_string_editor(string[] str,int nrOfStrings) {

        //    switch (nrOfStrings) {

        //        case 5:
        //            if (str[0].Count() > str[1].Count() + str[2].Count() + str[3].Count() + str[4].Count()) {
        //                str[5] = str[4];
        //                str[4] = str[3];
        //                str[3] = str[2];
        //                str[2] = str[1];
        //                str[1] = Environment.NewLine;
        //            } else if (str[0].Count() + str[1].Count() > str[2].Count() + str[3].Count() + str[4].Count()) {
        //                str[5] = str[4];
        //                str[4] = str[3];
        //                str[3] = str[2];
        //                str[2] = Environment.NewLine;
        //            } else if (str[0].Count() + str[1].Count() + str[2].Count() > str[3].Count() + str[4].Count()) {
        //                str[5] = str[4];
        //                str[4] = str[3];
        //                str[3] = Environment.NewLine;
        //            } else {
        //                str[5] = str[4];
        //                str[4] = Environment.NewLine;
        //            }
        //            break;

        //        case 4:
        //            if (str[0].Count() > str[1].Count() + str[2].Count() + str[3].Count()) {
        //                str[4] = str[3];
        //                str[3] = str[2];
        //                str[2] = str[1];
        //                str[1] = Environment.NewLine;
        //            } else if (str[0].Count() + str[1].Count() + str[1].Count() > str[3].Count()) {
        //                str[4] = str[3];
        //                str[3] = Environment.NewLine;
        //            } else {
        //                str[4] = str[3];
        //                str[3] = str[2];
        //                str[3] = Environment.NewLine;
        //            }
        //            break;

        //        case 3:
        //            if (str[0].Count() > str[1].Count() + str[2].Count()) {
        //                str[3] = str[2];
        //                str[2] = str[1];
        //                str[1] = Environment.NewLine;
        //            } else {
        //                str[3] = str[2];
        //                str[2] = Environment.NewLine;
        //            }
        //            break;

        //        case 2:

        //            str[2] = str[1];
        //            str[1] = Environment.NewLine;

        //            break;

        //        default:
        //            break;
        //    }



        //}

        private void Inventory_MouseHover(object sender, EventArgs e, bool is_hover) {
            

        }
    }

}
