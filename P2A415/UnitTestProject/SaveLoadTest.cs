using Microsoft.VisualStudio.TestTools.UnitTesting;
using RPGame;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPGame.Tests.Saveload {
    [TestClass()]
    public class SaveLoadTest {

        [TestMethod()]
        public void SaveLoad() {
            World world = new World(new Game());

            Player player = new Player();
            Player playerLoad;
            player.statistics.encounters = 5003;
            player.statistics.deaths = 5;
            player.character.stats.level = 300;
            player.character.calculateStats();
            world.save(player);
            world.load(out playerLoad);

            Assert.AreEqual(player.character.stats.level,playerLoad.character.stats.level);
            Assert.AreEqual(player.character.stats.curHP, playerLoad.character.stats.curHP);

            Assert.AreEqual(player.statistics.deaths,playerLoad.statistics.deaths);
            Assert.AreEqual(player.statistics.encounters, playerLoad.statistics.encounters);



        }
    }
}