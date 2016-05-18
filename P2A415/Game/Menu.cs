using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.IO;
using System.Threading;


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

            // Menu buttons
            buttons.Add(new Button("Resume Game", (button) => {
                this.isOpen = false;
            }));
            buttons.Add(new Button("Statistiscs", (button) => {
                statisticsToggle();
            }));
            buttons.Add(new Button("New Game", (button) => {
                File.Delete("save.dat");
                this.isOpen = false;
                int highestLevel = game.localPlayer.statistics.highestLevel;
                game.world = new World(game);
                game.localPlayer.statistics.highestLevel = highestLevel;
                
            }));

            buttons.Add(new Button((game.music.isMute) ? "Unmute" : "Mute", (button) => {
                game.music.toggleMute();
                if (game.music.isMute)
                    button.text = "Unmute";
                else
                    button.text = "Mute";
            }));

            buttons.Add(new Button("Quit", (button) => {
                game.shouldRun = false;
            }));
        }

        public void toggle() {
            isOpen = !isOpen;
            selected = 0;
            StatisticsIsOpen = false;
            if (game.localPlayer.tutorial.firstMenu) {
                game.popupMessage = new PopupMessage("First menu yeah?");
                game.localPlayer.tutorial.firstMenu = false;
            }
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
            if (!isOpen) {
                return;
            }

            if (StatisticsIsOpen) {
                drawStatistics(gfx);
            }

            const int padding = 16;
            const int buttonWidth = 384;
            const int buttonHeight = 128;
            
            //Draw menu background
            Rectangle menuRect = new Rectangle(
                game.ClientSize.Width / 2 - buttonWidth / 2 - padding,
                game.ClientSize.Height / 2 - ((buttonHeight + padding) * buttons.Count + padding) / 2,
                buttonWidth + padding*2,
                ((buttonHeight + padding) * buttons.Count + padding)
                );

            gfx.FillRectangle(menuBackground, menuRect);

            // Draws buttons
            for (int i = 0; i < buttons.Count; i++) {

                Rectangle buttonRect = new Rectangle(
                    menuRect.X + padding,
                    menuRect.Y + padding + i * (buttonHeight + padding),
                    buttonWidth,
                    buttonHeight
                    );

                gfx.DrawImage(buttonImage, buttonRect, new Rectangle(buttonWidth*1, buttonHeight * (selected == i ? 1 : 0), buttonWidth, buttonHeight), GraphicsUnit.Pixel );

                StringFormat stringFormat = new StringFormat();
                stringFormat.Alignment = StringAlignment.Center;
                stringFormat.LineAlignment = StringAlignment.Center;
                gfx.DrawString(buttons[i].text, font, Brushes.DeepPink, buttonRect, stringFormat);
            }
        }

        public void drawStatistics(Graphics gfx) {
            Font statFont = new Font("Arial", 26);

            Statistics statistics = game.localPlayer.statistics;
            String text = $@"Statistics
Seed: {game.world.seed}
Time played: {statistics.timePlayed.ToString("0.00"), -8}

Encounters: {statistics.encounters} 
Kills: {statistics.kills}
Deaths: {statistics.deaths}

Questions: {statistics.questions} 
Correct: {statistics.correct}
Wrong: {statistics.wrong}

Distance: {statistics.distance}
Highest Level: {statistics.highestLevel}
Hardest Monster: {statistics.monsterHighestLevel}
Town visits: {statistics.townVisits}";

            const int padding = 4;
            SizeF size = gfx.MeasureString(text, statFont);
            
            RectangleF uiRect = new RectangleF(game.ClientSize.Width - size.Width - padding, padding, size.Width, size.Height);
            RectangleF textRect = new RectangleF(uiRect.X + padding, uiRect.Y + padding, size.Width, size.Height);
            gfx.FillRectangle(menuBackground, uiRect);

            gfx.DrawString(text, statFont, Brushes.WhiteSmoke, textRect);
        }
    }
}
