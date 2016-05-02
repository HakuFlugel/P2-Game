﻿using System;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace RPGame {
    public class UserInterface {

        private Game game;

        private SolidBrush background;


        ///private int screenWidth, screenHeight;

        public UserInterface(Game game) {
            //screenWidth = 1920;//game.ClientSize.Width;
            //screenHeight = 1080;//game.ClientSize.Height;
            this.game = game;

            background = new SolidBrush(Color.FromArgb(128, Color.Black));
        }

        public void draw(Graphics gfx) {

            Character localCharacter = game.localPlayer.character;
            //Bitmap transparentbox = new Bitmap("Content/transparentbar.png");
            //gfx.DrawImage(transparentbox, new RectangleF(2, 2, 300, 100), new Rectangle(0, 0, 1, 1), GraphicsUnit.Pixel);
            gfx.FillRectangle(background, new Rectangle(2, 2, 300, 100));

            Font font = new Font("Arial", 16, FontStyle.Regular);
            gfx.DrawString($@"Unnamed Player | {(long)localCharacter.stats.curHP} HP
Level: {localCharacter.stats.level}
Experience: {localCharacter.stats.exp}/{localCharacter.expRequired()}" , font, Brushes.WhiteSmoke, 10, 10);

            Combat currentCombat = game.localPlayer.character.currentCombat;
//            if (currentCombat != null) {
//                Font bigfont = new Font("Arial", 32, FontStyle.Regular);
//
//                gfx.DrawString($@"{currentCombat.firstCharacter.stats.hp}
//{currentCombat.secondCharacter.stats.hp}
//{currentCombat.answerString}
//{currentCombat.enemyAttackTime}
//{currentCombat.currentQuestion.text}
//{currentCombat.currentQuestion.expression}", bigfont, brush, game.ClientSize.Width/2-50, 50);
//
//
//                drawQuestionMenu(gfx);
//            }

        }

        public void drawQuestionMenu(Graphics gfx) {

        }
    }
}