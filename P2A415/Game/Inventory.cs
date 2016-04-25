using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFormsTest {
    public class ItemPictureBox : PictureBox {
        public int index;
    }

    public class Inventory {

        public Form form;
        List<ItemPictureBox> itemsInInventry;
        List<Items> Carried_sorted;

        Inventory(Form form,List<Items> Carried, List<Items> Equipped) {
                
            this.form = form;

            for(int index = 0; index < 32; index++) 
                itemsInInventry.Add(new ItemPictureBox());

            Carried_sorted = Carried.OrderBy(ch => ch.itemName).ToList();

            int PictureX = 50, PictureY = 200, Height = 20, Width = 20,xdex = 0;
            foreach(var item in itemsInInventry) {
                if(xdex % 5 == 0) {
                    PictureY += 25;
                    PictureX = 50;
                }   
                item.Width = Width;
                item.Height = Height;
                item.Location = new Point(PictureX, PictureY);
                PictureX += 25;
                xdex++;
            }

            int carriedCount = Carried.Count;
            for (int index = 0; index < carriedCount; index++ ) {
                itemsInInventry[index].BackgroundImage = ImageLoader.Load(Carried_sorted[index].itemImageFile);
                itemsInInventry[index].index = index;
                itemsInInventry[index].MouseHover += (sender, e) => { Inventory_MouseHover(sender, e, Carried_sorted[index],true); };
                itemsInInventry[index].MouseLeave += (sender, e) => { Inventory_MouseHover(sender, e, Carried_sorted[index], false); };
            }
        }
        ToolTip
        private void Inventory_MouseHover(object sender, EventArgs e, Items item,bool is_hover) {
            if(is_hover) {
                
            }
            else {

            }
            
        }
    }

}
