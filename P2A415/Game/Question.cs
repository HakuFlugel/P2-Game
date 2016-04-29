using System;
using System.Collections.Generic;
using System.Linq;

namespace RPGame {


    public abstract class Question {

        public static Random rand = new Random();
        public static int requiredLevel = 0;

        public static List<Tuple<Type, int>> questionTypes = new List<Tuple<Type, int>>();
        static Question() {
            questionTypes.Add (new Tuple<Type, int> (typeof(Addition), Addition.requiredLevel));
            questionTypes.Add (new Tuple<Type, int> (typeof(Subtraction), Subtraction.requiredLevel));
        }

        public static Question selectQuestion(int level) {

            Console.WriteLine("Debug: questionTypes");
            foreach (var item in questionTypes) {
                Console.WriteLine(item.Item1 + " " + item.Item2);

            }
            Console.WriteLine("---");

            var possibleTypes = questionTypes.Where(tuple => tuple.Item2 <= level).Select(tuple => tuple.Item1);
            Type questionType = possibleTypes.ElementAt(rand.Next(possibleTypes.Count()));

            return (Question)Activator.CreateInstance(questionType, level);
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