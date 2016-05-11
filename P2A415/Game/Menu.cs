using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;


namespace RPGame {
    public class Menu {

        private class Button {
            public string text;
            public Action<Button> onPress;
            public Button(string text, Action<Button> onPress) {
                this.text = text;
                this.onPress = onPress;
            }
        }

        Game game;
        private List<Button> buttons = new List<Button>();
        public  int selected = 0;
        public bool isOpen { get; private set; } = false;
        public bool StatisticsIsOpen = false;

        private SolidBrush menuBackground;
        private Bitmap buttonImage;
        Font font;


        public Menu(Game game) {
            this.game = game;

            menuBackground = new SolidBrush(Color.FromArgb(128, Color.Black));
            buttonImage = ImageLoader.Load("Content/Buttons.png");

            font = new Font("Arial", 32
                , FontStyle.Bold);

            
            buttons.Add(new Button("Resume Game", (button) => {
                //button.text = "Resume Game";
                this.isOpen = false;
            }));
            buttons.Add(new Button("Statistiscs", (button) => {
                statisticsToggle();
            }));
            buttons.Add(new Button("New Game", (button) => {
                Application.Restart();
            }));

            buttons.Add(new Button("Quit", (button) => {
                game.shouldRun = false;
            }));
        }

        public void toggle() {
            isOpen = !isOpen;
            selected = 0;
            StatisticsIsOpen = false;
        }

        public void statisticsToggle() {
            
            StatisticsIsOpen = !StatisticsIsOpen;

        }


        public void keyInput(KeyEventArgs e) {
            switch (e.KeyCode) {

                case Keys.W:
                case Keys.Up:
                    if(--selected < 0) {
                        selected = 0;
                    }
                    break;

                case Keys.S:
                case Keys.Down:
                    if (++selected > buttons.Count-1) {
                        selected = buttons.Count-1;
                    }
                    break;

                case Keys.Enter:
                    selectItem();
                    break;

                default:
                    break;
            }
            Console.WriteLine("selected: " + selected);
        }

        public void selectItem() {
            Button selectedButton = buttons[selected];
            Console.WriteLine("selected" + selected + selectedButton);
            selectedButton.onPress(selectedButton);
        }


        public void draw(Graphics gfx) {
            if (StatisticsIsOpen) {
                drawStatistics(gfx);
            }

            const int padding = 16;
            const int buttonWidth = 384;
            const int buttonHeight = 128;


            Rectangle menuRect = new Rectangle(
                game.ClientSize.Width / 2 - buttonWidth / 2 - padding,
                game.ClientSize.Height / 2 - ((buttonHeight + padding) * buttons.Count + padding) / 2,
                buttonWidth + padding*2,
                ((buttonHeight + padding) * buttons.Count + padding)
                );


            gfx.FillRectangle(menuBackground, menuRect);

            for (int i = 0; i < buttons.Count; i++) {

                Rectangle buttonRect = new Rectangle(
                    menuRect.X + padding,
                    menuRect.Y + padding + i * (buttonHeight + padding),
                    buttonWidth,
                    buttonHeight
                    );

                gfx.DrawImage(buttonImage, buttonRect, new Rectangle(384*1/*TODO: is in game*/, 128 * (selected == i ? 1 : 0), 384, 128), GraphicsUnit.Pixel );

                StringFormat stringFormat = new StringFormat();
                stringFormat.Alignment = StringAlignment.Center;
                stringFormat.LineAlignment = StringAlignment.Center;
                gfx.DrawString(buttons[i].text, font, Brushes.DeepPink, buttonRect, stringFormat);
            }
        }

        public void drawStatistics(Graphics gfx) {
            String text = $@"Statistics 
Encounters: {Statistics.Encounters} 
Kills: {Statistics.Kills}
Deaths: {Statistics.Deaths}

Questions: {Statistics.Questions} 
Correct: {Statistics.Correct}
Wrong: {Statistics.Wrong}

Distance: {Statistics.Distance}
Highest Level: {Statistics.HighestLevel}
Town visits: {Statistics.TownVisit}";


            const int padding = 4;
            SizeF size = gfx.MeasureString(text, font);

            RectangleF uiRect = new RectangleF(game.ClientSize.Width - size.Width - 3 * padding, padding, size.Width + 2 * padding, size.Height + 2 * padding);
            RectangleF textRect = new RectangleF(uiRect.X + padding, uiRect.Y + padding, size.Width, size.Height);
            gfx.FillRectangle(menuBackground, uiRect);

            gfx.DrawString(text, font, Brushes.WhiteSmoke, textRect);
        }
    }
}
