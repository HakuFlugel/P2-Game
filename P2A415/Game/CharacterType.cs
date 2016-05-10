using System;
using System.Collections.Generic;
using System.Drawing;

namespace RPGame {
    public class CharacterType {

        public string imageFile;
        public string name;
        public double maxHP;
        public double attack;
        public double attackSpeed;
        public double armorPen;
        public double defence;
        public ulong exp;

        public static List<CharacterType> characterTypes = new List<CharacterType>();

        static CharacterType() {
            characterTypes.Add(new CharacterType() {
                imageFile = "Content/character.png",
                name = "Player",
                maxHP = 100,
                attack = 40,
                attackSpeed = 0.75,
                armorPen = 5,
                defence = 7,
                exp = 10
            });

            characterTypes.Add(new CharacterType() {
                imageFile = "Content/ghoul.png",
                name = "Ghoul",
                maxHP = 100,
                attack = 35,
                attackSpeed = 7,
                armorPen =5.2,
                defence = 4,
                exp = 10
            });

            characterTypes.Add(new CharacterType() {
                imageFile = "Content/zombie.png",
                name = "Zombie",
                maxHP = 130,
                attack = 24,
                attackSpeed = 9,
                armorPen = 5.2,
                defence = 6,
                exp = 13
            });
        }
    }    
}
  



