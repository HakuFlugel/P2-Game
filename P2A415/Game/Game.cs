﻿using System;
using System.Windows.Forms;
using System.Drawing;
using System.Diagnostics; // fps osv.
using System.Collections.Generic;
using System.Security.Cryptography;

namespace RPGame {
    public class Game : Form {
        private Menu menu;
        private Inventory invi;

        public World world;
        public Player localPlayer;

        public bool shouldRun = true;
        private Graphics graphics;


        private Stopwatch stopWatch = new Stopwatch();

        private long lastTime = 0;
        private long thisTime = 0;

        public UserInterface userInterface;

        public Game() {

            userInterface = new UserInterface(this);
            this.Text = "Titel";
            Bounds = Screen.PrimaryScreen.Bounds;
            this.WindowState = FormWindowState.Maximized;

            graphics = CreateGraphics();

            //CharacterType.loadCharacterTypes();

            menu = new Menu(this);                                                             //menu ting
            invi = new Inventory(this);                                                         //inventory ting
            Items item = new Items();
            


            invi.GetItem(item.MakeItem(new Items() {
                itemName = "Sword",
                itemHP = 1,
                itemLVL = 1,
                itemDMG = 1,
                itemDEF = 0,
                equipSlot = new Items.itemType {
                    Hands = 1
                }
            }, 2) );
          


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


            if (e.KeyChar == (char)Keys.Escape) {
                menu.toggle();                                                                 //menu ting

            } else if (e.KeyChar == 'E' || e.KeyChar == 'e') {
                invi.toggle();
                

            } else if (localPlayer.character.currentCombat != null) {
                localPlayer.character.currentCombat.keyPress(sender, e);
            }
        }

        private void keyInput (object sender, KeyEventArgs e, bool isDown) {
            
            //bool inCombat = localPlayer.character.currentCombat != null;

            if (menu.isOpen && isDown) {
                menu.keyInput(e);
            } else if(invi.isOpen && isDown) {
                invi.keyInput(e);
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
            //thisTime = Environment.TickCount;

            double deltaTime = (double)(thisTime - lastTime) / Stopwatch.Frequency;

            //Text = $"{((double)Stopwatch.Frequency / (thisTime - lastTime))} {Stopwatch.IsHighResolution} {Stopwatch.Frequency}";
            Text = $"{localPlayer.character.position.x}, {localPlayer.character.position.y}";
            if (menu.isOpen) {                                                                      //menu ting
                return;
            }
            localPlayer.update(this, deltaTime);

            if (this.localPlayer.character.currentCombat == null) {
                world.update(deltaTime);
            }
            // Clear input here?
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
                world.draw(gfx, localPlayer.character.position); // TODO: maybe move localplayer into world?
                }

                (localPlayer.character.currentCombat)?.draw(gfx);

                
                

                userInterface.draw(gfx);

                if (invi.isOpen) {
                    invi.draw(gfx);
                }
                
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

