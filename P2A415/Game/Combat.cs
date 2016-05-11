using System;
using System.Drawing;
using System.Windows.Forms;

namespace RPGame {
    public class Combat {

        Bitmap picture = ImageLoader.Load("Content/combatscreen.png");

        public Character firstCharacter;
		public Character secondCharacter;

        private Position whereThePlayerCameFrom = new Position();
		public Question currentQuestion;

        private Game game;

        SolidBrush barForeground = new SolidBrush(Color.White);
        SolidBrush barBackground = new SolidBrush(Color.Black);

		public bool hasEnded = false;

        public string answerString = "";

        private double enemyTimePerAttack;
        public double enemyAttackTime;

        public Combat(Game game, Character firstCharacter, Character secondCharacter) {
            this.game = game;
            this.firstCharacter = firstCharacter;
            this.secondCharacter = secondCharacter;

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
                enemyAttackTime = enemyTimePerAttack;
            }
        }

        public void doAttack() {
            doAttack(firstCharacter, secondCharacter);
            firstCharacter.stats.curHP += firstCharacter.stats.attack / Math.Max(0.5, 1 + (secondCharacter.stats.defence - firstCharacter.stats.armorPen)/20.0) * 0.05;
            enemyAttackTime = Math.Min(enemyAttackTime + firstCharacter.stats.attackSpeed, enemyTimePerAttack);
        }

        private void doAttack(Character attacker, Character victim) {

            victim.stats.curHP -= (attacker.stats.attack) / Math.Max(0.5, 1 + (secondCharacter.stats.defence - firstCharacter.stats.armorPen)/20.0);

            // Victory/Defeat
            if (victim.stats.curHP <= 0) {
                ulong exp = (ulong)(Math.Pow(victim.stats.level, 1.4) * 1.1 + 5);
                int lvl_raised = attacker.addExperience(exp); //Scale xp

                if(!victim.Equals(game.localPlayer.character))
                    game.loot.show(exp, lvl_raised, victim.stats.level);

                attacker.stats.curHP += (attacker.stats.maxHP - attacker.stats.curHP) / 4;


            game.world.regions[firstCharacter.position.x / 32,firstCharacter.position.y / 32].characters.Remove(victim);

				hasEnded = true;
                // Do victory/lose stuff

                if (victim==firstCharacter) {
                    victim.position = whereThePlayerCameFrom;
                    victim.stats.curHP = victim.stats.maxHP / 16;
                    game.world.regions[firstCharacter.position.x / 32,firstCharacter.position.y / 32].characters.Add(victim);
                    
                }
            }
        }

        public void draw(Graphics gfx) {
            int width = game.ClientSize.Width;
            int height = game.ClientSize.Height;

            gfx.DrawImage(picture, new RectangleF(0, 0, width, height),
                new Rectangle(0, 0, 800, 600), GraphicsUnit.Pixel);

            gfx.DrawImage(firstCharacter.texture,
                new RectangleF(width / 4f - 32, height / 4f - 32, 500, 500),
                new Rectangle(0, 0, 64, 64), GraphicsUnit.Pixel);

            gfx.DrawImage(secondCharacter.texture,
                new RectangleF(width / 2 - 32, height / 4f -32, 500, 500),
                new Rectangle(0, 0, 64, 64), GraphicsUnit.Pixel);

            Font bigfont = new Font("Arial", 32, FontStyle.Regular);
            Font biggerfont = new Font("Arial", 44, FontStyle.Regular);

            string player_name = CharacterType.characterTypes[firstCharacter.characterType].name;
            double player_health = Math.Round(firstCharacter.stats.curHP, 0);
            double player_level = firstCharacter.stats.level;
            string monster_name = CharacterType.characterTypes[secondCharacter.characterType].name;
            double monster_health = Math.Round(secondCharacter.stats.curHP, 0);
            double monster_level = secondCharacter.stats.level;

            string timeleft = enemyAttackTime.ToString("0.00");

            const int padding = 4;
            const int barWidth = 768;
            const int barHeight = 48;

            Rectangle barBackgroundRect = new Rectangle(
                width  / 2  - barWidth / 2      - padding,
                height / 64,
                barWidth  + 2 * padding,
                barHeight + 2 * padding
            );

            Rectangle barForegroundRect = new Rectangle(
                barBackgroundRect.X + padding,
                barBackgroundRect.Y + padding,
                (int)((barBackgroundRect.Width - 2 * padding) * (enemyAttackTime/enemyTimePerAttack)),
                barBackgroundRect.Height - 2 * padding
            );

            gfx.FillRectangle(barBackground, barBackgroundRect);
            gfx.FillRectangle(barForeground, barForegroundRect);

            StringFormat stringFormat = new StringFormat();
            stringFormat.Alignment = StringAlignment.Center;
            stringFormat.LineAlignment = StringAlignment.Center;
            gfx.DrawString($"{timeleft}", bigfont, Brushes.OrangeRed, barBackgroundRect, stringFormat);
            
            // draw player and monster text
            string left = $@"Name: {player_name}" + "\n" + $@"Health: {player_health} " + "\n" + $@"Level: {player_level}";
            string right = $@"Name: { monster_name}" + "\n" + $@"Health: {monster_health}" + "\n" + $@"Level: {monster_level}";

            drawinfo(left, gfx, 0); // left
            drawinfo(right, gfx, 1); // right

            gfx.DrawString($@"{currentQuestion.text}", bigfont, Brushes.WhiteSmoke, width / 3f - 50, height / 1.3f);
            gfx.DrawString($@"{currentQuestion.expression}  {answerString}", biggerfont, Brushes.WhiteSmoke, width / 3f - 50, height / 1.2f);

        }


        public void drawinfo(string text, Graphics gfx, int i ) {

            if (i > 1 || i < 0) {
                throw new ArgumentOutOfRangeException("Can be either left(0) or right(1)");
            }

            SolidBrush background = new SolidBrush(Color.FromArgb(128, Color.Black));
            int padding = 4;
            Font font = new Font("Arial", 32, FontStyle.Regular);

            SizeF size = gfx.MeasureString(text, font);
            PointF boxCenter = new PointF(i==0 ? game.ClientSize.Width/8 : game.ClientSize.Width/8*7, game.ClientSize.Height / 3);

            RectangleF uiRect = new RectangleF(boxCenter.X - size.Width/2-padding, boxCenter.Y - size.Height / 2 - padding, size.Width + 2 * padding, size.Height + 2 * padding);
            RectangleF textRect = new RectangleF(uiRect.X + padding, uiRect.Y + padding, size.Width, size.Height);
            gfx.FillRectangle(background, uiRect);

            gfx.DrawString(text, font, Brushes.WhiteSmoke, textRect);

        }
    }
}

