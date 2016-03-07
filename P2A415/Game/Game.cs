using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace Game {
    public class Game : Microsoft.Xna.Framework.Game {

        public bool running = true;

        Random rand = new Random();

        List<Player> playerList = new List<Player>();
//        List<Enemy> enemyList = new List<Enemy>();
        List<Character> characterList = new List<Character>();

        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        public Game() {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize() {
            base.Initialize();
        }

        protected override void LoadContent() {
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

            using (var game = new Game()) {
                game.Run();
            }

        }
    }
}

