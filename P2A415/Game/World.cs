using System;
using System.Drawing;
using System.Collections.Generic;
using System.IO;

namespace WinFormsTest {
    public class World {
        public List<Character> characters = new List<Character>();

        public Region[,] regions = new Region[16,16];

        private Random rand;

        public World() {

            generateWorld();

            characters.Add(new Character(2, 1, 3));
            characters.Add(new Character(2, 3, 3));

          for (int i = 0; i < 100; i++) {
                characters.Add(new Character(2, i*10%64, i*10/64));
            }
            Random rand = new Random(); // TODO: remove
            Game.instance.localPlayer = new Player(rand.Next(10)+10, rand.Next(10));// characters[rand.Next(characters.Count - 1)];
            characters.Add(Game.instance.localPlayer.character);
        }

        public int this[long x, long y] {
            get {
                return regions[x / 32, y / 32][x % 32, y % 32];
            }
            set {
                regions[x / 32, y / 32][x % 32, y % 32] = value;
            }
        }

        private enum GeneratedTile {
            Ground, // 3
            Trees, // 0,1,2
            Mountain, //???
            Path, // 3-18
            Town // ????
        }

        private void generateWorld() { // TODO: seed?

            // Allocate regions
            for (int x = 0; x < regions.GetLength(0); x++) {
                for (int y = 0; y < regions.GetLength(1); y++) {
                    regions[x, y] = new Region(x, y);
                }
            }

            rand = new Random();
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
            // monsters

            Dictionary<GeneratedTile,int> ttweight = new Dictionary<GeneratedTile, int>();
            ttweight.Add(GeneratedTile.Ground, 2);
            ttweight.Add(GeneratedTile.Mountain, 64);
            ttweight.Add(GeneratedTile.Path, 1);
            ttweight.Add(GeneratedTile.Trees, 16);
            ttweight.Add(GeneratedTile.Town, 1);


            // Weights
            for (int x = 0; x < regions.GetLength(0) * 32; x++) {
                for (int y = 0; y < regions.GetLength(1) * 32; y++) {
                    weights[x, y] = ttweight[(GeneratedTile)this[x, y]];
                }
            }

            // path

            //todo: choose which towns to link

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
                        this[x, y] = 3+rand.Next()%15;
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
            for (int x = 0; x < regions.GetLength(0); x++) {
                for (int y = 0; y < regions.GetLength(1); y++) {
                    regions[x, y].draw(gfx, cameraPosition);
                }
            }
            foreach (var character in characters) {
                character.draw(gfx, cameraPosition);
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


