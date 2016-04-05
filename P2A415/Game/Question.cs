using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace WinFormsTest {
    public class QuestionType {
        public static List<QuestionType> list = new List<QuestionType>();
        static QuestionType() {
            Random random = new Random();

            list.Add(new QuestionType(
                "42",
                (Question question, long level) => {
                    question.level = level;
                    question.text = "The right answer is always 42!";

                },
                (Question question, long answer ) => {
                    return answer == 42;
                }
            ));

            //
            /*list += new QuestionType(
                "Addition",
                (Question question, long level) => {
                    question.level = level;
                    question.text = "Add the following numbers together";

                    question.operands = new int[random.Next(2, 2+(int)Math.Log10(level+1))];

                    for (int i = 0; i < question.operands.Length; i++) {

                        question.operands[i] = random.Next((level/25)*(level/25), level * 4);

                        question.expression += question.operands[i];
                        if (i != question.operands.Length)
                            question.expression += " + ";
                        else
                            question.expression += " = ";
                    }
                },
                (Question question, long answer) => {
                    int correctAnswer = 0;
                    foreach (var operand in question.operands) {
                        correctAnswer += operand;
                    }

                    return answer == correctAnswer;
                }
            );

            //
            list += new QuestionType(
                "Addition",
                (Question question, long level) => {
                    question.level = level;
                    question.text = "Add the following numbers together";

                    question.operands = new int[random.Next(2, 2+(int)Math.Log10(level+1))];

                    for (int i = 0; i < question.operands.Length; i++) {

                        question.operands[i] = random.Next(, level * 4);

                        question.expression += question.operands[i];
                        if (i != question.operands.Length)
                            question.expression += " + ";
                        else
                            question.expression += " = ";
                    }
                },
                (Question question, long answer) => {
                    int correctAnswer = 0;
                    foreach (var operand in question.operands) {
                        correctAnswer += operand;
                    }

                    return answer == correctAnswer;
                }
            );*/
        }

        public long requiredLevel = 0;
        public string category;

        public QuestionType(string category, Initializer initialize, AnswerValidator validateAnswer) {
            this.category = category;
        }

        public delegate void Initializer(Question question, long level);
        public delegate bool AnswerValidator(Question question, long answer);

        public Initializer initialize; //level
        public AnswerValidator validateAnswer; //answer
    }

    public class Question {



        public long level = 0;
        //public int scale

        public long[] operands;

        public string text;
        public string expression;



        public Question(QuestionType questionType, long level) {
            questionType.initialize(this, level);
        }
    }
}

