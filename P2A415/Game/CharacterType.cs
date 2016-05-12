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

        public static List<CharacterType> characterTypes = new List<CharacterType>();

        static CharacterType() {
            characterTypes.Add(new CharacterType() {
                imageFile = "Content/character.png",
                name = "Player",
                maxHP = 100,
                attack = 20,
                attackSpeed = 0.75,
                armorPen = 7,
                defence = 7,
            });

            characterTypes.Add(new CharacterType() {
                imageFile = "Content/zombie.png",
                name = "Zombie",
                maxHP = 64,
                attack = 24,
                attackSpeed = 9,
                armorPen = 5.2,
                defence = 5,
            });

            characterTypes.Add(new CharacterType() {
                imageFile = "Content/bat.png",
                name = "Bat",
                maxHP = 52,
                attack = 28,
                attackSpeed = 8,
                armorPen = 5,
                defence = 4,
            });

            characterTypes.Add(new CharacterType() {
                imageFile = "Content/spider.png",
                name = "Spider",
                maxHP = 56,
                attack = 26,
                attackSpeed = 7,
                armorPen = 4.6,
                defence = 4.5,
            });
        }
    }    
}
  



