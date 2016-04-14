using System;
using System.Drawing;

// this is a change because git wont fix stuff
using System.Windows.Forms;

namespace WinFormsTest {
    public class Combat {

        Character firstCharacter;
        Character secondCharacter;

        Position whereThePlayerCameFrom = new Position();
        Question currentQuestion;

        string answerString;

        double enemyTimePerAttack = 10;
        double enemyAttackTime = 10;


        public Combat(Character firstCharacter, Character secondCharacter) {
            this.firstCharacter = firstCharacter;
            this.secondCharacter = secondCharacter;


            whereThePlayerCameFrom.x = firstCharacter.position.x;
            whereThePlayerCameFrom.y = firstCharacter.position.y;


            whereThePlayerCameFrom = firstCharacter.position;
        }

        public void keyPress(object sender, KeyPressEventArgs e) {
            if (char.IsDigit(e.KeyChar)) {
                answerString += e.KeyChar;
            } else if (e.KeyChar == (char)Keys.Back) {
                answerString = answerString.Substring(0, answerString.Length - 1);
            } else if (e.KeyChar == (char)Keys.Enter) {
                //currentQuestion.
            }
            
        }

        public void update(double deltaTime) {
            enemyAttackTime -= deltaTime;
            if (enemyAttackTime <= 0) {
                //doAttack(enemy, player);
                firstCharacter.position.x = whereThePlayerCameFrom.x+3;
                firstCharacter.position.y = whereThePlayerCameFrom.y+3;
                enemyAttackTime = enemyTimePerAttack;
            }

            firstCharacter.position = whereThePlayerCameFrom;
        }

        public void doAttack() {
            doAttack(firstCharacter, secondCharacter);
        }

        private void doAttack(Character attacker, Character victim) {

            victim.stats.hp -= attacker.stats.attack / victim.stats.defence;
            if (victim.stats.hp <= 0) {
                // Do victory/lose stuff
            }
        }

        //public void draw();
    }
}
