﻿using System;
using System.Drawing;

namespace RPGame {

    public struct Stats {
        public double curHP;// = 100.0;
        public double maxHP;
        public double attack;// = 1.0;
        public double defence;// = 1.0;
        public double armorPen;
        public double attackSpeed;

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
        public Combat currentCombat;
        public Inventory invetory;

        public int characterType;

        public Character(int characterType, long x, long y, int level) {
            position.x = x;
            position.y = y;

            this.stats.level = level;

            this.characterType = characterType;

            CharacterType charType = CharacterType.characterTypes[characterType];

            texture = ImageLoader.Load(charType.imageFile);
            //game.ClientSize.Content.Load<Texture2D>("character.png");
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
                if (region.townx == position.x && region.towny == position.y) {
                    stats.curHP = stats.maxHP;
                } 
            }
        }

        public void draw(Game game, Graphics gfx, Position cameraPosition) {
            double x, y;
            x = ((position.x - cameraPosition.x) + (position.xoffset * position.offsetScale - cameraPosition.xoffset * cameraPosition.offsetScale)) * 64*2 + game.ClientSize.Width  / 2  - 64;
            y = ((position.y - cameraPosition.y) + (position.yoffset * position.offsetScale - cameraPosition.yoffset * cameraPosition.offsetScale)) * 64*2 - game.ClientSize.Height / 2  + 64;

            gfx.DrawImage(texture, new RectangleF((float)x, -(float)y, 64.0f*2, 64.0f*2), new Rectangle(0,0,63,63), GraphicsUnit.Pixel);

        }

        public void move(Game game,long x, long y) {
            if (canMove(game ,position.x + x, position.y + y)) {
                int length = game.world.characters.Count;
                for (int i = 0; i < length; i++) {

                    Character character = game.world.characters[i];

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

                position.xoffset -= x;
                position.yoffset -= y;

                position.offsetScale = 1.0f;
            } // else // TODO: feedback?
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
            double[] invetentoryStats;
            invetory.calStats(out invetentoryStats);    //tempHP, tempDefence, tempAttack, tempPen, tempSpeed

            if (charType.name == "Player") {
                stats.maxHP = charType.maxHP * Math.Pow(1.05, stats.level)       + invetentoryStats[0];
                stats.defence = charType.defence * Math.Pow(1.05, stats.level)   + invetentoryStats[1];
                stats.attack = charType.attack * Math.Pow(1.05, stats.level)     + invetentoryStats[2];
                stats.armorPen = charType.armorPen * Math.Pow(1.03, stats.level) + invetentoryStats[3];
                stats.attackSpeed = charType.attackSpeed +                       + invetentoryStats[4];
            } else { // mobs
                stats.maxHP = charType.maxHP * Math.Pow(1.07, stats.level);
                stats.defence = charType.defence * Math.Pow(1.05, stats.level); ;
                stats.attack = charType.attack * Math.Pow(1.07, stats.level);
                stats.armorPen = charType.armorPen * Math.Pow(1.07, stats.level);
                stats.attackSpeed = charType.attackSpeed + (3 / (stats.level / 5 + 1));
            }

            stats.curHP = stats.maxHP;
          
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

                calculateStats();
            }
            //stats.level = x^(1/2.5)/2.5
        }
    }
        
}

