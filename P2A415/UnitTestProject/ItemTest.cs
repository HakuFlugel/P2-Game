using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace RPGame.Tests {
    [TestClass()]
    public class UnitTest1 {
        [TestMethod()]
        public void ItemTest() {

            for (int level = -50; level < 550; level += 10) {

                for (int i = 0; i < Item.itemTypes.Count; i++) {
                    Item item = new Item(Item.itemTypes[i], level);
                    Assert.AreEqual(item.itemHP, (Item.itemTypes[i].itemHP * Math.Pow(1.20, level)));

                    Assert.AreEqual(item.itemDMG, (Item.itemTypes[i].itemDMG * Math.Pow(1.20, level)));

                    Assert.AreEqual(item.itemDEF, (Item.itemTypes[i].itemDEF * Math.Pow(1.17, level)));

                    Assert.AreEqual(item.itemPENE, (Item.itemTypes[i].itemPENE * Math.Pow(1.17, level)));

                    Assert.AreEqual(item.itemLVL, level);
                }
            }
        }
        
    }
}
