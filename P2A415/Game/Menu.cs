using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFormsTest {
    class Menu : Panel {

        public Menu(int height, int width,bool is_in_game) {

            Button Start = new Button();

            Button Quit = new Button();
            if (!is_in_game)
                Start.Text = "Start";
            else
                Start.Text = "Continue";

            Quit.Text = "Quit";
            Quit.Height = Start.Height = 50;
            Quit.Width = Start.Width = 200;


            Quit.Location = new Point(width / 2 - 100, 150);
            Start.Location = new Point(width / 2 - 100, 50);

            SetStyle(ControlStyles.SupportsTransparentBackColor, true);





            BackgroundImage = Image.FromFile(@"..\..\Resources\Transp.png") ;     //FromArgb(50, 88, 44, 55);
            Location = new Point(0, 0);
            Width = width;
            Height = height;
            

            Quit.Click += new EventHandler(Quit_is_click);
            //if(Console.ReadKey().Key == ConsoleKey.Escape && is_in_game && this.Visible) {
            //    this.Visible = false;
            //}
            Controls.Add(Start);
            Controls.Add(Quit);
            
        }

        public void Quit_is_click(Object sender, EventArgs e) {
            
            DialogResult dialog = MessageBox.Show("R u sure?","Quit", MessageBoxButtons.YesNo,MessageBoxIcon.Warning);
            
            if(dialog == DialogResult.Yes) {
                Application.Exit();
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
