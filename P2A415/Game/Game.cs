using System;
using System.Windows.Forms;
using System.Drawing;
using System.Diagnostics; // fps osv.
using System.Collections.Generic;
using System.Security.Cryptography;

namespace RPGame {
    public class Game : Form {
        private Menu menu;

        public World world;
        public Player localPlayer;

        public bool shouldRun = true;
        private Graphics graphics;

        public Looting loot;

        public DrawGeneralMessage generalMessage;


        private Stopwatch stopWatch = new Stopwatch();

        private long lastTime = 0;
        private long thisTime = 0;

        public UserInterface userInterface;

        public static void Main() {
            using (Game game = new Game()) {
                game.run();
            }
        }

        public Game() {
            userInterface = new UserInterface(this);
            this.Text = "Titel";
            Bounds = Screen.PrimaryScreen.Bounds;
            this.WindowState = FormWindowState.Maximized;

            graphics = CreateGraphics();

            menu = new Menu(this);

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

            Resize += (sender, e) => {
                localPlayer.character.currentCombat?.resize();
            };
        }

        private void keyPress(object sender, KeyPressEventArgs e) { // TODO: flyt til keyinput
            if (e.KeyChar == (char)Keys.Escape) {
                menu.toggle();

            } else if (e.KeyChar == 'E' || e.KeyChar == 'e') {
                localPlayer.inventory.toggle(this);
            } else if (localPlayer.character.currentCombat != null) {
                localPlayer.character.currentCombat.keyPress(sender, e);
            }
        }

        private void keyInput (object sender, KeyEventArgs e, bool isDown) {


            if (generalMessage != null && isDown) {
                if (e.KeyCode == Keys.Space || e.KeyCode == Keys.Enter || e.KeyCode == Keys.E || e.KeyCode == Keys.Escape) {
                    generalMessage = null;
                }
            } else if (loot != null && isDown) {
                if (e.KeyCode == Keys.Space || e.KeyCode == Keys.Enter || e.KeyCode == Keys.E || e.KeyCode == Keys.Escape) {
                    loot = null;
                }
            } else if (menu.isOpen && isDown) {
                menu.keyInput(e);
            } else if (localPlayer.inventory.isOpen && isDown) {
                localPlayer.inventory.keyInput(e);
            } else

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

            // Draw titleimage
            Graphics gfx = CreateGraphics();
            RectangleF barForegroundRect = new RectangleF(
               0,0,ClientSize.Width, ClientSize.Height
            );

            Bitmap titleImage = ImageLoader.Load("Content/combatscreen.png");

            gfx.DrawImage(titleImage, new RectangleF(0, 0, ClientSize.Width, ClientSize.Height));

            world = new World(this);

            while(shouldRun) {
                update();
                render();

                Application.DoEvents();
            }
        }

        private void update() {
            lastTime = thisTime;
            thisTime = stopWatch.ElapsedTicks;

            double deltaTime = (double)(thisTime - lastTime) / Stopwatch.Frequency;

            Text = $"{localPlayer.character.position.x}, {localPlayer.character.position.y}";
            if ((menu.isOpen || localPlayer.inventory.isOpen) && localPlayer.character.currentCombat == null) {
                return;
            }
            localPlayer.update(this, deltaTime);

            if (this.localPlayer.character.currentCombat == null) {
                world.update(deltaTime);
            }
        }
        
        private void render() {
            int width = ClientSize.Width;
            int height = ClientSize.Height;

            if (width < 1 || height < 1) {
                return;
            }

            using (Bitmap bmp = new Bitmap(width, height))
            using (Graphics gfx = Graphics.FromImage(bmp)) {
  
                gfx.Clear(Color.Black);

                if (localPlayer.character.currentCombat == null) {
                    world.draw(gfx, localPlayer.character.position);
                    userInterface.draw(gfx);
                }

                localPlayer.character.currentCombat?.draw(gfx);

                localPlayer.inventory.draw(gfx,this);

                loot?.draw(gfx, this);

                menu.draw(gfx);

                generalMessage.draw(gfx,this);

                graphics.DrawImage(bmp, 0, 0);
            }
        }
    }
}

