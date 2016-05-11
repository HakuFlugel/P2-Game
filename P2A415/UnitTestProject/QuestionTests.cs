using Microsoft.VisualStudio.TestTools.UnitTesting;
using RPGame;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPGame.Tests {
    [TestClass()]
    public class QuestionTests {

        [TestMethod()]
        public void QustionGen() {
            int level = 20;
            Question question = Question.selectQuestion(level);


        }

    }
}