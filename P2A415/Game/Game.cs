﻿using System;
using System.Windows.Forms;
using System.Drawing;
using System.Diagnostics; // fps osv.
using System.Collections.Generic;
using System.Security.Cryptography;

namespace WinFormsTest {
    public class Game : Form {
        public Menu menu { get; set; }
        public Inventory inve { get; set; }
        public static Game instance;

        //public World world;
        public Player localPlayer;

        public bool shouldRun = true;
        private Graphics graphics;
        private Pen Pen;


        private Stopwatch stopWatch = new Stopwatch();

        private long lastTime = 0;
        private long thisTime = 0;

        public World world;


        public UserInterface userInterface = new UserInterface();

        public Game() {
            instance = this;

            this.Text = "Titel";
            Bounds = Screen.PrimaryScreen.Bounds;
            this.WindowState = FormWindowState.Maximized;

            graphics = CreateGraphics();
            Pen = new Pen(Color.DarkRed,1);

            //CharacterType.loadCharacterTypes();

            //this.menu = new Menu(this);
           


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
            if(e.KeyChar == (char) Keys.Escape) {
                //menu.Update_menu();
            }
            if (localPlayer.character.currentCombat != null) {
                localPlayer.character.currentCombat.keyPress(sender, e);
            }
        }

        private void keyInput (object sender, KeyEventArgs e, bool isDown) {
            
            //bool inCombat = localPlayer.character.currentCombat != null;

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
//            if (menu.is_in_menu) {
//                return;
//            }
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

                for (int i = 0; i < 500; i++) {
                    Position position = localPlayer.character.position;
                    float x,y;
                    x = (float)(i % 50 - position.x - position.xoffset * position.offsetScale) * 64*2 + Game.instance.Width / 2 - 64;
                    y = (float)(i / 50 - position.y - position.yoffset * position.offsetScale) * 64*2 - Game.instance.Height/ 2 + 64;

                    gfx.DrawRectangle(Pen, x, -y, 128, 128);
                    gfx.DrawRectangle(Pen, x+32, -y+32, 64, 64);

                }

                world.draw(gfx, localPlayer.character.position); // TODO: maybe move localplayer into world?

                (localPlayer.character.currentCombat)?.draw(gfx);

                userInterface.draw(gfx);
                
                    Inventory invi = new Inventory();

                List<Items> item = new List<Items>();
                item.Add(new Items() {
                    itemImageFile = "",
                    itemName = "Sword of Slays",
                    itemHP = 1,
                    itemLVL = 1,
                    itemDMG = 1,
                    itemDEF = 0,
                    equipSlot = new Items.itemType() {
                        Hands = 1
                    }
                });
                item.Add(new Items() {
                    itemImageFile = "",
                    itemName = "Sword of bobb",
                    itemHP = 22,
                    itemLVL = 100,
                    itemDMG = 654,
                    itemDEF = 2,
                    equipSlot = new Items.itemType() {
                        Hands = 1
                    }
                });

                invi.DrawInvi(gfx);
                invi.draw(gfx, item, new List<Items> {( new Items() {
                itemImageFile = "",
                itemName = "God's Gloves",
                itemHP = 1,
                itemLVL = 2,
                itemDMG = 1,
                itemDEF = 10,
                equipSlot = new Items.itemType() {
                    Gloves = 1,
                    Hands = -1
                } }) }, false);

                
                
                
                
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

