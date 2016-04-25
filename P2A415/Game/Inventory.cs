using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFormsTest {
    public class Inventory {

        public Form form;
        List<PictureBox> itemsInInventry;
        List<Items> Carried_sorted;

        Inventory(Form form,List<Items> Carried, List<Items> Equipped) {
                
            this.form = form;

            for(int index = 0; index < 32; index++) 
                itemsInInventry.Add(new PictureBox());

            Carried_sorted = Carried.OrderBy(ch => ch.itemName).ToList();

            int carriedCount = Carried.Count;


            for(int index = 0; index < carriedCount; index++ ) {
                itemsInInventry[index] = Carried_sorted[index].Bitmap;
                itemsInInventry[index].MouseHover += Inventory_MouseHover;
            }
                
                
            

        }

        private void Inventory_MouseHover(object sender, EventArgs e) {
            
        }
    }
}
