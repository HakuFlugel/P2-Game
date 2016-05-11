using System;

namespace RPGame {
//    public struct Currency {
//        ulong triangles; // Geometri 
//        ulong squares; // Algebra
//        ulong pentagons; // Statistik
//
//        ulong hexagons; // Combined tringle+square+pentagon
//    }

    public class Player {
        public Character character;
        public Inventory inventory;

        public PlayerInput input = new PlayerInput();

        public Player(Region region) {
            character = new Character(region, 0, region.x * 32 + region.townx, region.y * 32 + region.towny, 0);
            inventory = new Inventory(this);
            character.inventory = inventory;

            // TODO: debug code below
            character.stats.level = 42;
            character.position.x = 140;
            character.position.y = 150;
        }

        public void update(Game game,double deltaTime) {
            if (character.currentCombat != null) { // Is the player in combat?

                character.currentCombat.update(deltaTime);
                if (character.currentCombat.hasEnded) {
                    character.currentCombat = null;
                }

            }else { // Not in combat

                if (character.position.offsetScale <= -Character.moveDelay) { // Slight delay before being able to move again

                    // Move the player according to their input
                    if (input.moveUp) {
                        character.move(game, 0, 1);
                    } else if (input.moveDown) {
                        character.move(game, 0, -1);
                    } else if (input.moveRight) {
                        character.move(game, 1, 0);
                    } else if (input.moveLeft) {
                        character.move(game, -1, 0);
                    }
                }
            }
        }
    }
}
