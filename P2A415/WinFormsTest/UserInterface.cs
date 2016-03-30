using System;
using System.Drawing;
using System.Windows.Forms;

namespace WinFormsTest {
    public class UserInterface {

        private Brush brush;

        private int screenWidth, screenHeight;



        public UserInterface() {
            screenWidth = 1920;//Game.instance.Width;
            screenHeight = 1080;//Game.instance.Height;

            brush = new SolidBrush(Color.WhiteSmoke);


        }

        public void draw(Graphics gfx) {
            Font font = new Font("Arial", 16, FontStyle.Regular);
            gfx.DrawString("Joakims mor", font, brush, screenWidth/2 -50, screenHeight/2-100);
        }
    }
}

