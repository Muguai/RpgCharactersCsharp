using Equipment;
using Hero;
using Xunit;

using System.Reflection;

namespace Tests
{
    public class HeroStatsTest
    {
        [Fact]
        public void TotalStats_StatsAfterNoEquip_5_2_1()
        {
            // Arrange
            HeroStats expectedStats = new HeroStats(5,2,1);
            Barbarian barb = new Barbarian("Cohen");
            
            // Act & Asset
            Assert.Equal(barb.TotalStats().ToString(), expectedStats.ToString());
        }
        [Fact]
        public void Equip_AttributesCorrectAfterEquip_6_3_2()
        {
            // Arrange
            HeroStats expectedStats = new HeroStats(6,3,2);
            Barbarian barb = new Barbarian("Cohen");
            HeroStats stat = new HeroStats(1, 1, 1);
            var armor = new Armor(ArmorType.Mail, Slot.Head, stat, "DumbArmor", 1);
            // Act
            barb.Equip(armor);
            // Asset
            Assert.Equal(barb.TotalStats().ToString(), expectedStats.ToString());
        }
        [Fact]
        public void Equip_AttributesCorrectAfterTwoEquip_7_4_3()
        {
            // Arrange
            HeroStats expectedStats = new HeroStats(7,4,3);
            Barbarian barb = new Barbarian("Cohen");
            HeroStats stat = new HeroStats(1, 1, 1);
            var armor = new Armor(ArmorType.Mail, Slot.Head, stat, "DumbArmor", 1);
            var armor2 = new Armor(ArmorType.Mail, Slot.Body, stat, "DumbArmor2", 1);

            // Act
            barb.Equip(armor);
            barb.Equip(armor2);
            // Asset
            Assert.Equal(barb.TotalStats().ToString(), expectedStats.ToString());
        }

        [Fact]
        public void Equip_AttributesCorrectAfterReEquip_8_5_4()
        {
            // Arrange
            HeroStats expectedStats = new HeroStats(8,5,4);
            Barbarian barb = new Barbarian("Cohen");
            HeroStats stat = new HeroStats(1, 1, 1);
            HeroStats stat2 = new HeroStats(2, 2, 2);

            var armor = new Armor(ArmorType.Mail, Slot.Head, stat, "DumbArmor", 1);
            var armor2 = new Armor(ArmorType.Mail, Slot.Body, stat, "DumbArmor2", 1);
            var armor3 = new Armor(ArmorType.Mail, Slot.Body, stat2, "DumbArmor3", 1);

            // Act
            barb.Equip(armor);
            barb.Equip(armor2);
            barb.Equip(armor3);
            // Asset
            Assert.Equal(barb.TotalStats().ToString(), expectedStats.ToString());
        }

    }
}