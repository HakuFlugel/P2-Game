﻿using System;
using System.IO;

namespace WinFormsTest {
    public struct Currency {
        ulong triangles; // Geometri 
        ulong squares; // Algebra
        ulong pentagons; // Statistik

        ulong hexagons; // Combined tringle+square+pentagon
    }

    public class Player {
        public Character character;

        public Currency currency;

        public PlayerInput input = new PlayerInput();

        public Player(long x = 0, long y = 0) {
            character = new Character(x, y);
            character.layer = 1.0f;
        }

        public void update(double deltaTime)
        {
            var test = typeof(Character);
            if (character.currentCombat != null) { // Is the player in combat?

                character.currentCombat.doAttack();
                character.currentCombat.update(deltaTime);
                character.currentCombat = null;

            }else { // Not in combat



                if (character.position.offsetScale <= -Character.moveDelay) { // Slight delay before being able to move again
               
                    // Move the player according to their input
                    if (input.moveUp) {
                        character.move(0, 1);
                    } else if (input.moveDown) {
                        character.move(0, -1);

                    } else if (input.moveRight) {
                        character.move(1, 0);
                    } else if (input.moveLeft) {
                        character.move(-1, 0);
                    }
                }
            }
        }


    }
}

