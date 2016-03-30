using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using OpenTK.Platform.MacOS;


namespace Game {
    public class Game : Microsoft.Xna.Framework.Game {

        public bool running = true;

        //Random rand = new Random();

        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Texture2D image;

        public Game() : base() {
            graphics = new GraphicsDeviceManager(this);
            graphics.IsFullScreen = true;
            graphics.HardwareModeSwitch = false;

            graphics.SynchronizeWithVerticalRetrace = true; // vsync?

            Content.RootDirectory = "Content";
        }

        protected override void Initialize() {
            base.Initialize();
        }

        protected override void LoadContent() {
            spriteBatch = new SpriteBatch(GraphicsDevice); // TODO: move?

            image = Content.Load<Texture2D>("Frontscreen.png");

            base.LoadContent();
        }

        protected override void UnloadContent() {
            // TODO: Unload any non ContentManager content here
            base.UnloadContent();
        }

        protected override void Update(GameTime gameTime) {

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime) {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();
            spriteBatch.Draw(image, Vector2.Zero, Color.Wheat);
            spriteBatch.End();

            base.Draw(gameTime);
        }

/*        public void run() {


            Player player = new Player(0, 0);

            playerList.Add(player);
            characterList.Add(player.character);

            while(running) {

                update();
                render();

                // events              
            }

        }*/

        static void Main(string[] args) {
            //            foreach (var item in args) {
            //
            //            }

            var tt = new TileType(){walkable => {GetInt{}}}

            using (var game = new Game()) {
                game.Run();
            }

        }
    }
}

