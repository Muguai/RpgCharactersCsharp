using Equipment;
using Hero;
using Xunit;
using System.Reflection;

namespace Tests
{
    public class WeaponTests
    {
        /*
        [Fact]
        public void WeaponCreation_WeaponGetsCorrectInitValuesAtCreation_test1WeaponDagger4()
        {
            // Arrange
            string expectedName = "test";
            int expectedRequiredLevel = 1;
            Slot expectedItemSlot = Slot.Weapon;
            WeaponType expectedWeaponType = WeaponType.Dagger;
            int expectedDmg = 4;
            Weapon testWeapon = new Weapon(WeaponType.Dagger, 4, "test", 1);

            // Act
            var actualName = testWeapon.ItemName;            
            var actualRequiredLevel = testWeapon.RequiredLevel;
            var actualItemSlot = testWeapon.ItemSlot;
            var actualWeaponType = testWeapon.WeaponType;
            var actualWeaponDmg = testWeapon.WeaponDamage;
            //Assert
            Assert.Equal((expectedName + expectedRequiredLevel + expectedItemSlot.ToString() + expectedWeaponType.ToString() + expectedDmg)
            , (actualName + actualRequiredLevel + actualItemSlot.ToString() + actualWeaponType.ToString() + actualWeaponDmg)
            );
        }
        [Fact]
        public void Equip_TestEquipingInvalidWeapon_InvalidWeaponException()
        {
            // Arrange
            Wizard wiz = new Wizard("Rincewind");
            Weapon weapon = new Weapon(WeaponType.Hatchet, 4, "Bad Hatchet", 1);

            // Act & Assert
            Assert.Throws<InvalidWeaponException>(() => wiz.Equip(weapon));

        }

        [Fact]
        public void Equip_EquippingAWeaponToAHero_NotNull()
        {
            // Arrange
            SwashBuckler swash = new SwashBuckler("Patchy");
            Weapon weapon = new Weapon(WeaponType.Dagger, 4, "Bad Dagger", 1);

            // Act
            swash.Equip(weapon);
            //Assert
            var type = typeof(HeroClass);
            var equipmentField = type.GetField("equipment", BindingFlags.NonPublic | BindingFlags.Instance);

            if (equipmentField != null)
            {
                var equipDict = equipmentField.GetValue(swash) as Dictionary<Slot,Item>;
                Assert.NotNull(equipDict?[Slot.Weapon]);
            }
            else
            {
                Assert.Fail("A Field was not found or is null.");
            };
        }
        */
    }
}