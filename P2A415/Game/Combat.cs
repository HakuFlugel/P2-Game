using System;
using System.Drawing;

// this is a change because git wont fix stuff
using System.Windows.Forms;

namespace RPGame {
    public class Combat {

        Bitmap picture = ImageLoader.Load("Content/combatscreen.png");

        public Character firstCharacter;
		public Character secondCharacter;

        private Position whereThePlayerCameFrom = new Position();
		public Question currentQuestion;

		public bool hasEnded = false;

        public string answerString = "";

        private double enemyTimePerAttack = 8;
        public double enemyAttackTime = 8;


        public Combat(Character firstCharacter, Character secondCharacter) {
            this.firstCharacter = firstCharacter;
            this.secondCharacter = secondCharacter;

/*          whereThePlayerCameFrom.x = firstCharacter.position.x;
            whereThePlayerCameFrom.y = firstCharacter.position.y;*/

            whereThePlayerCameFrom = firstCharacter.position;

            currentQuestion = Question.selectQuestion(firstCharacter.stats.level);
        }

        public void keyPress(object sender, KeyPressEventArgs e) {
			if (char.IsDigit(e.KeyChar) || e.KeyChar == '-') {
                answerString += e.KeyChar;
			} else if (e.KeyChar == (char)Keys.Back && answerString.Length >= 1) {
                answerString = answerString.Substring(0, answerString.Length - 1);
			} else if (e.KeyChar == (char)Keys.Enter && answerString.Length >= 1) {
                //currentQuestion.

                bool isCorrect = false;
                try {
                    int answer = int.Parse(answerString);
                    isCorrect = currentQuestion.validateAnswer(answer);

				} catch (Exception ex) when (ex is FormatException || ex is OverflowException) {
                    Console.WriteLine(ex.Message);
                    //enemyAttackTime -= 1;
                }

                if (isCorrect) {
					doAttack();
                } else {
                    enemyAttackTime -= 1;
                    // TODO: effect, shake?
                }

				answerString = "";

                currentQuestion = Question.selectQuestion(firstCharacter.stats.level);
            }
            
        }

        public void update(double deltaTime) {
            enemyAttackTime -= deltaTime;
            if (enemyAttackTime <= 0) {
				doAttack(secondCharacter, firstCharacter);
                //firstCharacter.position.x = whereThePlayerCameFrom.x+3;
                //firstCharacter.position.y = whereThePlayerCameFrom.y+3;
                enemyAttackTime = enemyTimePerAttack;
            }

            //firstCharacter.position = whereThePlayerCameFrom; // TODO: evt. flytte til hvis man taber
        }

        public void doAttack() {
            doAttack(firstCharacter, secondCharacter);
            firstCharacter.stats.hp += firstCharacter.stats.attack / (1 + secondCharacter.stats.defence / 10) / 20;
            enemyAttackTime += 0.3333333333333333333333333;
        }

        private void doAttack(Character attacker, Character victim) {

            victim.stats.hp -= (attacker.stats.attack + attacker.stats.level*20) / (1 + victim.stats.defence / 10);
            if (victim.stats.hp <= 0) {
                attacker.addExperience((ulong)(Math.Pow(attacker.stats.level, 1.4)*1.1+5)); // TODO: skal være victim.stats.level når monstre begynder at scale

                if (attacker.stats.hp < 100) {
                    attacker.stats.hp += (100 - attacker.stats.hp) / 4; //
                }

                Game.instance.world.characters.Remove(victim);

				hasEnded = true;
                // Do victory/lose stuff
            }
        }

        public void draw(Graphics gfx) {

            gfx.DrawImage(picture, new RectangleF(0, 0, Game.instance.Width-15, Game.instance.Height),
                new Rectangle(0, 0, 800, 600), GraphicsUnit.Pixel);



            //gfx.DrawImage         //maybe todo -> HAV SEX MED HEM og derefter, lav bluuuur med image i bagrund.

            gfx.DrawImage(firstCharacter.texture,
                new RectangleF(Game.instance.Width / 2-50, Game.instance.Height / 15f, 500, 500),
                new Rectangle(0, 0, 64, 64), GraphicsUnit.Pixel);



            gfx.DrawImage(secondCharacter.texture,
                new RectangleF(Game.instance.Width / 4f-50, Game.instance.Height / 15f , 500, 500),
                new Rectangle(0, 0, 64, 64), GraphicsUnit.Pixel);

            Font bigfont = new Font("Arial", 32, FontStyle.Regular);
            Brush brush = new SolidBrush(Color.WhiteSmoke);


            double player_health = Math.Round(firstCharacter.stats.hp, 0);
            double monster_health = Math.Round(secondCharacter.stats.hp, 0);

            gfx.DrawString($@"Time left: {enemyAttackTime}", bigfont, brush, Game.instance.Width / 3f - 50, Game.instance.Height / 20f);


            gfx.DrawString($@"Health: {player_health}", bigfont, brush, Game.instance.Width / 1.8f, Game.instance.Height / 1.6f);
            gfx.DrawString($@"Health: {monster_health}", bigfont, brush, Game.instance.Width / 3.8f, Game.instance.Height / 1.6f);

            gfx.DrawString($@"{currentQuestion.text}", bigfont, brush, Game.instance.Width / 3f - 50, Game.instance.Height / 1.5f);
            gfx.DrawString($@"{currentQuestion.expression}  {answerString}", bigfont, brush, Game.instance.Width / 3f - 50, Game.instance.Height / 1.4f);


        }
    }
}

