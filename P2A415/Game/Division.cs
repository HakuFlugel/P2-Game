using System;

namespace RPGame {
    public class Division : Question {
        int correctAnswer;

        public Division(int level) : base(level) {
            this.text = "Divide the following numbers";

            this.operands = new int[rand.Next(2, 2 + (int)Math.Log10(level + 1))];
            int length = operands.Length;

            correctAnswer = rand.Next(2, 2 + (int)Math.Sqrt(level + 1));

            operands[0] = correctAnswer;
            for (int index = 1; index < length; index++) {
                operands[index] = rand.Next(2, 2 + (int)Math.Log(level + 1) * 4);
                operands[0] *= operands[index];
            }

            for (int index = 0; index < length; index++) {
                expression += operands[index];
                if (index != length - 1)
                    expression += " / ";
                else
                    expression += " = ";
            }
        }

        public override bool validateAnswer(int answer) {
            return answer == correctAnswer;
        }
    }
}
