using Microsoft.VisualStudio.TestTools.UnitTesting;
using RPGame;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPGame.Tests.Additio {
    [TestClass()]
    public class QuestionTests {

        [TestMethod()]
        public void AdditionTest() {
            int level = 6;
            Addition question = new Addition(level);
            int addi1 = 2, addi2 = 5;


            question.operands[0] = addi1;
            question.operands[1] = addi2;
            
            Assert.IsTrue(question.validateAnswer(addi1+addi2));

        }
    }
}