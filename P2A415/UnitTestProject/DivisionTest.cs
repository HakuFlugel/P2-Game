﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using RPGame;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPGame.Tests.Div {
    [TestClass()]
    public class QuestionTests {

        [TestMethod()]
        public void DivisionTest() {
            int level = 0;
            Division question = new Division(level);
            int subb1 = 2, subb2 = 5;


            question.operands[0] = subb1;
            question.operands[1] = subb2;

            Assert.IsTrue(question.validateAnswer((subb1 * subb2) / subb2));

        }
    }
}