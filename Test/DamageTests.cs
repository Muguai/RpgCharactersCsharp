using Equipment;
using Hero;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using System.Reflection;

namespace Tests
{
    [TestClass]
    public class DamageTests
    {
        [TestMethod]
        public void Damage_DamageWithNoWeapon_0Dmg()
        {
            // Arrange
            SwashBuckler swash = new SwashBuckler("Patchy");
            int expectedDmg = 0;
            
            // Act
            int actualDmg = swash.Damage();
            //Assert
            Assert.AreEqual(expectedDmg, actualDmg);
        }

        [TestMethod]
        public void Damage_DamageWithEquippedWeapon_SomeDmg()
        {
            // Arrange
            SwashBuckler swash = new SwashBuckler("Patchy");
            Weapon weapon = new Weapon(WeaponType.Dagger, 4, "Bad Dagger", 1);
            int expectedDmg = 4 + (1 + swash.TotalStats().getSum("dex") / 100);
            
            // Act
            swash.Equip(weapon);
            int actualDmg = swash.Damage();
            //Assert
            Assert.AreEqual(expectedDmg, actualDmg);
        }

        [TestMethod]
        public void Damage_DamageWithReEquippedWeapon_SomeDmg()
        {
            // Arrange
            SwashBuckler swash = new SwashBuckler("Patchy");
            Weapon weapon = new Weapon(WeaponType.Dagger, 4, "Bad Dagger", 1);
            Weapon weapon2 = new Weapon(WeaponType.Dagger, 5, "Bad Dagger", 1);

            int expectedDmg = 5 + (1 + swash.TotalStats().getSum("dex") / 100);
            
            // Act
            swash.Equip(weapon2);
            int actualDmg = swash.Damage();
            //Assert
            Assert.AreEqual(expectedDmg, actualDmg);
        }

         [TestMethod]
        public void Damage_DamageWithWeaponAndArmor_SomeDmg()
        {
            // Arrange
            SwashBuckler swash = new SwashBuckler("Patchy");
            Weapon weapon = new Weapon(WeaponType.Dagger, 4, "Bad Dagger", 1);
            HeroStats stat = new HeroStats(10,10,10);
            Armor armor = new Armor(ArmorType.Leather, Slot.Head, stat, "DumbArmor", 1);
            swash.Equip(armor);
            int expectedDmg = 4 + (1 + swash.TotalStats().getSum("dex") / 100);
            
            // Act
            swash.Equip(weapon);
            int actualDmg = swash.Damage();
            //Assert
            Assert.AreEqual(expectedDmg, actualDmg);
        }
    }
}