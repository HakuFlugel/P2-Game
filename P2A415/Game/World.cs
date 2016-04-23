using System;
using System.Drawing;
using System.Collections.Generic;
using System.IO;

namespace WinFormsTest {
    public class World {
        public List<Character> characters = new List<Character>();

        public Region[,] regions = new Region[2,2];

        private Random rand;

        public World() {
            //CharacterType.loadCharacterTypes();
            for (int x = 0; x < regions.GetLength(0); x++) {
                for (int y = 0; y < regions.GetLength(1); y++) {
                    regions[x, y] = new Region(x, y);
                }
            }

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

        private void generateWorld() { // TODO: seed?

            rand = new Random();
            //regions;

            int[,] biomes = new int[regions.GetLength(0), regions.GetLength(1)];
            int[,][,] weights = new int[regions.GetLength(0), regions.GetLength(1)][,];


            for (int x = 0; x < regions.GetLength(0); x++) {
                for (int y = 0; y < regions.GetLength(1); y++) {
                    if (rand.Next()%10 < 9) {
                        makeMountains(x * 32 + rand.Next() % 32, y * 32 + rand.Next() % 32);
                    }
                }
            }


            // monsters

            // roads


        }

        private void makeMountains(int x, int y, int count = 1) {

            this[x,y] =

            // Spread
            for (int i = 0; i < 4; i++) {
                if (rand.Next(100)/count >= 10) {
                    
                    switch (i) {
                    case 1:
                        makeMountains(x, y + 1);
                        break;
                    case 2:
                        makeMountains(x + 1, y);
                        break;
                    case 3:
                        makeMountains(x, y - 1);
                        break;
                    case 4:
                        makeMountains(x - 1, y);
                        break;
                    default:
                        break;
                    }
                }

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


