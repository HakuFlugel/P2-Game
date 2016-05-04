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
    }    
}
  



