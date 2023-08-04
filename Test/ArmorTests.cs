using Equipment;
using Hero;
using Xunit;
using System.Reflection;

namespace Tests
{
    public class ArmorTests
    {
        [Fact]
        public void ArmorCreation_ArmorGetsCorrectInitValuesAtCreation_test1HeadCloth1_1_1()
        {
            // Arrange
            string expectedName = "test";
            int expectedRequiredLevel = 1;
            Slot expectedItemSlot = Slot.Head;
            ArmorType expectedArmorType = ArmorType.Cloth;
            HeroStats expectedStats = new HeroStats(1, 1, 1);
            Armor testArmor = new Armor(ArmorType.Cloth, Slot.Head, new HeroStats(1, 1, 1), "test", 1);

            // Act

            var actualName = testArmor.ItemName;
            var actualRequiredLevel = testArmor.RequiredLevel;
            var actualItemSlot = testArmor.ItemSlot;
            var actualArmorType = testArmor.ArmorType;
            var actualStats = testArmor.ArmorStats;
            // Assert
            Assert.Equal((expectedName + expectedRequiredLevel + expectedItemSlot.ToString() + expectedArmorType.ToString() + expectedStats.ToString() )
            , (actualName + actualRequiredLevel + actualItemSlot.ToString() + actualArmorType.ToString() +  actualStats.ToString())
            );
        }
        [Fact]
        public void Equip_TestEquippingInvalidArmor_InvalidArmorException()
        {
            // Arrange
            Barbarian barb = new Barbarian("Carl");
            HeroStats stat = new HeroStats(1, 1, 1);
            var armor = new Armor(ArmorType.Cloth, Slot.Head, stat, "DumbArmor", 1);

            // Act & Asset
            Assert.Throws<InvalidArmorException>(() => barb.Equip(armor));

        }

        [Fact]
        public void Equip_EquippingAArmorToAHero_NotNull()
        {
            // Arrange
            SwashBuckler swash = new SwashBuckler("Patchy");
            Armor armor = new Armor(ArmorType.Leather, Slot.Body, new HeroStats(1,1,1), "Bad armor", 1);

            // Act
            swash.Equip(armor);
            //Assert
            var type = typeof(HeroClass);
            var equipmentField = type.GetField("equipment", BindingFlags.NonPublic | BindingFlags.Instance);

            if (equipmentField != null)
            {
                var equipDict = equipmentField.GetValue(swash) as Dictionary<Slot,Item>;
                Assert.NotNull(equipDict?[Slot.Body]);
            }
            else
            {
                Assert.Fail("A Field was not found or is null.");
            };
        }
    }
}