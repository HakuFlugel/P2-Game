using System;
using System.Drawing;
using System.Windows.Forms;

namespace RPGame {
    public class Combat {

        private Game game;
        private Position whereThePlayerCameFrom = new Position();
        Bitmap picture = ImageLoader.Load("Content/combatscreen.png");

        public Character firstCharacter;
		public Character secondCharacter;

		public Question currentQuestion;
        public string answerString = "";

        SolidBrush barForeground = new SolidBrush(Color.WhiteSmoke);
        SolidBrush barBackground = new SolidBrush(Color.Black);
        SolidBrush questionBackground = new SolidBrush(Color.FromArgb(128,Color.Black));

        Font mediumfont = new Font("Arial", 18, FontStyle.Regular);
        Font bigfont = new Font("Arial", 72, FontStyle.Regular);
        Font biggerfont = new Font("Arial", 96, FontStyle.Regular);
        Font font = new Font("Arial", 32, FontStyle.Regular);

        SizeF barSize = new SizeF(768, 24);

        public bool hasEnded = false;

        private double enemyTimePerAttack;
        public double enemyAttackTime;

        public Combat(Game game, Character firstCharacter, Character secondCharacter) {
            this.game = game;
            this.firstCharacter = firstCharacter;
            this.secondCharacter = secondCharacter;

            whereThePlayerCameFrom = firstCharacter.position;

            enemyTimePerAttack = secondCharacter.stats.attackSpeed;
            enemyAttackTime = enemyTimePerAttack;
            currentQuestion = Question.selectQuestion((firstCharacter.stats.level + secondCharacter.stats.level)/2);
            game.localPlayer.statistics.encounters++;

            resize();
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
                    game.localPlayer.statistics.correct++;
                    doAttack();
                } else {
                    game.localPlayer.statistics.wrong++;
                    enemyAttackTime -= 2.5;
                    // TODO: effect, shake?
                }

				answerString = "";

