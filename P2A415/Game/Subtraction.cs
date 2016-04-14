using System;

namespace WinFormsTest {


    public class Subtraction : Question {
        public Subtraction(int level) : base(level) {
            this.text = "Subtract the following numbers";

            this.operands = new int[rand.Next(2, 2 + (int)Math.Log10(level + 1))];

            for (int i = 0; i < operands.Length; i++) {
                operands[i] = rand.Next(level/5, level * 4);
                expression += operands[i];
                if (i != operands.Length)
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

