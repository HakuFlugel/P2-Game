using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFormsTest {


    public class Inventory {
        

        static List<RectangleF> itemsInInventry = new List<RectangleF>();
        List<Items> Carried_sorted;
        ToolTip bob = new ToolTip();
        static Inventory() {

            int PictureX = 50, PictureY = 200, Height = 20, Width = 20;

            for (int index = 0; index < 32; index++) {
                if (index % 8 == 0) {
                    PictureY += 60;
                    PictureX = 50;
                }

                
                PictureX += 25;
               
            }
        }
        
        



        public void draw(Graphics gfx, List<Items> Carried, List<Items> Equipped, bool is_on) {

            Font namefont = new Font("Arial", 15, FontStyle.Bold), 
                 lvlfont = new Font("Arial", 10, FontStyle.Regular), 
                 statsfont = new Font("Arial", 12, FontStyle.Regular), 
                 flavortextFont = new Font("Arial", 12, FontStyle.Italic);

            SizeF stringSize = new SizeF();
            int textPositionX = 0, textPositionY = 0;

            Carried_sorted = Carried.OrderBy(ch => ch.itemName).ToList();

            int carriedCount = Carried.Count;
            //for (int index = 0; index < carriedCount; index++) {
            //    gfx.DrawImage(ImageLoader.Load(Carried_sorted[index].itemImageFile),
            //    new Rectangle(itemsInInventry[index].X, itemsInInventry[index].Y, itemsInInventry[index].Width, itemsInInventry[index].Height),
            //    new Rectangle(0, 0, 64, 64), GraphicsUnit.Pixel);
            //}
            int xdex = 0;


            foreach(var item in itemsInInventry) {
                xdex++;
                if ((Cursor.Position.X > item.X && Cursor.Position.Y > item.Y) &&
                   (Cursor.Position.X < item.X + item.Width && Cursor.Position.Y < item.Y + item.Height)) {

                    if (carriedCount >= xdex) {
                        int index = xdex - 1;
                        gfx.FillRectangle(new SolidBrush(Color.DarkSalmon), new Rectangle(Cursor.Position, new Size(200, 500)));
                        if(!is_on) {
                            textPositionX = Cursor.Position.X + 5;
                            textPositionY = Cursor.Position.Y + 5;
                            is_on = true;
                        }

                        gfx.DrawString(Carried_sorted[index].itemName, namefont, Brushes.WhiteSmoke,
                            new RectangleF(new PointF(textPositionX, textPositionY),
                            new SizeF(190,gfx.MeasureString(Carried_sorted[index].itemName,namefont).Height)));

                        Console.WriteLine("Bobs brain = " + Carried_sorted[index].itemName);
                        gfx.DrawString("Level: " + Carried_sorted[index].itemLVL, lvlfont, Brushes.WhiteSmoke,
                            textPositionX + 5, textPositionY += (int)gfx.MeasureString(Carried_sorted[index].itemName, namefont, 190).Height + 5);

                        gfx.DrawString("Health: " + Carried_sorted[index].itemHP + Environment.NewLine + "Damage: " + Carried_sorted[index].itemDMG + Environment.NewLine + "Defence: " + Carried_sorted[index].itemDEF, statsfont, Brushes.WhiteSmoke, 
                            textPositionX +  5, textPositionY += (int)gfx.MeasureString(Carried_sorted[index].itemLVL.ToString(), lvlfont, 190).Height);
                 

                        //gfx.DrawString(Carried_sorted[index].itemFlavortext, flavortextFont,Brushes.WhiteSmoke,
                        //    new RectangleF(new PointF(textPositionX += (int)gfx.MeasureString(Carried_sorted[index].itemDEF.ToString(), statsfont, 190).Height + 5, textPositionY + 2),
                        //    new SizeF(190, gfx.MeasureString(Carried_sorted[index].itemName, namefont).Height)));
                            
                    }
                }
            }
        }


        public void DrawInvi(Graphics gfx) {
            int width = (int)(Game.instance.Width / 1.2), height = (int)(Game.instance.Height / 1.2);
            int placex = Game.instance.Width / 2 - width / 2;
            int placey = Game.instance.Height / 2 - height / 2;

            gfx.FillRectangle(new SolidBrush(Color.DarkGray), new Rectangle(placex, placey, width, height));

            float xdex = placex + 2, ydex = placey + 2, outterboxWid = 60, outtaboxHei = 60;

            for (int index = 0; index < 32;index++) {
                if(index % 8 == 0) { xdex = placex + 2; ydex += height / 4 - 40; }
                gfx.FillRectangle(new SolidBrush(Color.Black), new RectangleF(xdex ,ydex , outterboxWid, outtaboxHei));
                gfx.FillRectangle(new SolidBrush(Color.GhostWhite), new RectangleF(xdex + 2, ydex + 2, outterboxWid - 4, outterboxWid - 4));

                itemsInInventry.Add(new RectangleF(xdex, ydex, 51, 51));

                xdex += 60;
            }
            
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
