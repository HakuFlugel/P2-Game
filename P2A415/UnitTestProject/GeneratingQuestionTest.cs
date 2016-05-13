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
        public void QuestionGenerationTest() {
            int level = 2;
            Question question = Question.selectQuestion(level);

            Assert.AreEqual(question.text, "Add the following numbers");

        }
    }
}