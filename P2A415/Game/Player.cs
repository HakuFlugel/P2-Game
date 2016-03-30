using System;

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
            //KeyboardState keyboardState = Keyboard.GetState();

            if (character.position.offsetScale <= 0)
            {
                character.position.xoffset = 0.0f;
                character.position.yoffset = 0.0f;
            }

            if (character.position.offsetScale <= -0.25f) // Slight delay before being able to move again
            {
               
                if (input.moveUp)
                {
                    character.move(0, 1);
                }
                else if (input.moveDown)
                {
                    character.move(0, -1);

                }
                else if (input.moveRight)
                {
                    character.move(1, 0);
                }
                else if (input.moveLeft)
                {
                    character.move(-1, 0);
                }
            }
            else
            {
                character.position.offsetScale -= 4.0f*(deltaTime);

            }
        }


    }
}

