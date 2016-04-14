using System;
using System.Collections.Generic;
using System.Linq;

namespace WinFormsTest {


    public abstract class Question {

        public static Random rand = new Random();
        public static long requiredLevel = 0;

        public static List<Type> questionTypes = new List<Type>();
        static Question() {
            questionTypes.Add(typeof(Addition));
            questionTypes.Add(typeof(Subtraction));
        }

        public static Type selectQuestionType(int level) {
            while(true) {
                var possibleTypes = questionTypes.Where(qt => level >= (int)qt.GetProperty("requiredLevel").GetValue(null));
                return possibleTypes.ElementAt(rand.Next(possibleTypes.Count()));
            }
        }

        public int level = 0;
        //public int scale

        public int[] operands;

        public string text;
        public string expression;



        public Question(int level) {
            this.level = level;
        }

        public abstract bool validateAnswer(int answer);

    }
}