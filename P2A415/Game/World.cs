using System;
using System.Drawing;
using System.Collections.Generic;
using System.IO;

namespace RPGame {
    public class World {

        public Region[,] regions = new Region[16,16];
        private Random rand;
        private Game game;

        public World(Game game) {
            this.game = game;

            rand = new Random(42); //42 is a seed.

            generateWorld();

            game.localPlayer = new Player(regions[0,0]);
            regions[game.localPlayer.character.position.x/32, game.localPlayer.character.position.y/32].characters.Add(game.localPlayer.character);

            regions[1,1].characters.Add(new Character(regions[1,1], 4, regions[1,1].townx+32, regions[1,1].towny+32, calculateLevel(64, 64)));
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
            Ground=0, // 3
            Trees=1, // 0,1,2
            Mountain=19,
            Path=18, // 3-18
            Town=20
        }

        private void generateWorld() {

            // Allocate regions
            for (int x = 0; x < regions.GetLength(0); x++) {
                for (int y = 0; y < regions.GetLength(1); y++) {
                    regions[x, y] = new Region(x, y);
                }
            }

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

            generateTowns();
            
            //Number in ttweight.Add() is the weight for when generating roads.
            Dictionary<GeneratedTile,int> ttweight = new Dictionary<GeneratedTile, int>();
            ttweight.Add(GeneratedTile.Ground, 2);
            ttweight.Add(GeneratedTile.Mountain, 32);
            ttweight.Add(GeneratedTile.Trees, 16);
            ttweight.Add(GeneratedTile.Town, 1);
            //ttweight.Add(GeneratedTile.Path, 1);

            // Weights
            for (int x = 0; x < regions.GetLength(0) * 32; x++) {
                for (int y = 0; y < regions.GetLength(1) * 32; y++) {
                    weights[x, y] = ttweight[(GeneratedTile)this[x, y]];
                }
            }

            // monsters
            generateMonsters(weights);

            //// path
            RoadMaker roadmaker = new RoadMaker(this, weights);
            roadmaker.generatePath(new RoadMaker.coords(0, 0), new RoadMaker.coords(regions[0, 0].townx, regions[0, 0].towny));

            for (int x = 0; x < regions.GetLength(0); x++) {
                for (int y = 0; y < regions.GetLength(1); y++) {

                    int graph = (Math.Abs(x - 8) * Math.Abs(y - 8));
                    if (x < regions.GetLength(0) - 1 && graph >= rand.Next(-15, 63)) {
                        roadmaker.generatePath(new RoadMaker.coords(32 * x + regions[x, y].townx, 32 * y + regions[x, y].towny), new RoadMaker.coords((x + 1) * 32 + regions[x + 1, y].townx, (y) * 32 + regions[x + 1, y].towny));
                    }
                    if (y < regions.GetLength(1) - 1 && graph >= rand.Next(-15, 63)) {
                        roadmaker.generatePath(new RoadMaker.coords(32 * x + regions[x, y].townx, 32 * y + regions[x, y].towny), new RoadMaker.coords((x) * 32 + regions[x, y + 1].townx, (y + 1) * 32 + regions[x, y + 1].towny));
                    }
                }
            }

            // Final tiles
            int regionsx = regions.GetLength(0) * 32;
            int regionsy = regions.GetLength(1) * 32;

            for (int x = 0; x < regionsx; x++) {
                for (int y = 0; y < regionsy; y++) {

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
                        int pathOffset = 0;

                        pathOffset += shouldPathConnect(x, y + 1) ? 1 : 0;
                        pathOffset += shouldPathConnect(x + 1, y) ? 2 : 0;
                        pathOffset += shouldPathConnect(x, y - 1) ? 4 : 0;
                        pathOffset += shouldPathConnect(x - 1, y) ? 8 : 0;

                        this[x, y] = 3 + pathOffset;

                        // directions
                        break;
                    default:
                        break;
                    }
                    this[x, y] += 21 * biomes[x/32, y/32];
                    this[x, y] %= 21 * 4;
                }
            }
                }

        private bool shouldPathConnect(int x, int y) {
            try {
                return this[x, y]%21 > 3 && this[x, y]%21 <= 18 || this[x, y]%21 == 20;
            } catch (IndexOutOfRangeException) {
                return false;
            }
        }

