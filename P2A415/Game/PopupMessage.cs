using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace RPGame {
    public class PopupMessage {

        public string text;

        public Bitmap image;
        private Brush background = new SolidBrush(Color.FromArgb(128, Color.Black));

        public PopupMessage next;


        public PopupMessage(string text, PopupMessage next = null) { 
            this.text = text;
            this.next = next;
        }

        public PopupMessage(Bitmap image, PopupMessage next = null) {
            this.image = image;
            this.next = next;
        }

        public void draw(Graphics gfx, Game game) {
            int padding = 32;

            float gameWidth = game.ClientSize.Width, gameHeight = game.ClientSize.Height;
            Font font = new Font("Arial", 32, FontStyle.Regular);

            if (text != null) {
                SizeF textSize = gfx.MeasureString(text, font, (int)(gameWidth * 0.4));

                SizeF displayBoxSize = new SizeF(Math.Max(textSize.Width + 2 * padding, 400), Math.Max(textSize.Height + 2 * padding, 2/*00*/));

                RectangleF displayBoxRect =
                    new RectangleF(
                        (gameWidth - displayBoxSize.Width) / 2,
                        (gameHeight - displayBoxSize.Height) / 2,
                        displayBoxSize.Width, displayBoxSize.Height
                        );

                gfx.FillRectangle(background, new RectangleF(0, 0, gameWidth, gameHeight));
                gfx.FillRectangle(background, displayBoxRect);
                gfx.DrawString(text, font, Brushes.Wheat, new RectangleF(displayBoxRect.X + padding, displayBoxRect.Y + padding, gameWidth * 0.4f, gameHeight * 0.6f));
            }

            if (image != null) {
                gfx.DrawImage(image, new RectangleF(0, 0, gameWidth, gameHeight));
            }
        }
    }
}
