using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace RPGame.InventoryTest.Tests {
    [TestClass()]
    public class InventoryTest {
        [TestMethod()]
        public void CharacterInvetoryTest() {

            Player player = new Player(new Region(16, 32));
            int x = player.inventory.inventory[0].GetLength(1);
            int y = player.inventory.inventory[0].GetLength(0);

            for (int i = 0; i < player.inventory.inventory[0].Length; i++) {
                Item item = new Item(Item.itemTypes[i%13], 1);
                player.inventory.addItem(item);

                Assert.ReferenceEquals(player.inventory.inventory[0][i/x,i%x], item);
            }
        }
    }
}