        private void generateMonsters(int[,] weights) {
            for (int x = 0; x < regions.GetLength(0); x++) {
                for (int y = 0; y < regions.GetLength(1); y++) {

                    for (int i = 0; i < 64; i++) {
                        makeMonsters(x * 32 + rand.Next() % 32, y * 32 + rand.Next() % 32, weights);
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

        public int calculateLevel(int x, int y) {
            return (int)((x + y) / 32.0 + Math.Sqrt(x * y) / 4);
        }

        private void makeMonsters(int x, int y, int[,] weights) {

            int lvl = calculateLevel(x, y);
            int count = rand.Next(1, 8);

            for (int i = 0; i < count; i++) {
                if (x < 0 || y < 0 || x >= 32 * 16 || y >= 32 * 16) {
                    return;
                }
                if (this[x,y] != (int)GeneratedTile.Ground) {
                    return;
                }

                regions[x/32,y/32].characters.Add(new Character(regions[x / 32, y / 32], rand.Next()%3+1, x, y, lvl));

                modifyWeight(weights, x, y, 8);

                modifyWeight(weights, x+1, y, 2);
                modifyWeight(weights, x, y+1, 2);
                modifyWeight(weights, x-1, y, 2);
                modifyWeight(weights, x, y-1, 2);

                modifyWeight(weights, x+1, y+1, 1);
                modifyWeight(weights, x-1, y+1, 1);
                modifyWeight(weights, x+1, y-1, 1);
                modifyWeight(weights, x-1, y-1, 1);

                x += rand.Next(-2, 2);
                y += rand.Next(-2, 2);
            }
        }

        private void generateMountains() {
            for (int x = 0; x < regions.GetLength(0); x++) {
                for (int y = 0; y < regions.GetLength(1); y++) {

                    for (int i = 0; i < 4; i++) {
                        makeMountains(x * 32 + rand.Next() % 32, y * 32 + rand.Next() % 32);
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
                // Do nothing
            }
        }

        private void generateTrees() {
            for (int x = 0; x < regions.GetLength(0); x++) {
                for (int y = 0; y < regions.GetLength(1); y++) {

                    for (int i = 0; i < 32; i++) {
                        MakeTrees(x * 32 + rand.Next() % 32, y * 32 + rand.Next() % 32);
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
                // Do nothing
            }
        }
        private void generateTowns() {
            for (int x = 0; x < regions.GetLength(0); x++) {
                for (int y = 0; y < regions.GetLength(1); y++) {
                    regions[x, y].makeTown(rand);
                }
            }
        }

        public void update(double deltaTime) {
            regions[game.localPlayer.character.position.x / 32, game.localPlayer.character.position.y / 32].update(game, deltaTime);
        }

        public void draw(Graphics gfx, Position cameraPosition) {
            int xlen = regions.GetLength(0), ylen = regions.GetLength(1);

            // X range
            int cameraStartX = (cameraPosition.x - game.ClientSize.Width / 2 / (64 * 2) - 2)/32;//- 2;
            cameraStartX = Math.Max(cameraStartX, 0);

            int cameraEndX = (cameraPosition.x + game.ClientSize.Width / 2 / (64 * 2) + 2) / 32;//+ 2;
            cameraEndX = Math.Min(cameraEndX, xlen-1);

            // Y range
            int cameraStartY = (cameraPosition.y - game.ClientSize.Height / 2 / (64 * 2) - 2)/32;//- 2;
            cameraStartY = Math.Max(cameraStartY, 0);

            int cameraEndY = (cameraPosition.y + game.ClientSize.Height / 2 / (64 * 2) + 2) / 32;//+ 2;
            cameraEndY = Math.Min(cameraEndY, ylen-1);


            for (int x = cameraStartX; x <= cameraEndX; x++) {
                for (int y = cameraStartY; y <= cameraEndY; y++) {
                    regions[x, y].draw(game, gfx, cameraPosition);
                }
            }

            for (int x = cameraStartX; x <= cameraEndX; x++) { 
                for (int y = cameraStartY; y <= cameraEndY; y++) {
                    regions[x, y].drawCharacters(game, gfx, cameraPosition);
                }
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
