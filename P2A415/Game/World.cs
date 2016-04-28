using System;
using System.Drawing;
using System.Collections.Generic;
using System.IO;

namespace RPGame {
    public class World {
        public List<Character> characters = new List<Character>();

        public Region[,] regions = new Region[16,16];

        private Random rand;

        private Game game;

        public World(Game game) {
            this.game = game;

            generateWorld();

            //characters.Add(new Character(2, 1, 3));
            //characters.Add(new Character(2, 3, 3));

          //for (int i = 0; i < 1000; i++) {
          //      characters.Add(new Character(2, i*10%64, i*10/64));
          //  }
            Random rand = new Random(); // TODO: remove
            game.localPlayer = new Player(rand.Next(10)+10, rand.Next(10));// characters[rand.Next(characters.Count - 1)];
            characters.Add(game.localPlayer.character);
        }

        public int this[long x, long y] {
            get {
                return regions[x / 32, y / 32][x % 32, y % 32];
            }
            set {
                regions[x / 32, y / 32][x % 32, y % 32] = value;
            }
        }

        public enum GeneratedTile {
            Ground, // 3
            Trees, // 0,1,2
            Mountain, //???
            Path=18, // 3-18
            Town=20 // ????
        }

        private void generateWorld() { // TODO: seed?

            // Allocate regions
            for (int x = 0; x < regions.GetLength(0); x++) {
                for (int y = 0; y < regions.GetLength(1); y++) {
                    regions[x, y] = new Region(x, y);
                }
            }

            rand = new Random(2);
            //regions;

            int[,] biomes = new int[regions.GetLength(0), regions.GetLength(1)];
            int[,] weights = new int[regions.GetLength(0) * 32, regions.GetLength(1) * 32];

            // Biomes
            for (int x = 0; x < biomes.GetLength(0); x++) {
                for (int y = 0; y < biomes.GetLength(1); y++) {
                    biomes[x, y] = rand.Next() % 4;
                }
            }

            generateMountains();

            generateTrees();

            // towns
            

            Dictionary<GeneratedTile,int> ttweight = new Dictionary<GeneratedTile, int>();
            ttweight.Add(GeneratedTile.Ground, 2);
            ttweight.Add(GeneratedTile.Mountain, 64);
            //ttweight.Add(GeneratedTile.Path, 1);
            ttweight.Add(GeneratedTile.Trees, 16);
            //ttweight.Add(GeneratedTile.Town, 1);


            // Weights
            for (int x = 0; x < regions.GetLength(0) * 32; x++) {
                for (int y = 0; y < regions.GetLength(1) * 32; y++) {
                    weights[x, y] = ttweight[(GeneratedTile)this[x, y]];
                }
            }

            // monsters
            generateMonsters(weights);


            //// path
            //RoadMaker roadmaker = new RoadMaker(this, weights);
            //roadmaker.generatePath(new RoadMaker.coords(0,0), new RoadMaker.coords(511,511));
            //roadmaker.generatePath(new RoadMaker.coords(0,0), new RoadMaker.coords(128,64));
            //roadmaker.generatePath(new RoadMaker.coords(0,0), new RoadMaker.coords(64,128));

            ////TODO: hvorfor giver de 2 her exception???
            //roadmaker.generatePath(new RoadMaker.coords(0,0), new RoadMaker.coords(0,32));
            //roadmaker.generatePath(new RoadMaker.coords(0,0), new RoadMaker.coords(32,0));

            // Final tiles
            for (int x = 0; x < regions.GetLength(0)*32; x++) {
                for (int y = 0; y < regions.GetLength(1)*32; y++) {
                    switch (this[x,y]) {
                    case (int)GeneratedTile.Ground:
                        this[x, y] = 3;
                        break;
                    case (int)GeneratedTile.Trees:
                        this[x, y] = 0+rand.Next()%2;
                        break;
                    case (int)GeneratedTile.Mountain:
                        this[x, y] = 19;
                        break;
                    case (int)GeneratedTile.Path:
                        this[x, y] = 18;
                        // directions
                        break;
                    default:
                        break;
                    }

                    this[x, y] += 19 * biomes[x/32, y/32];
                    this[x, y] %= 19 * 4;

                }
            }

        }

        private void generateMonsters(int[,] weights) {
            for (int x = 0; x < regions.GetLength(0); x++) {
                for (int y = 0; y < regions.GetLength(1); y++) {

                    for (int i = 0; i < 64; i++) {
                        //if (rand.Next()%10 < 9) {
                        makeMonsters(x * 32 + rand.Next() % 32, y * 32 + rand.Next() % 32, weights);
                        //}
                    }

                }
            }
        }

