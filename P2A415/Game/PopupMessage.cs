﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace RPGame {
    public class PopupMessage {

        public string text;

        private Brush background = new SolidBrush(Color.FromArgb(128, Color.Black));


        public PopupMessage(string text) { this.text = text; }

        public void draw(Graphics gfx, Game game) {
            int padding = 12;


            float gameWidth = game.Width, gameHeight = game.Height;
            Font font = new Font("Arial", 32, FontStyle.Regular);


            SizeF textSize = gfx.MeasureString(text, font);

            SizeF displayBoxSize = new SizeF(Math.Max(textSize.Width + 2 * padding, 400), Math.Max(textSize.Height + 2 * padding, 200));

            RectangleF displayBoxRect =
                new RectangleF(
                    (gameWidth - displayBoxSize.Width) / 2,
                    (gameHeight - displayBoxSize.Height) / 2,
                    displayBoxSize.Width, displayBoxSize.Height
                    );

            gfx.FillRectangle(background, new RectangleF(0, 0, gameWidth, gameHeight));
            gfx.FillRectangle(background, displayBoxRect);
            gfx.DrawString(text, font, Brushes.Wheat, displayBoxRect.X + padding, displayBoxRect.Y + padding);




        }
    }
}
