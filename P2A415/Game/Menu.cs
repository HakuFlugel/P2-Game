using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFormsTest {
    public class Menu {
        public bool is_in_menu = true;
        public List<Control> listWithMenu = new List<Control>();
        public Form form;
        public Menu(Form form) {

            this.form = form;

            int where_button_start = 300;

            listWithMenu.Add(new Button());
            listWithMenu.Add(new Button());
            listWithMenu.Add(new Label());
            listWithMenu.Add(new Panel());

            foreach(var item in listWithMenu) {
                if(item.GetType() == typeof(Button)) {

                    item.Location = new Point(form.Width / 2 - 100, where_button_start += 100);
                    item.Width = 200; item.Height = 50;
                    item.Font = new Font("Bradley Hand ITC", (float)20, FontStyle.Italic);
                    form.Controls.Add(item);

                }else if(item.GetType() == typeof(Label)) {

                    item.Text = "RPG - Game";
                    item.Font = new Font("Bradley Hand ITC", (float)175, FontStyle.Bold);
                    item.Location = new Point(10, 20);
                    item.BackColor = Color.Transparent;
                    item.AutoSize = true;
                    form.Controls.Add(item);

                }else if(item.GetType() == typeof(Panel)) {

                    item.Width = form.Width;
                    item.Height = form.Height;
                    item.SendToBack();
                    form.Controls.Add(item);
                }
            }
            
            listWithMenu[0].Text = "Start";
            listWithMenu[1].Text = "Quit";

            listWithMenu[1].Click += new EventHandler(Quit_is_click);
            listWithMenu[0].Click += new EventHandler(Start_is_click);
        }

        public void Update_menu() {

            is_in_menu = true;

            listWithMenu[0].Text = "Continue";

            listWithMenu[0].Show();
            listWithMenu[1].Show();
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
                is_in_menu = false;
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
