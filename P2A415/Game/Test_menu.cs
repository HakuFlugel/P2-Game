using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFormsTest {
    public partial class Test_menu : Form {
        public Test_menu(int height, int width, bool is_in_game) {
            InitializeComponent();
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

            this.BackColor = Color.Pink;
            this.TransparencyKey = Color.Pink;


            //BackgroundImage = Image.FromFile(@"..\..\Resources\Transp.png");     //FromArgb(50, 88, 44, 55);
            Location = new Point(0, 0);
            Width = width;
            Height = height;


            Quit.Click += new EventHandler(Quit_is_click);
            //if ( == ConsoleKey.Escape && is_in_game && this.Visible) {
            //    this.Visible = false;
            //}
            Controls.Add(Start);
            Controls.Add(Quit);

        }

        public void Quit_is_click(Object sender, EventArgs e) {

            DialogResult dialog = MessageBox.Show("R u sure?", "Quit", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (dialog == DialogResult.Yes) {
                Application.Exit();
            }
            return;
        }
    }
}
