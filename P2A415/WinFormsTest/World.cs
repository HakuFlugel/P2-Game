using System;
using System.Drawing;
using System.Collections.Generic;

namespace WinFormsTest {
    public class World {
        public List<Character> characters = new List<Character>();

        public Region[,] regions = new Region[2,2];

        public World() {
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
            foreach (var character in characters) {
                character.draw(gfx, cameraPosition);
            }
        }
    }
}

