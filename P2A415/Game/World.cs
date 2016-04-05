using System;
using System.Drawing;
using System.Collections.Generic;
using System.IO;

namespace WinFormsTest {
    public class World {
        public List<Character> characters = new List<Character>();

        public Region[,] regions = new Region[2,2];

        public World() {
            CharacterType.loadCharacterTypes();
            for (int x = 0; x < regions.GetLength(0); x++) {
                for (int y = 0; y < regions.GetLength(1); y++) {
                    Console.WriteLine($"{x}{y}");
                    regions[x, y] = new Region(x, y);
                }
            }

            for (int i = 0; i < 100; i++) {
                characters.Add(new Character(i%10, i/10));
            }
            Random rand = new Random();
            Game.instance.localPlayer = new Player(rand.Next(10), rand.Next(10));// characters[rand.Next(characters.Count - 1)];
            characters.Add(Game.instance.localPlayer.character);
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


