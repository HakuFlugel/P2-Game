using System;
using System.Drawing;

namespace RPGame {

    public struct Stats {
        public double curHP;
        public double maxHP;
        public double attack;
        public double defence;

        public int level;
        public ulong exp;
    }

    public struct Position {
        public int x;
        public int y;

        public double xoffset;
        public double yoffset;
        public double offsetScale;

        public Position (int x, int y) {
            this.x = x;
            this.y = y;

            xoffset = 0;
            yoffset = 0;

            offsetScale = 0;
        }
    }

    public class Character {
        public static double moveDelay = 0.25;

        public Region region;
        public Bitmap texture;
        public Position position;
        public Stats stats = new Stats();

        public int characterType;

        public Combat currentCombat;

        public Character(Region region, int characterType, int x, int y, int level)
        {
            position.x = x;
            position.y = y;

            this.region = region;

            this.stats.level = level;

            this.characterType = characterType;

            CharacterType charType = CharacterType.characterTypes[characterType];

            texture = ImageLoader.Load(charType.imageFile);
            calculateStats();
            
        }

        public void update(Game game, double deltaTime) {
            if (position.offsetScale > -Character.moveDelay) { // Slight delay before being able to move again
                // Animate movement
                position.offsetScale -= 4.0f * (deltaTime);

            }

            // If the character has reached the tile, then set the offset to 0,0
            if (position.offsetScale <= 0) {
                position.xoffset = 0.0f;
                position.yoffset = 0.0f;


                Region region = game.world.regions[position.x / 32, position.y / 32];
                if (region.townx == position.x % 32 && region.towny == position.y % 32) {
                    stats.curHP = stats.maxHP;
                }
                
            }
        }

        public void draw(Game game, Graphics gfx, Position cameraPosition)
        {
            double x, y;
            x = ((position.x - cameraPosition.x) + (position.xoffset * position.offsetScale - cameraPosition.xoffset * cameraPosition.offsetScale)) * 64*2 + game.ClientSize.Width  / 2  - 64;
            y = ((position.y - cameraPosition.y) + (position.yoffset * position.offsetScale - cameraPosition.yoffset * cameraPosition.offsetScale)) * 64*2 - game.ClientSize.Height / 2  + 64;

            gfx.DrawImage(texture, new RectangleF((float)x, -(float)y, 64.0f*2, 64.0f*2), new Rectangle(0,0,63,63), GraphicsUnit.Pixel);

        }

        public void move(Game game,int x, int y) {
            if (canMove(game, position.x + x, position.y + y)) {
                int length = game.world.regions[(position.x + x) / 32, (position.y + y) / 32].characters.Count;
                for (int i = 0; i < length; i++) {

                    Character character = game.world.regions[(position.x + x) / 32, (position.y + y) / 32].characters[i];

                    if (character != this
                        && character.position.x == this.position.x + x
                        && character.position.y == this.position.y + y)
                    {
                        currentCombat = new Combat(game, this, character);
                        break;
                    }
                }

                position.x += x;
                position.y += y;

                Region newRegion = game.world.regions[position.x / 32, position.y / 32];
                if (newRegion != region) {
                    region.characters.Remove(this);
                    newRegion.characters.Add(this);
                    region = newRegion;
                }

                position.xoffset -= x;
                position.yoffset -= y;

                position.offsetScale = 1.0f;
            }

        }

        public bool canMove(Game game, long x, long y) {
            try {
                return TileType.tileTypes[game.world[x,y]].Moveable;
            } catch (Exception e) {
                Console.WriteLine("Can't move: " + e);
                return false;
            }

        }

        public void calculateStats() {

            CharacterType charType = CharacterType.characterTypes[characterType];

            stats.maxHP = charType.maxHP * Math.Pow(1.05, stats.level);
            stats.curHP = stats.maxHP;
            stats.attack = charType.attack * Math.Pow(1.10, stats.level); ;
            stats.defence = charType.defence * Math.Pow(1.05, stats.level); ;
        }

        public ulong expRequired() {
            return expRequired(stats.level);
        }

        public static ulong expRequired(int level) {
            return (ulong)(Math.Pow(level, 1.8) * 10 + 10);
        }

        public void addExperience(ulong exp) {
            stats.exp += exp;
            while (stats.exp >= expRequired()) { 

                stats.exp -= expRequired();
                stats.level++;

                calculateStats();
            }
        }
    }    
}

