using System;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace RPGame {
    public class UserInterface {

        private Game game;

        private SolidBrush background;


        ///private int screenWidth, screenHeight;

        public UserInterface(Game game) {
            this.game = game;

            background = new SolidBrush(Color.FromArgb(128, Color.Black));
        }

        public void draw(Graphics gfx) {

            Character localCharacter = game.localPlayer.character;

            Font font = new Font("Arial", 16, FontStyle.Regular);


            String text = $@"Unnamed Player | {(long)localCharacter.stats.curHP} HP
Level: {localCharacter.stats.level}
Experience: {localCharacter.stats.exp}/{localCharacter.expRequired()}
Zone level: {game.world.calculateLevel(localCharacter.position.x, localCharacter.position.y)}";


            const int padding = 4;
            SizeF size = gfx.MeasureString(text, font);

            RectangleF uiRect = new RectangleF(4, 4, size.Width+2*padding, size.Height+2*padding);
            RectangleF textRect = new RectangleF(uiRect.X+padding, uiRect.Y+padding, size.Width, size.Height);
            gfx.FillRectangle(background, uiRect);

            gfx.DrawString(text , font, Brushes.WhiteSmoke, textRect);
            
        }
    }
}