                currentQuestion = Question.selectQuestion((firstCharacter.stats.level + secondCharacter.stats.level)/2);
            }  
        }

        internal void resize() {
            int width = game.ClientSize.Width;
            int height = game.ClientSize.Height;

            float scaleX, scaleY, scaleFinal;
            scaleX = width / 1920.0f;
            scaleY = height / 1080.0f;
            scaleFinal = Math.Min(scaleX, scaleY);
            scaleFinal = Math.Max(scaleFinal, 0.01f);

            font = new Font("Arial", 32 * scaleFinal, FontStyle.Regular);
            mediumfont = new Font("Arial", 18 * scaleFinal, FontStyle.Regular);
            bigfont = new Font("Arial", 72 * scaleFinal, FontStyle.Regular);
            biggerfont = new Font("Arial", 96 * scaleFinal, FontStyle.Regular);

            barSize = new SizeF(768 * scaleFinal, 32 * scaleFinal);
        }

        public void update(double deltaTime) {
            enemyAttackTime -= deltaTime * (game.Focused ? 1.0 : 1.1); // Penalty if game is not focused to punish using the calculator
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
            victim.stats.curHP -= (attacker.stats.attack) / Math.Max(0.5, 1 + (victim.stats.defence - attacker.stats.armorPen)/20.0);

            // Victory/Defeat
            if (victim.stats.curHP <= 0) {
                ulong exp = (ulong)(Math.Pow(1.20, victim.stats.level) * 4 + Math.Pow(1.40, victim.stats.level) * 2 + Math.Pow(1.60, victim.stats.level) * 1);
                int lvl_raised = attacker.addExperience(game, exp);

                if(!victim.Equals(game.localPlayer.character))
                    game.loot = new Looting(game,lvl_raised,exp,secondCharacter.stats.level);

                attacker.stats.curHP += (attacker.stats.maxHP - attacker.stats.curHP) / 4;

            game.world.regions[firstCharacter.position.x / 32,firstCharacter.position.y / 32].characters.Remove(victim);

				hasEnded = true;
                // Do victory/lose
                if (victim==secondCharacter) {
                    game.localPlayer.statistics.kills++;
                    game.localPlayer.statistics.monsterHighestLevel = secondCharacter.stats.level;
                }
                if (victim==firstCharacter) {
                    game.localPlayer.statistics.distance--;
                    game.localPlayer.statistics.deaths++;
                    victim.position = whereThePlayerCameFrom;
                    victim.stats.curHP = victim.stats.maxHP / 16;
                    game.world.regions[firstCharacter.position.x / 32,firstCharacter.position.y / 32].characters.Add(victim);
                }
            }
        }

        public void draw(Graphics gfx) {
            int width = game.ClientSize.Width;
            int height = game.ClientSize.Height;

            // Draw Combat background
            gfx.DrawImage(picture, new RectangleF(0, 0, width, height),
                new Rectangle(0, 0, 799, 599), GraphicsUnit.Pixel);

            // Draw character models
            int characterImageSize = 512;
            float sizeX, sizeY, sizeFinal;
            sizeX = characterImageSize / 1920.0f * width;
            sizeY = characterImageSize / 1080.0f* height;
            sizeFinal = Math.Min(sizeX, sizeY);

            //player
            gfx.DrawImage(firstCharacter.texture,
                new RectangleF(width / 3 - sizeFinal / 2, height / 2f - sizeFinal * 0.8f, sizeFinal, sizeFinal),
                new Rectangle(0, 0, 64, 64), GraphicsUnit.Pixel);

            //monster
            gfx.DrawImage(secondCharacter.texture,
                new RectangleF(width / 3 * 2 - sizeFinal / 2, height / 2f - sizeFinal * 0.8f, sizeFinal, sizeFinal),
                new Rectangle(0, 0, 64, 64), GraphicsUnit.Pixel);

            string monster_name = CharacterType.characterTypes[secondCharacter.characterType].name;
            double monster_health = Math.Round(secondCharacter.stats.curHP, 0);
            double monster_level = secondCharacter.stats.level;

            string timeleft = enemyAttackTime.ToString("0.00");

            // Draw time bar
            RectangleF barRect = new RectangleF(
                width  / 2  - barSize.Width / 2,
                height / 64,
                barSize.Width,
                barSize.Height
            );
            drawBar(gfx, barRect, enemyAttackTime / enemyTimePerAttack);

            // Draw questionbar
            RectangleF questionRect = new RectangleF(
                width / 16,
                height * 3 / 5,
                width - width / 8 ,
                height / 3);

            gfx.FillRectangle(questionBackground, questionRect);

            // Draw character info
            StringFormat stringFormat = new StringFormat();
            stringFormat.Alignment = StringAlignment.Center;
            stringFormat.LineAlignment = StringAlignment.Center;
            gfx.DrawString($"{timeleft}", mediumfont, Brushes.OrangeRed, barRect, stringFormat);
            
            // draw player and monster text
            drawinfo(gfx, 0, firstCharacter); 
            drawinfo(gfx, 1, secondCharacter); 

            // draw question
            SizeF sizeOfQustionText = gfx.MeasureString(currentQuestion.text,bigfont);
            SizeF sizeOfExpression = gfx.MeasureString(currentQuestion.expression, biggerfont);

            gfx.DrawString($@"{currentQuestion.text}", bigfont, Brushes.WhiteSmoke, (width /2) - sizeOfQustionText.Width/2, height / 1.55f);
            gfx.DrawString($@"{currentQuestion.expression}  {answerString}", biggerfont, Brushes.WhiteSmoke, ((width / 2) - sizeOfExpression.Width / 2)- width/8, height / 1.35f);
        }

        public void drawinfo(Graphics gfx, int i, Character character) {
            if (i > 1 || i < 0) {
                throw new ArgumentOutOfRangeException("Can be either left(0) or right(1)");
            }

            string name = CharacterType.characterTypes[character.characterType].name;
            double health = Math.Round(character.stats.curHP, 0);
            double level = character.stats.level;
            string text = $@"Name: { name}" + "\n" + $@"Health: {health}" + "\n" + $@"Level: {level}";

            int padding = 4;
            SizeF size = gfx.MeasureString(text, font);
            float width = game.ClientSize.Width, height = game.ClientSize.Height;

            RectangleF uiRect = new RectangleF(
                i == 0 ? (width / 64) : (width / 64 * 63 - size.Width - 2 * padding),
                height / 3,
                size.Width + 2 * padding, 
                size.Height + 2 * padding);
            RectangleF textRect = new RectangleF(uiRect.X + padding, uiRect.Y + padding, size.Width, size.Height);
            gfx.FillRectangle(questionBackground, uiRect);

            drawBar(gfx, uiRect, character.stats.curHP / character.stats.maxHP, true);
            gfx.DrawString(text, font, Brushes.WhiteSmoke, textRect);
        }

        public void drawBar(Graphics gfx, RectangleF barBackgroundRect, double fraction, bool isHP = false ) {
            
            const int padding = 4;

            fraction = Math.Min(fraction, 1.0);
            fraction = Math.Max(fraction, 0.0);

            RectangleF barForegroundRect = new RectangleF(
                barBackgroundRect.X + padding,
                barBackgroundRect.Y + padding,
                (int)((barBackgroundRect.Width - 2 * padding) * fraction),
                barBackgroundRect.Height - 2 * padding
            );

            gfx.FillRectangle(barBackground, barBackgroundRect);
            if (fraction > 0) {
                gfx.FillRectangle(isHP ? new SolidBrush(Color.FromArgb((int)(255*(1-fraction)), (int)(192*fraction), 32)) : barForeground, barForegroundRect);

            }
        }
    }
}