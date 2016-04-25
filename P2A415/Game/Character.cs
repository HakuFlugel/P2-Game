using System;
using System.Drawing;

namespace WinFormsTest {

    public struct Stats {
        public double hp;// = 100.0;
        public double attack;// = 1.0;
        public double defence;// = 1.0;

        public int level;// = 0;
        public ulong exp;// = 0.0;


        // TODO: load ctor?
    }

    public struct Position {
        public long x;// = 0;
        public long y;// = 0;

        public double xoffset;// = 0.0;
        public double yoffset;// = 0.0;

        public double offsetScale;

        public Position (long x, long y) {
            this.x = x;
            this.y = y;

            xoffset = 0;
            yoffset = 0;

            offsetScale = 0;
        }
    }

    public class Character {
        public static double moveDelay = 0.25;

        public Bitmap texture;

        public Position position;
        public Stats stats = new Stats();

        public int characterType;

        public Combat currentCombat;

        public Character(int characterType) : this(characterType, 0, 0) {}
        public Character(int characterType, long x, long y)
        {
            position.x = x;
            position.y = y;

            this.characterType = characterType;

            texture = ImageLoader.Load(CharacterType.characterTypes[characterType].imageFile);
                //Game.instance.Content.Load<Texture2D>("character.png");

            stats.hp = 100;
            stats.attack = 25;
            stats.defence = 2;
        }

        public void update(double deltaTime) {
            if (position.offsetScale > -Character.moveDelay) { // Slight delay before being able to move again
                // Animate movement
                position.offsetScale -= 4.0f * (deltaTime);

            }

            // If the character has reached the tile, then set the offset to 0,0
            if (position.offsetScale <= 0) {
                position.xoffset = 0.0f;
                position.yoffset = 0.0f;
            }
        }

        public void draw(Graphics gfx, Position cameraPosition)
        {
            double x, y;
            x = ((position.x - cameraPosition.x) + (position.xoffset * position.offsetScale - cameraPosition.xoffset * cameraPosition.offsetScale)) * 64*2 + Game.instance.Width  / 2  - 64;
            y = ((position.y - cameraPosition.y) + (position.yoffset * position.offsetScale - cameraPosition.yoffset * cameraPosition.offsetScale)) * 64*2 - Game.instance.Height / 2  + 64;

            //gfx.DrawImage(texture, (float)x, (float)y);
            gfx.DrawImage(texture, new RectangleF((float)x, -(float)y, 64.0f*2, 64.0f*2), new Rectangle(0,0,64,64), GraphicsUnit.Pixel);
            //Position*image size*image scale
            //Game.instance.spriteBatch.Draw(texture,
            //    new Vector2((position.x + (position.xoffset * position.offsetScale))*64, (position.y + (position.yoffset * position.offsetScale))*-64)
            //    , null, Color.White, 0.0f, new Vector2(32f, 32f), 1.0f, SpriteEffects.None, layer);

        }

        public void move(long x, long y) {
            if (canMove(position.x + x, position.y + y)) {
                for (int i = 0; i < Game.instance.world.characters.Count; i++) {

                    Character character = Game.instance.world.characters[i];

                    if (character != this
                        && character.position.x == this.position.x + x
                        && character.position.y == this.position.y + y)
                    {
                        currentCombat = new Combat(this, character);
                        break;

                    }
                }


                position.x += x;
                position.y += y;

                position.xoffset -= x;
                position.yoffset -= y;

                position.offsetScale = 1.0f;
            } // else // TODO: feedback?

        }

        public bool canMove(long x, long y) {
            //if Game.instance.wo
            try {
                return TileType.tileTypes[Game.instance.world[x,y]].Moveable;
            } catch (Exception e) {
                Console.WriteLine("Can't move: " + e);
                return false;
            }

        }

        public ulong expRequired() {
            return expRequired(stats.level);
        }

        public static ulong expRequired(int level) {
            return (ulong)(Math.Pow(level, 1.8) * 10 + 10);
        }

        public void addExperience(ulong exp) {
            stats.exp += exp;
            while (stats.exp >= expRequired()) { // In theory you shouldn't be able to gain enough experience for muliple levels

                stats.exp -= expRequired();
                stats.level++;
            }
            //stats.level = x^(1/2.5)/2.5
        }
    }
        
}

