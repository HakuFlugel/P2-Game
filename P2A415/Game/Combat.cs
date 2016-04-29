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

        private Game game;

		public bool hasEnded = false;

        public string answerString = "";

        private double enemyTimePerAttack;
        public double enemyAttackTime;


        public Combat(Game game, Character firstCharacter, Character secondCharacter) {
            this.game = game;
            this.firstCharacter = firstCharacter;
            this.secondCharacter = secondCharacter;

/*          whereThePlayerCameFrom.x = firstCharacter.position.x;
            whereThePlayerCameFrom.y = firstCharacter.position.y;*/

            whereThePlayerCameFrom = firstCharacter.position;

            enemyTimePerAttack = CharacterType.characterTypes[secondCharacter.characterType].attackSpeed;
            enemyAttackTime = enemyTimePerAttack;

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
                    enemyAttackTime -= 2.5;
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
            firstCharacter.stats.curHP += firstCharacter.stats.attack / (1 + secondCharacter.stats.defence / 10) / 20;
            enemyAttackTime += 0.3333333333333333333333333;
        }

        private void doAttack(Character attacker, Character victim) {

            victim.stats.curHP -= (attacker.stats.attack + attacker.stats.level*20) / (1 + victim.stats.defence / 10);

            // Victory/Defeat
            if (victim.stats.curHP <= 0) {
                attacker.addExperience((ulong)(Math.Pow(victim.stats.level, 1.4)*1.1+5)*100); // TODO: skal være victim.stats.level når monstre begynder at scale // TODO: fjern *100

                attacker.stats.curHP += (attacker.stats.maxHP - attacker.stats.curHP) / 4;
                

                game.world.characters.Remove(victim);

				hasEnded = true;
                // Do victory/lose stuff

                if (victim==firstCharacter) {
                    victim.position = whereThePlayerCameFrom;
                    victim.stats.curHP = victim.stats.maxHP / 16;
                    game.world.characters.Add(victim);
                    
                }
            }
        }

        public void draw(Graphics gfx) {
            int width = game.ClientSize.Width;
            int height = game.ClientSize.Height;

            gfx.DrawImage(picture, new RectangleF(0, 0, width, height),
                new Rectangle(0, 0, 800, 600), GraphicsUnit.Pixel);



            //gfx.DrawImage         //maybe todo -> HAV SEX MED HEM og derefter, lav bluuuur med image i bagrund.

            gfx.DrawImage(firstCharacter.texture,
                new RectangleF(width / 4f - 50, height / 15f, 500, 500),
                new Rectangle(0, 0, 64, 64), GraphicsUnit.Pixel);



            gfx.DrawImage(secondCharacter.texture,
                new RectangleF(width / 2 - 50, height / 15f, 500, 500),
                new Rectangle(0, 0, 64, 64), GraphicsUnit.Pixel);

            Font bigfont = new Font("Arial", 32, FontStyle.Regular);
            Font biggerfont = new Font("Arial", 44, FontStyle.Regular);

            string player_name = CharacterType.characterTypes[firstCharacter.characterType].name;
            double player_health = Math.Round(firstCharacter.stats.curHP, 0);
            double player_level = firstCharacter.stats.level;
            string monster_name = CharacterType.characterTypes[secondCharacter.characterType].name;
            double monster_health = Math.Round(secondCharacter.stats.curHP, 0);
            double monster_level = secondCharacter.stats.level;


            string timeleft = enemyAttackTime.ToString("0.#0");
            Bitmap barimg = new Bitmap("Content/blankbar.png");
            Bitmap barimg2 = new Bitmap("Content/blankbar2.png");

            gfx.DrawImage(barimg2, new RectangleF(width / 2 - ((width / 2) - 70) / 2, height / 22f, (width / 2) - 70, 50), new Rectangle(0, 0, 1, 1), GraphicsUnit.Pixel);
            gfx.DrawImage(barimg, new RectangleF(width / 2 - ((width / 2) - 70) / 2 + 2, height / 20f, (float)(enemyAttackTime / enemyTimePerAttack * (width / 2 - 100)), 44), new Rectangle(0, 0, 1, 1), GraphicsUnit.Pixel);
            gfx.DrawString($@"{timeleft}", bigfont, Brushes.OrangeRed, width / 2f - gfx.MeasureString(timeleft,bigfont).Width / 2, height / 20f);


            gfx.DrawString($@"{player_name}
Health: {player_health}
Level: {player_level}", bigfont, Brushes.WhiteSmoke, width / 10.0f, height / 4.0f);

            gfx.DrawString($@"{monster_name}
Health: {monster_health}
Level: {monster_level}", bigfont, Brushes.WhiteSmoke, width / 1.3f, height / 4.0f);


            gfx.DrawString($@"{currentQuestion.text}", bigfont, Brushes.WhiteSmoke, width / 3f - 50, height / 1.3f);
            gfx.DrawString($@"{currentQuestion.expression}  {answerString}", biggerfont, Brushes.WhiteSmoke, width / 3f - 50, height / 1.2f);

        }
    }
}

