using System;

namespace RPGame {


    public class Subtraction : Question {
        public Subtraction(int level) : base(level) {
            this.text = "Subtract the following numbers";

            this.operands = new int[rand.Next(2, 2 + (int)Math.Log10(level + 1))];
            int length = operands.Length;

            for (int i = 0; i < length; i++) {
                operands[i] = rand.Next(level/5, 4 + level * 4);
                expression += operands[i];
                if (i != length - 1)
                    expression += " - ";
                else
                    expression += " = ";
            }
        }

        public override bool validateAnswer(int answer) {
            int correctAnswer = operands[0];

            for (int i = 1; i < operands.Length; i++) {
                correctAnswer -= operands[i];
            }

            return answer == correctAnswer;
        }
    }

}

