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
            gfx.DrawString(@"Unnamed Player
Level: 1337
Gold: 42" , font, brush, 10, 10);


            if (Game.instance.localPlayer.character.currentCombat != null) {
                Font bigfont = new Font("Arial", 32, FontStyle.Regular);
                gfx.DrawString(@"I am an enemuh
Boooring! Boooring!
Rank Up!
Master Sergeant Shooter Person!" , bigfont, brush, Game.instance.Width/2-50, Game.instance.Height/2 -150);

                drawQuestionMenu(gfx);
            }

        }

        public void drawQuestionMenu(Graphics gfx) {
            
        }
    }
}

