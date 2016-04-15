using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFormsTest {
    public class Menu {
        public static bool is_start_of_game = true;
        public List<Control> listWithMenu = new List<Control>();
        public Form form;
        public Menu(Form form) {
            this.form = form;
            Update_menu();
        }

        public void Update_menu() {
            listWithMenu.Add(new Button());
            listWithMenu.Add(new Button());
            listWithMenu.Add(new Label());
            listWithMenu.Add(new Panel());
            listWithMenu[3].SendToBack();
            listWithMenu[2].Text = "RPG - Game";
            listWithMenu[2].Font = new Font("Bradley Hand ITC", (float)175, FontStyle.Bold);
            listWithMenu[2].BackColor = Color.Transparent;
            listWithMenu[0].Font = listWithMenu[1].Font = new Font("Bradley Hand ITC", (float)20, FontStyle.Italic);
            listWithMenu[2].AutoSize = true;
            if (is_start_of_game) {
                listWithMenu[3].Width = form.Width;
                listWithMenu[3].Height = form.Height;
                form.Controls.Add(listWithMenu[3]);
                listWithMenu[0].Text = "Start";
                listWithMenu[2].Location = new Point(10, 20);
                listWithMenu[2].BringToFront();
                form.Controls.Add(listWithMenu[2]);
                is_start_of_game = false;

            } else
                listWithMenu[0].Text = "Continue";


            listWithMenu[0].Show();
            listWithMenu[1].Show();

       


            listWithMenu[1].Text = "Quit";
            listWithMenu[1].Height = listWithMenu[0].Height = 50;
            listWithMenu[1].Width = listWithMenu[0].Width = 200;


            listWithMenu[1].Location = new Point(form.Width / 2 - 100, 600);
            listWithMenu[0].Location = new Point(form.Width / 2 - 100, 500);
            listWithMenu[1].BringToFront();
            listWithMenu[0].BringToFront();

            listWithMenu[1].Click += new EventHandler(Quit_is_click);
            listWithMenu[0].Click += new EventHandler(Start_is_click);

            form.Controls.Add(listWithMenu[0]);
            form.Controls.Add(listWithMenu[1]);
            listWithMenu[3].SendToBack();
        }

        public void Quit_is_click(Object sender, EventArgs e) {
            
            DialogResult dialog = MessageBox.Show("R u sure?","Quit", MessageBoxButtons.YesNo,MessageBoxIcon.Warning);
            
            if(dialog == DialogResult.Yes) {
                Application.Exit();
            }
            return;
        }
        public void Start_is_click(Object sender, EventArgs e) {
            foreach(var item in listWithMenu) {
                item.Hide();
                form.Focus();
            }
           
            return;
        }

        //protected override CreateParams CreateParams {
        //    get {
        //        CreateParams cp = base.CreateParams;
        //        cp.ExStyle |= 0x00000020; // WS_EX_TRANSPARENT
        //        return cp;
        //    }
        //}

        //protected override void OnPaint(PaintEventArgs e) {
        //    e.Graphics.FillRectangle(new SolidBrush(this.BackColor), this.ClientRectangle);
        //}
    }
}
