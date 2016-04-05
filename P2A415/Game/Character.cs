<<<<<<< HEAD
﻿using System;
using System.Drawing;

namespace WinFormsTest {

    public struct Stats {
        public double hp;// = 100.0;
        public double attack; // Should be updated to match level and possibly gear
        public double defence;// = 1.0;

        public ulong level;// = 0;
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
        public float layer = 0.5f;

        public Position position;
        public Stats stats = new Stats();

        public Combat currentCombat;

        public Character() : this(0, 0) {}
        public Character(long x, long y)
        {
            position.x = x;
            position.y = y;

            texture = (Bitmap)Bitmap.FromFile("Content/character.png");
                //Game.instance.Content.Load<Texture2D>("character.png");
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
            return true;
        }
    }
        
}

=======
﻿using System;
using System.Drawing;

namespace WinFormsTest {

    public struct Stats {
        double hp;// = 100.0;
        double attack;// = 1.0;
        double defence;// = 1.0;

        ulong level;// = 0;
        ulong exp;// = 0.0;


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
        public Bitmap texture;
        public float layer = 0.5f;

        public Position position;
        public Stats stats = new Stats();

        public Character() : this(0, 0) {}
        public Character(long x, long y)
        {
            position.x = x;
            position.y = y;

            texture = ImageLoader.Load(CharacterType.characterTypes[0].imageFile);
                //Game.instance.Content.Load<Texture2D>("character.png");
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
                position.x += x;
                position.y += y;

                position.xoffset -= x;
                position.yoffset -= y;

                position.offsetScale = 1.0f;
            } // else // TODO: feedback?
        }

        public bool canMove(long x, long y) {
            //if Game.instance.wo
            return true;
        }
    }
        
}

>>>>>>> master
