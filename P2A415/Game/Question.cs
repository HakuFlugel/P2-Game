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

        public static Question selectQuestion(int level) {
            foreach (var item in questionTypes) {
                Console.WriteLine(item);
            }
            var possibleTypes = questionTypes.Where(qt => level >= qt.GetField("requiredLevel").GetValue(qt));
            Type questionType = possibleTypes.ElementAt(rand.Next(possibleTypes.Count()));

            return (Question)Activator.CreateInstance(questionType);
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