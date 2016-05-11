using Microsoft.VisualStudio.TestTools.UnitTesting;
using RPGame;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPGame.Tests {
    [TestClass()]
    public class CombatTests {
        [TestMethod()]
        public void CombatTest() {
            Region region = new Region(10, 10);

            Character monster = new Character(region,2,0,0,10);
            
            Player player = new Player(region);

            Combat combat = new Combat(new Game(), player.character, monster);

            double playerHp = player.character.stats.curHP;
            combat.update(100.0);

            double monsterHp = monster.stats.curHP;
            combat.doAttack();

            //Player test hp change
            Assert.AreNotEqual(player.character.stats.curHP, playerHp);
            Assert.IsTrue(playerHp > player.character.stats.curHP);

            //Monster test hp change
            Assert.AreNotEqual(monster.stats.curHP, monsterHp);
            Assert.IsTrue(monsterHp > monster.stats.curHP);

        }
    }
}