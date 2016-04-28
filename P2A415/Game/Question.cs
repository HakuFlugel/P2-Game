﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace WinFormsTest {


    public abstract class Question {

        public static Random rand = new Random();

        public static List<Tuple<Type, int>> questionTypes = new List<Tuple<Type, int>>();
        static Question() {
            questionTypes.Add (new Tuple<Type, int> (typeof(Addition), 0));     // the number is a level requrement for the qustions type
            questionTypes.Add (new Tuple<Type, int> (typeof(Subtraction), 5));
            questionTypes.Add(new Tuple<Type, int>(typeof(Division), 15));
            questionTypes.Add(new Tuple<Type, int>(typeof(Multiplication), 10));
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