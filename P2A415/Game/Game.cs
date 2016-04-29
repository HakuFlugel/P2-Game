using System;
using System.Windows.Forms;
using System.Drawing;
using System.Diagnostics; // fps osv.
using System.Collections.Generic;
using System.Security.Cryptography;

namespace RPGame {
    public class Game : Form {
        Menu menu;
        public static Game instance;

        public World world;
        public Player localPlayer;

        public bool shouldRun = true;
        private Graphics graphics;
        private Pen Pen;


        private Stopwatch stopWatch = new Stopwatch();

        private long lastTime = 0;
        private long thisTime = 0;

        public UserInterface userInterface = new UserInterface();

        public Game() {
            instance = this;

            this.Text = "Titel";
            Bounds = Screen.PrimaryScreen.Bounds;
            this.WindowState = FormWindowState.Maximized;

            graphics = CreateGraphics();

            //CharacterType.loadCharacterTypes();

            menu = new Menu(this);                                                             //menu ting
                  
            FormClosing += delegate {
                shouldRun = false;
            };

            KeyPress += keyPress;

            KeyDown += (sender, e) => {
                keyInput(sender, e, true);
            };
            KeyUp += (sender, e) => {
                keyInput(sender, e, false);
            };
        }

        private void keyPress(object sender, KeyPressEventArgs e) {
            if(e.KeyChar == (char)Keys.Escape) {
                menu.toggle();                                                                 //menu ting
            } else if (localPlayer.character.currentCombat != null) {
                localPlayer.character.currentCombat.keyPress(sender, e);
            }
        }

        private void keyInput (object sender, KeyEventArgs e, bool isDown) {

            //bool inCombat = localPlayer.character.currentCombat != null;

            if (menu.isOpen && isDown) {
                menu.keyInput(e);
            }

            switch (e.KeyCode) {
            case Keys.W:
            case Keys.Up:
                localPlayer.input.moveUp = isDown;
                break;
            case Keys.S:
            case Keys.Down:
                localPlayer.input.moveDown = isDown;
                break;
            case Keys.D:
            case Keys.Right:
                localPlayer.input.moveRight = isDown;
                break;
            case Keys.A:
            case Keys.Left:
                localPlayer.input.moveLeft = isDown;
                break;

            default:
                break;
            }

            Console.WriteLine(e.KeyCode +" "+ isDown);
        }

        public void run() {

            Show();
            Activate();

            stopWatch.Start();


            world = new World();

            while(shouldRun) {
                update();
                render();

                Application.DoEvents();

            }
        }

        private void update() {
            
            lastTime = thisTime;
            thisTime = stopWatch.ElapsedTicks;
            //thisTime = Environment.TickCount;

            double deltaTime = (double)(thisTime - lastTime) / Stopwatch.Frequency;

            //Text = $"{((double)Stopwatch.Frequency / (thisTime - lastTime))} {Stopwatch.IsHighResolution} {Stopwatch.Frequency}";
            Text = $"{localPlayer.character.position.x}, {localPlayer.character.position.y}";
            if (menu.isOpen) {                                                                      //menu ting
                return;
            }
            localPlayer.update(deltaTime);

            if (Game.instance.localPlayer.character.currentCombat == null) {
                world.update(deltaTime);
            }
            // Clear input here?
        }
        
        private void render() {
            using (Bitmap bmp = new Bitmap(ClientSize.Width, ClientSize.Height))
            using (Graphics gfx = Graphics.FromImage(bmp)) {
  
                gfx.Clear(Color.Black);

                world.draw(gfx, localPlayer.character.position); // TODO: maybe move localplayer into world?

                (localPlayer.character.currentCombat)?.draw(gfx);

                userInterface.draw(gfx);
                if (menu.isOpen) {
                    menu.draw(gfx);
                }

                graphics.DrawImage(bmp, 0, 0);
            }

        }

        public static void Main() {
            using (Game game = new Game()) {
                game.run();
            }
        }
    }
}

