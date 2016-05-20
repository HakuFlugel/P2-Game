﻿using System;
using System.Windows.Forms;
using System.Drawing;
using System.Diagnostics; // fps osv.
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Runtime.InteropServices;
using System.IO;
using System.Media;
using System.Threading;

namespace RPGame {
    public class Game : Form {
        private Menu menu;
        public UserInterface userInterface;
        public PopupMessage popupMessage;
        public Looting loot;

        public World world;
        public Player localPlayer;

        public MusicPlayer music;

        public bool shouldRun = true;
        private Graphics graphics;

        private Stopwatch stopWatch = new Stopwatch();

        private bool hasPressedM = false;
        
        private long lastTime = 0;
        private long thisTime = 0;

        public Game() {
           
            userInterface = new UserInterface(this);
            this.Text = "Titel";
            Bounds = Screen.PrimaryScreen.Bounds;
            this.WindowState = FormWindowState.Maximized;
            FormBorderStyle = FormBorderStyle.None;

            graphics = CreateGraphics();
            
            music = new MusicPlayer();
            menu = new Menu(this);
            // Events
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

        

        public static void Main() {
            

            using (Game game = new Game()) {
                game.run();
            }
        }

        private void keyPress(object sender, KeyPressEventArgs e) { // TODO: flyt til keyinput
            if (localPlayer.character.currentCombat != null) {
                localPlayer.character.currentCombat.keyPress(sender, e);
            }
        }

        public void keyInput (object sender, KeyEventArgs e, bool isDown) {
            if (e.KeyCode == Keys.M) {
                if (isDown && !hasPressedM) {

                    music.toggleMute();
                    hasPressedM = true;
                    Thread t = new Thread(() => {
                        
                        Thread.Sleep(1250);
                        hasPressedM = false;
                        
                    });
                    t.IsBackground = true;
                    t.Start();
                } 
            } else if (popupMessage != null) {
                if ((e.KeyCode == Keys.Space || e.KeyCode == Keys.Enter || e.KeyCode == Keys.E || e.KeyCode == Keys.Escape || e.KeyCode == Keys.T) && isDown) {
                    popupMessage = popupMessage.next;
                }
            } else if (loot != null) {
                if ((e.KeyCode == Keys.Space || e.KeyCode == Keys.Enter || e.KeyCode == Keys.E || e.KeyCode == Keys.Escape) && isDown) {
                    loot = null;
                }
            } else if (e.KeyCode == Keys.Escape) {
                if (isDown) {
                    menu?.toggle();
                }
            } else if (e.KeyCode == Keys.E) {
                if (isDown) {
                    localPlayer.inventory.toggle(this);
                }
            } else if (e.KeyCode == Keys.T && isDown) {
                if (localPlayer.inventory.isOpen == true) {
                    popupMessage = new PopupMessage("Inventory Tutorial",
                    new PopupMessage(ImageLoader.Load("Content/InvExplain1.png"),
                    new PopupMessage(ImageLoader.Load("Content/InvExplain2.png"),
                    new PopupMessage(ImageLoader.Load("Content/InvExplain3.png"),
                    new PopupMessage(ImageLoader.Load("Content/InvExplain4.png"),
                    new PopupMessage(ImageLoader.Load("Content/InvExplain5.png"),
                    new PopupMessage("You can press T to see this tutorial again")))))));
                }
                if (localPlayer.character.currentCombat == null && localPlayer.inventory.isOpen != true) {
                    popupMessage = new PopupMessage(ImageLoader.Load("Content/Information.png"));
                }
                
            } else if (menu.isOpen && isDown) {
                menu.keyInput(e);
            } else if (localPlayer.inventory.isOpen && isDown) {
                localPlayer.inventory.keyInput(this, e);
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
        }

        public void run() {
            Show();

            Activate();

            stopWatch.Start();

            // Draw titleimage
            Graphics gfx = CreateGraphics();
            RectangleF barForegroundRect = new RectangleF(0,0,ClientSize.Width, ClientSize.Height);
            Bitmap titleImage = ImageLoader.Load("Content/Titleimage.png");
            gfx.DrawImage(titleImage, new RectangleF(0, 0, ClientSize.Width, ClientSize.Height));

            // World
            world = new World(this);
            while(shouldRun) {

                update();
                render();
                
                Application.DoEvents();
            }
            
            if (!music.isMuted) { music.toggleMute(); }
            
            
            world.save();

        }

        private void update() {
            lastTime = thisTime;
            thisTime = stopWatch.ElapsedTicks;

            double deltaTime = (double)(thisTime - lastTime) / Stopwatch.Frequency;

            Text = $"{localPlayer.character.position.x}, {localPlayer.character.position.y}";

            music.update();

            if ((menu.isOpen || localPlayer.inventory.isOpen) && localPlayer.character.currentCombat == null || popupMessage != null) {
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

                music.draw(gfx,this);

                localPlayer.inventory.draw(gfx,this);

                loot?.draw(gfx, this);

                menu.draw(gfx);

                popupMessage?.draw(gfx,this);

                graphics.DrawImage(bmp, 0, 0);
            }
        }
    }
}