        private void modifyWeight(int[,] weights, int x, int y, int add) {
            try {
                weights[x, y] += add;
            } catch (IndexOutOfRangeException) {
                // nothing
            }
        }

        private void makeMonsters(int x, int y, int[,] weights) {

            int lvl = (int)((x + y) / 32.0 + Math.Sqrt(x * y) / 4);

            for (int i = 0; i < rand.Next(1,8); i++) {
                if (x < 0 || y < 0 || x >= 32 * 16 || y >= 32 * 16) {
                    return;
                }
                if (this[x,y] != (int)GeneratedTile.Ground) {
                    return;
                }

                characters.Add(new Character(rand.Next(1, 2), x, y, lvl));

                modifyWeight(weights, x, y, 8);

                modifyWeight(weights, x+1, y, 2);
                modifyWeight(weights, x, y+1, 2);
                modifyWeight(weights, x-1, y, 2);
                modifyWeight(weights, x, y-1, 2);

                modifyWeight(weights, x+1, y+1, 1);
                modifyWeight(weights, x-1, y+1, 1);
                modifyWeight(weights, x+1, y-1, 1);
                modifyWeight(weights, x-1, y-1, 1);

                x += rand.Next(-3, 3);
                y += rand.Next(-3, 3);
            }

        }

        private void generateMountains() {
            for (int x = 0; x < regions.GetLength(0); x++) {
                for (int y = 0; y < regions.GetLength(1); y++) {

                    for (int i = 0; i < 4; i++) {
                        //if (rand.Next()%10 < 9) {
                        makeMountains(x * 32 + rand.Next() % 32, y * 32 + rand.Next() % 32);
                        //}
                    }

                }
            }
        }

        private void makeMountains(int x, int y, int count = 1) {

            try {
                if (this[x,y] != (int)GeneratedTile.Ground) {
                    return;
                }

                this[x, y] = (int)GeneratedTile.Mountain;

                // Spread
                for (int i = 0; i < 4; i++) {
                    if (rand.Next(100)/count >= 12) {
                        
                        switch (i) {
                        case 0:
                            makeMountains(x, y + 1, count + 1);
                            break;
                        case 1:
                            makeMountains(x + 1, y, count + 1);
                            break;
                        case 2:
                            makeMountains(x, y - 1, count + 1);
                            break;
                        case 3:
                            makeMountains(x - 1, y, count + 1);
                            break;
                        default:
                            break;
                        }
                    }

                }
            } catch (IndexOutOfRangeException) {
                //Console.WriteLine("Mountains hit bounds");
                // Do nothing
            }
        }

        private void generateTrees() {
            for (int x = 0; x < regions.GetLength(0); x++) {
                for (int y = 0; y < regions.GetLength(1); y++) {

                    for (int i = 0; i < 32; i++) {

                        //if (rand.Next() % 100 < 95) {
                        MakeTrees(x * 32 + rand.Next() % 32, y * 32 + rand.Next() % 32);
                        //}
                    }
                }
            }
        }

        private void MakeTrees(int x, int y, int count = 1) {

            try {

                if (this[x,y] != (int)GeneratedTile.Ground) {
                    return;
                }

                this[x, y] = (int)GeneratedTile.Trees;

                // Spread
                for (int i = 0; i < 4; i++) {
                    if (rand.Next(100)/count/count >= 8) {

                        switch (i) {
                        case 0:
                            MakeTrees(x, y + 1, count + 1);
                            break;
                        case 1:
                            MakeTrees(x + 1, y, count + 1);
                            break;
                        case 2:
                            MakeTrees(x, y - 1, count + 1);
                            break;
                        case 3:
                            MakeTrees(x - 1, y, count + 1);
                            break;
                        default:
                            break;
                        }
                    }

                }
            } catch (IndexOutOfRangeException) {
                //Console.WriteLine("Trees hit bounds");
                // Do nothing
            }
        }


        public void update(double deltaTime) {

            foreach (var character in characters) {
                character.update(deltaTime); // TODO: update each region instead, which should then update characters
            }
        }

        public void draw(Graphics gfx, Position cameraPosition) {
            int xlen = regions.GetLength(0), ylen = regions.GetLength(1);

            for (int x = 0; x < xlen; x++) { // TODO: udregn interval
                for (int y = 0; y < ylen; y++) {
                    regions[x, y].draw(game, gfx, cameraPosition);
                }
            }
            foreach (var character in characters) {
                character.draw(game, gfx, cameraPosition);
            }
        }

        /*public void WorldSave() { // saves world to file.
            using (BinaryWriter MyFile = new BinaryWriter(File.Open("Saves", FileMode.Create))) {
                foreach (var item in arr) {
                    MyFile.Write(item);
                }
                MyFile.Close();

            }
        }*/
    }
}


