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
        
        static List<RectangleF> itemsInInventry = new List<RectangleF>();
        private itemindex[][] totalCarried = new itemindex[8][];
        private itemindex[][] totalEquipped = new itemindex[4][];

        int test_int = 2;

        private int selectedX = 0;
        private int selectedY = 0;

        public bool isOpen { get; private set; } = false;

        private SolidBrush Background = new SolidBrush(Color.FromArgb(128,Color.Black));
        private Bitmap EqippedImage;
       

        List<Items> Carried_sorted;
        static List<Items> Carried = new List<Items>();
        static List<Items> Equipped = new List<Items>();

        ToolTip bob = new ToolTip();

        public Inventory() {
            int lengthCarried = totalCarried.Length;
            int lengthEquipped = totalEquipped.Length;

            for (int i = 0; i < lengthCarried; i++)
                totalCarried[i] = new itemindex[9];
            for (int i = 0; i < lengthEquipped; i++)
                totalEquipped[i] = new itemindex[3];

            
            EqippedImage = ImageLoader.Load("Content/Character.png");
            

            for(int indey = 0; indey < 8; indey++) {
                for (int index = 0; index < 8; index++)
                    totalCarried[indey][index] = new itemindex(indey, index);

            }

            for(int indey = 0; indey < lengthEquipped;indey++) {
                for (int index = 0; index < 3; index++)
                    totalEquipped[indey][index] = new itemindex(indey, index);
            }

        }

        public void keyInput(KeyEventArgs e) {
            switch (e.KeyCode) {

                case Keys.Delete:
                case Keys.Back:
                    DeleteItem();
                    break;

                case Keys.Space:
                    Items item = new Items();
                    

                    GetItem(item.MakeItem(new Items() {
                        itemName = "Two Handed Sword",
                        itemHP = 1,
                        itemLVL = 1,
                        itemDMG = 1,
                        itemDEF = 0,
                        equipSlot = new Items.itemType {
                            Hands = 2
                        }
                    }, test_int++));
                    GetItem(item.MakeItem(new Items() {
                        itemName = "Chest Plate",
                        itemHP = 1,
                        itemLVL = 1,
                        itemDMG = 1,
                        itemDEF = 0,
                        equipSlot = new Items.itemType {
                            Chest = 1
                        }
                    }, test_int++));
                    GetItem(item.MakeItem(new Items() {
                        itemName = "Boots",
                        itemHP = 1,
                        itemLVL = 1,
                        itemDMG = 1,
                        itemDEF = 0,
                        equipSlot = new Items.itemType {
                            Boots = 1
                        }
                    }, test_int++));
                    break;

                case Keys.W:
                case Keys.Up:
                    if (--selectedY < 0) {
                        selectedY = 0;
                    }
                    
                    break;

                case Keys.S:
                case Keys.Down:

                    if (++selectedY > 7) 
                        selectedY = 7;
                    if (selectedX > 7)
                        selectedY = (selectedY > 3 ? 3 : selectedY);

                    break;

                case Keys.A:
                case Keys.Left:
                    if (--selectedX < 0)
                        selectedX = 0;
                    break;

                case Keys.D:
                case Keys.Right:
                    if (++selectedX > 10)
                        selectedX = 10;
                    if (selectedX > 7)
                        selectedY = (selectedY > 3 ? 3 : selectedY);
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


        if (selectedX > 7)
            if (GeneralQuestion("unequip")) {
                if (totalEquipped[selectedY][selectedX - 8].item != null) {
                    if (!GetItem(totalEquipped[selectedY][selectedX - 8].item)) {
                            GeneralMessage("Inventory full");
                            return -20;
                        }
                        

                    Equipped.Remove(totalEquipped[selectedY][selectedX - 8].item);
                }
                

            return 0;
                    
        }
            Items[] y_x = new Items[2];
            int itterate = 0,count = 0;
            if (totalCarried[selectedY][selectedX].item != null) {
                foreach (var item in Equipped) {

                if (item != null && totalCarried[selectedY][selectedX].item != null && totalCarried[selectedY][selectedX].item.equipSlot.Equals(item.equipSlot)) {
                    y_x[count] = item;                    
                    count++;  
                }

                itterate++;
            }
            
                if (count == 2 && (totalCarried[selectedY][selectedX].item.equipSlot.Hands == 1 || totalCarried[selectedY][selectedX].item.equipSlot.RingSlot == 1)) {

                    Items wurstItem = y_x[0];

                    foreach (var items in y_x) 
                        if (wurstItem.itemLVL > items.itemLVL) 
                            wurstItem = items;
                        
                    
                    Equipped.Add(totalCarried[selectedY][selectedX].item);
                    Carried.Remove(totalCarried[selectedY][selectedX].item);

                    Carried.Add(wurstItem);
                    Equipped.Remove(wurstItem);

                    return 1;

                } else if (totalCarried[selectedY][selectedX].item.equipSlot.Hands == 2) {
                    
                    foreach (var items in Equipped) 
                        if (items.equipSlot.Hands != 0) 
                            Carried.Add(items);
        
                    
                    Equipped.Add(totalCarried[selectedY][selectedX].item);
                    Carried.Remove(totalCarried[selectedY][selectedX].item);
                    Equipped.RemoveAll(item => item.equipSlot.Hands != 0);

                } else if (count == 2 && totalCarried[selectedY][selectedX].item.equipSlot.Hands == 2) {

                    foreach (var items in y_x) {
                        Carried.Add(items);
                        Equipped.Remove(items);
                    }

                    Equipped.Add(totalCarried[selectedY][selectedX].item);
                    Carried.Remove(totalCarried[selectedY][selectedX].item);

                    return 2;

                } else if (count == 1 && !(totalCarried[selectedY][selectedX].item.equipSlot.Hands == 1 || totalCarried[selectedY][selectedX].item.equipSlot.RingSlot == 1)) {

                    Equipped.Add(totalCarried[selectedY][selectedX].item);
                    Carried.Remove(totalCarried[selectedY][selectedX].item);

                    Carried.Add(y_x[0]);
                    Equipped.Remove(y_x[0]);

                    return 3;
                }

                int countingHands = 0;
                foreach(var item in Equipped) 
                    countingHands += item.equipSlot.Hands;
                
                if(countingHands + totalCarried[selectedY][selectedX].item.equipSlot.Hands > 2) {
                    Carried.Add(Equipped.Find(x => x.equipSlot.Hands != 0));
                    Equipped.RemoveAll(x => x.equipSlot.Hands != 0);
                }
                
                Equipped.Add(totalCarried[selectedY][selectedX].item);
                Carried.Remove(totalCarried[selectedY][selectedX].item);

                return 4;
                
            }
            return -10;
        } 

        public void toggle(Game game) {
            if (!isOpen)
                game.localPlayer.character.calculateStats();
            isOpen = !isOpen;
            selectedX = 0;
            selectedY = 0;  
        }

        public bool GeneralQuestion(string text) {


            return true;
        }

        public void GeneralMessage(string text) {

        }

        public void DeleteItem() {
            if (selectedX > 7) {
                if (totalEquipped[selectedY][selectedX - 8].item != null)
                    if(GeneralQuestion("Delete Equipped?"))
                        Equipped.Remove(totalEquipped[selectedY][selectedX - 8].item);
            } else {
                if (totalCarried[selectedY][selectedX].item != null)
                    if(GeneralQuestion("Delete Carried?"))
                    Carried.Remove(totalCarried[selectedY][selectedX].item);
            }
                

             
            
        }

        public bool GetItem(Items item) {

            Carried.Add(item);
            if (Carried.Count > 63)
                return false;


            for(int index = 0; index < 8; index++) 

                for(int indey = 0; indey < 8; indey++) 

                    if (totalCarried[index][indey].item == null) {

                        totalCarried[index][indey].setItem(item);

                        return true;
                    }
            return false;
        }

        public void draw(Graphics gfx, Game game) {

            DrawCarried(gfx, game);



            Font namefont = new Font("Arial", 15, FontStyle.Bold), 
                 lvlfont = new Font("Arial", 10, FontStyle.Regular), 
                 statsfont = new Font("Arial", 12, FontStyle.Regular), 
                 flavortextFont = new Font("Arial", 12, FontStyle.Italic);
            
            float textPositionX = 0, textPositionY = 0;

            Carried_sorted = Carried.OrderBy(ch => ch.itemName).ToList();

            int carriedCount = Carried.Count;
            
            int xdex = 0;
            int y=0, x=0;
            for (int ypp = 0; ypp < 8; ypp++) 
                for (int xpp = 0; xpp < 8; xpp++)
                    totalCarried[ypp][xpp].setItem(null);

            for (int ypp = 0; ypp < 4; ypp++)
                for (int xpp = 0; xpp < 3; xpp++)
                    totalEquipped[ypp][xpp].setItem(null);

            foreach (var item in Carried_sorted) {
                totalCarried[y][x].setItem(item);
                x++;
                if (x == 8) { x = 0; y++; }
            }

            for(int ypp = 0; ypp < 8; ypp++) 
                for(int xpp = 0; xpp < 8; xpp++) 
                    if(totalCarried[ypp][xpp].item != null)
                        gfx.DrawImage(totalCarried[ypp][xpp].item.itemImageFile, new RectangleF(totalCarried[ypp][xpp].X, totalCarried[ypp][xpp].Y, 60, 60));
             
            x = 0; y = 0;
            foreach (var item in Equipped) {
                totalEquipped[y][x].setItem(item);
                x++;
                if(x == 3) { x = 0; y++; }
            }
            for(int ypp = 0; ypp < 4; ypp++)
                for(int xpp = 0; xpp < 3; xpp++)
                    if(totalEquipped[ypp][xpp].item != null)
                        gfx.DrawImage(totalEquipped[ypp][xpp].item.itemImageFile, new RectangleF(totalEquipped[ypp][xpp].X, totalEquipped[ypp][xpp].Y, 60, 60));


            if ((selectedX < 7 && totalCarried[selectedY][selectedX].item != null) || (selectedX >= 8 && totalEquipped[selectedY][selectedX - 8].item != null)) {
                Items item = selectedX < 7 ? totalCarried[selectedY][selectedX].item : totalEquipped[selectedY][selectedX - 8].item;
                int index = xdex - 1;
                float X = selectedX < 7 ? totalCarried[selectedY][selectedX].X : totalEquipped[selectedY][selectedX - 8].X,
                      Y = selectedX < 7 ? totalCarried[selectedY][selectedX].Y : totalEquipped[selectedY][selectedX - 8].Y;
                string flavortext = "";
                if (item.flavortext != null) {
                    flavortext = item.flavortext;
                }

                string name = item.itemName,
                    lvl = "Level: " + item.itemLVL.ToString(),
                    stats = "Health: " + item.itemHP.ToString() + Environment.NewLine +
                    "Damage: " + item.itemDMG.ToString() + Environment.NewLine +
                    "Defence: " + item.itemDEF.ToString() + Environment.NewLine + 
                    "Speed: " + item.itemSPEED.ToString() + Environment.NewLine + 
                    "Penetration: " + item.itemPENE.ToString() + Environment.NewLine;
                

                textPositionX = X + 65;
                textPositionY = Y + 65;
                SizeF sizeofFlavor = gfx.MeasureString(flavortext, flavortextFont, 190);

                int heightOfItAll = (int)(gfx.MeasureString(name, namefont,190).Height +
                                    gfx.MeasureString(lvl, lvlfont,190).Height +
                                    gfx.MeasureString(stats, statsfont,190).Height + 10);
                

                gfx.FillRectangle(new SolidBrush(Color.DarkSalmon), new RectangleF(textPositionX-5,textPositionY-5, 200, heightOfItAll + sizeofFlavor.Height));

                gfx.DrawString(name, namefont, Brushes.WhiteSmoke,
                    new RectangleF(new PointF(textPositionX, textPositionY),
                    new SizeF( gfx.MeasureString(name, namefont,190))));

                gfx.DrawString(lvl, lvlfont, Brushes.WhiteSmoke,
                    textPositionX + 5, textPositionY += (int)gfx.MeasureString(name, namefont, 190).Height);

                gfx.DrawString(stats, statsfont, Brushes.WhiteSmoke,
                    textPositionX + 5, textPositionY += (int)gfx.MeasureString(lvl, lvlfont, 190).Height);

                gfx.DrawString(flavortext, flavortextFont, Brushes.WhiteSmoke,
                    new RectangleF(new PointF(textPositionX + 5, textPositionY + gfx.MeasureString(stats, statsfont, 190).Height),
                    new SizeF(sizeofFlavor)));

            }
        }

        public void DrawCarried(Graphics gfx, Game game) {

            int width = (int)(game.Width / 1.2), height = (int)(game.Height / 1.2);
            int placex = game.Width / 2 - width / 2;
            int placey = game.Height / 2 - height / 2;

            Font font = new Font("Bradley Hand ITC", 40, FontStyle.Italic);
            gfx.FillRectangle(Background, new Rectangle(0,0,game.Width,game.Height));
            gfx.FillRectangle(new SolidBrush(Color.DarkGray), new Rectangle(placex, placey, width, height));
            
            float xdex = placex + 72, ydex = placey + 70, outterboxWid = 60, outterboxHei = 60;

            gfx.DrawImage(EqippedImage, new Rectangle(placex +width-850, placey-20, width-200, height), new Rectangle(0, 0, 64, 64), GraphicsUnit.Pixel);
            

            gfx.DrawString("Inventory", font, Brushes.Black,new Point(placex + 2,placey + 5));

            ydex = placey + height * 0.5f - 200;

            for (int indey = 0; indey < 4; indey++) {
                xdex = placex + width * 0.69f;
                

                for(int index = 0; index < 3; index++) {
                    totalEquipped[indey][index].X = xdex + 2;
                    totalEquipped[indey][index].Y = ydex + 2;

                    if(indey == selectedY && index == selectedX-8) {
                        drawSelected(gfx, xdex, ydex, outterboxWid, outterboxHei);
                    } else {
                        drawNotSelected(gfx, xdex, ydex, outterboxWid, outterboxHei);
                    }
                    xdex += 70;
                   
                }

                ydex += 70;
            }

            xdex = placex + 72; ydex = placey + 70; outterboxWid = 60; outterboxHei = 60;

            for (int indey = 0; indey < 8; indey++) {
                xdex = placex + 72;
                for (int index = 0; index < 8; index++) {

                    totalCarried[indey][index].X = xdex + 2;
                    totalCarried[indey][index].Y = ydex + 2;

                    if (indey == selectedY && index == selectedX) {
                        drawSelected(gfx, xdex, ydex, outterboxWid, outterboxHei);
                    } else {
                        drawNotSelected(gfx, xdex, ydex, outterboxWid, outterboxHei);
                    }
                    xdex += 70;
                    
                }
                ydex += 70;
                
            }
        }

        public void drawNotSelected(Graphics gfx, float xdex, float ydex, float outterboxWid, float outterboxHei) {
            gfx.FillRectangle(new SolidBrush(Color.Black), new RectangleF(xdex, ydex, outterboxWid, outterboxHei));
            gfx.FillRectangle(new SolidBrush(Color.GhostWhite), new RectangleF(xdex + 2, ydex + 2, outterboxWid - 4, outterboxWid - 4));
        }
        
        public void drawSelected(Graphics gfx, float xdex, float ydex, float outterboxWid, float outterboxHei) {
            gfx.FillRectangle(new SolidBrush(Color.Orange), new RectangleF(xdex-2, ydex-2, outterboxWid + 6, outterboxHei + 6));
            gfx.FillRectangle(new SolidBrush(Color.GhostWhite), new RectangleF(xdex + 2, ydex + 2, outterboxWid - 4, outterboxWid - 4));
        }
        
        public void calStats (out double[] array) {
            double attack = 0, defence = 0, speed = 0, penetration = 0, hp = 0;

            foreach (var item in Equipped) {
                if(item != null) {
                    attack += item.itemDMG;
                    defence += item.itemDEF;
                    speed += item.itemSPEED;
                    penetration += item.itemSPEED;
                    hp += item.itemHP;
                }
            }
            array = new double[5];
            array[0] = hp;
            array[1] = defence;
            array[2] = attack;
            array[3] = penetration;
            array[4] = speed;

        }
    }
}
