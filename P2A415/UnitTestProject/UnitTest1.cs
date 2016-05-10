using RPGame;
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace RPGame.Tests {
    [TestClass()]
    public class UnitTest1 {
        [TestMethod()]
        public void MakeItemTest() {
            bool valid = false;

            Items item = new Items();

            item.MakeItem(new Items(), 50);

            int length = Items.itemList.Count;

            for (int i = 0; i < length; i++) {

                if (Items.itemList[i].itemName ==) {
                    
                }
            }


            if (!valid) {
                Assert.Fail();
            }   
        }   


        public void CompairItems() {

        }
    }
}



namespace UnitTestProject {
    [TestClass]
    public class UnitTest1 {
        [TestMethod]
        public void TestMethod1() {
        }
    }
}
