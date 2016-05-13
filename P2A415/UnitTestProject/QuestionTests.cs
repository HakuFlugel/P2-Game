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
            int level = 2;
            Question question = Question.selectQuestion(level);



            int length = question.operands.Length;
            var awswer = 0;

            for (var index = 0; index < length; index++) {
                awswer += question.operands[index];
            }

          
            Assert.IsTrue(question.validateAnswer(awswer));

        }
    }
}