using System;

namespace WinFormsTest {
    
    public class Addition : Question{
        
        public Addition(int level) : base(level) {
            this.text = "Add the following numbers";

            this.operands = new int[rand.Next(2, 2 + (int)Math.Log10(level + 1))];

            for (int i = 0; i < operands.Length; i++) {
                operands[i] = rand.Next(level/5-(level*level/100), 4 + level * 4);
                expression += operands[i];
                if (i != operands.Length - 1)
                    expression += " + ";
                else
                    expression += " = ";
            }
        }

        public override bool validateAnswer(int answer) {
            int correctAnswer = 0;
            foreach (var operand in operands) {
                correctAnswer += operand;
            }
            return answer == correctAnswer;
        }
    }

}