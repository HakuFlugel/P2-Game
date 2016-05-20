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
            monster.stats.curHP *= 500;
            
            Player player = new Player(region);
            player.character.stats.curHP *= 500;


            Combat combat = new Combat(new Game(), player.character, monster);

            double playerHp = player.character.stats.curHP;
            combat.update(100.0);

            double monsterHp = monster.stats.curHP;
            combat.doAttack();

            //Player test hp change
            Assert.IsTrue(player.character.stats.curHP < playerHp);

            //Monster test hp change
            Assert.IsTrue(monster.stats.curHP < monsterHp);

        }
    }
}