using System;
using System.Drawing;
using System.Windows.Forms;

namespace RPGame {
    public class UserInterface {

        private Brush brush;

        ///private int screenWidth, screenHeight;



        public UserInterface() {
            //screenWidth = 1920;//Game.instance.Width;
            //screenHeight = 1080;//Game.instance.Height;

            brush = new SolidBrush(Color.WhiteSmoke);


        }

        public void draw(Graphics gfx) {

            Character localCharacter = Game.instance.localPlayer.character;


            Font font = new Font("Arial", 16, FontStyle.Regular);
            gfx.DrawString($@"Unnamed Player | {(long)localCharacter.stats.hp} HP
Level: {localCharacter.stats.level}
Experience: {localCharacter.stats.exp}/{localCharacter.expRequired()}" , font, brush, 10, 10);

            Combat currentCombat = Game.instance.localPlayer.character.currentCombat;
//            if (currentCombat != null) {
//                Font bigfont = new Font("Arial", 32, FontStyle.Regular);
//
//                gfx.DrawString($@"{currentCombat.firstCharacter.stats.hp}
//{currentCombat.secondCharacter.stats.hp}
//{currentCombat.answerString}
//{currentCombat.enemyAttackTime}
//{currentCombat.currentQuestion.text}
//{currentCombat.currentQuestion.expression}", bigfont, brush, Game.instance.Width/2-50, 50);
//
//
//                drawQuestionMenu(gfx);
//            }

        }

        public void drawQuestionMenu(Graphics gfx) {

        }
    }
}