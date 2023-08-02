using Equipment;
using Hero;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using System.Reflection;

namespace Tests
{
    [TestClass]
    public class HeroTests
    {
        [TestMethod]
        public void HeroCreation_HeroGetsCorrectInitValuesAtCreation_5_2_1Cohen1()
        {
            // Arrange
            HeroStats expectedStats = new HeroStats(5, 2, 1);
            string expectedName = "Cohen";
            int expectedLevel = 1;
            Barbarian barbarian = new Barbarian(expectedName);

            // Act
            var type = typeof(HeroClass);
            var levelField = type.GetField("level", BindingFlags.NonPublic | BindingFlags.Instance);
            var nameField = type.GetField("heroName", BindingFlags.NonPublic | BindingFlags.Instance);
            var statField = type.GetField("heroStats", BindingFlags.NonPublic | BindingFlags.Instance);

            if (nameField != null && levelField != null && statField != null)
            {
                var actualName = nameField.GetValue(barbarian) as string;
                var actualLevel = levelField.GetValue(barbarian) as int?;
                var actualStats = statField.GetValue(barbarian) as HeroStats;

                if (actualStats != null)
                {
                    // Assert
                    Assert.AreEqual((expectedStats.ToString() + expectedName + expectedLevel), (actualStats.ToString() + actualName + actualLevel));
                }
            }
            else
            {
                Assert.Fail("A Field was not found or is null.");
            };
        }
        [TestMethod]
        public void LevelUp_LevelAndStatsGetsIncreasedAtLevelUp_2_2_13_2()
        {
            // Arrange
            Wizard wizard = new Wizard("Ridcully");
            HeroStats expectedStats = new HeroStats(2, 2, 13);
            int expectedLevel = 2;
            // Act
            wizard.LevelUp();
            var type = typeof(HeroClass);
            var statField = type.GetField("heroStats", BindingFlags.NonPublic | BindingFlags.Instance);
            var levelField = type.GetField("level", BindingFlags.NonPublic | BindingFlags.Instance);
            if (levelField != null && statField != null)
            {
                var actualStats = statField.GetValue(wizard) as HeroStats;
                var actualLevel = levelField.GetValue(wizard) as int?;
                if (actualStats != null)
                {
                    // Assert
                    Assert.AreEqual(expectedStats.ToString() + expectedLevel, actualStats.ToString() + actualLevel);
                }
            }
            else
            {
                Assert.Fail("A field was not found or is null.");
            };
        }
    }
}