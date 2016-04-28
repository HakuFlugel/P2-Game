using System;
using System.Collections.Generic;
using System.Drawing;

namespace RPGame {
    public class CharacterType {
        public Bitmap Image;

        public string imageFile;
        public string name;
        public double maxHP;
        public double attack;
        public double attackSpeed;
        public double defence;
        public ulong exp;

        public static List<CharacterType> characterTypes = new List<CharacterType>();

        static CharacterType() {
            characterTypes.Add(new CharacterType() {
                imageFile = "Content/character.png",
                name = "Player",
                maxHP = 100,
                attack = 20,
                attackSpeed = 100,
                defence = 10,
                exp = 10
            });
            characterTypes.Add(new CharacterType() {
                imageFile = "Content/ghoul.png",
                name = "Ghoul",
                maxHP = 100,
                attack = 35,
                attackSpeed = 12,
                defence = 5,
                exp = 10
            });
            characterTypes.Add(new CharacterType() {
                imageFile = "Content/zombie.png",
                name = "Zombie",
                maxHP = 130,
                attack = 24,
                attackSpeed = 8,
                defence = 12,
                exp = 10
            });
        }



        /*
                public int type { get; set; } //0 for player - 1 for ___ - 2 for ___ -3 for _____ - 4 for _____ -5 for ____ etc
                public int level { get; set; }
                public float HP { get; set; }
                public float armor { get; set; }
                public float dmg { get; set; }



                public void CharacterScale(int lvl) {
                    HP = set_HP_for_lvl(lvl);
                    dmg = set_dmg_for_lvl(lvl);
                    armor = set_armor_for_lvl(lvl);
                }

                public float set_HP_for_lvl(int lvl) {
                    return lvl * ((lvl <= 10) ? 20 : (level <= 20) ? 30 : (level <= 30) ? 40 : (level <= 40) ? 50 : 100);
                }
                public float set_dmg_for_lvl(int lvl) {
                    return lvl * ((lvl <= 10) ? 5 : (level <= 20) ? 6 : (level <= 30) ? 7 : (level <= 40) ? 8 : 10);
                }
                public float set_armor_for_lvl(int lvl) {
                    return lvl * ((lvl <= 10) ? 1 : (level <= 20) ? 2 : (level <= 30) ? 3 : (level <= 40) ? 4 : 6);
                }

                public void LoadImage(string imageString) {
                    Image = (Bitmap)Bitmap.FromFile(imageString);
                }
        */
    }    
}
  



