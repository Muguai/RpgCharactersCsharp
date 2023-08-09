using Equipment;
using Hero;
using Xunit;
using System.Reflection;

namespace Tests
{
    public class HeroTests
    {
        [Theory]
        [InlineData(typeof(Barbarian), "Cohen")]
        [InlineData(typeof(Wizard), "RinceWind")]
        [InlineData(typeof(SwashBuckler), "Nobby")]
        [InlineData(typeof(Archer), "Sergeant Colon")]
        public void HeroName_HeroGetsCorrectNameAtCreation(Type heroType, string expectedName)
        {
            // Arrange
            HeroClass? hero = Activator.CreateInstance(heroType, expectedName) as HeroClass;

            // Act
            var type = heroType;
            var field = type.GetField("heroName", BindingFlags.NonPublic | BindingFlags.Instance);

            if (field != null)
            {
                var actualValue = field.GetValue(hero) as string;

                // Assert
                Assert.Equal(expectedName, actualValue);
            }
            else
            {
                Assert.Fail($"The 'heroName' field was not found or is null.");
            }
        }

        [Theory]
        [InlineData(typeof(Barbarian), 1)]
        [InlineData(typeof(Wizard), 1)]
        [InlineData(typeof(SwashBuckler), 1)]
        [InlineData(typeof(Archer), 1)]
        public void HeroLevel_HeroGetsCorrectLevelAtCreation(Type heroType, int expectedLevel)
        {
            // Arrange
            HeroClass? hero = Activator.CreateInstance(heroType, "Vetenari") as HeroClass;

            // Act
            var type = heroType;
            var field = type.GetField("level", BindingFlags.NonPublic | BindingFlags.Instance);

            if (field != null)
            {
                var actualValue = field.GetValue(hero) as int?;

                // Assert
                Assert.Equal(expectedLevel, actualValue);
            }
            else
            {
                Assert.Fail($"The 'level' field was not found or is null.");
            }
        }

        [Theory]
        [InlineData(typeof(Barbarian), 5, 2, 1)]
        [InlineData(typeof(Wizard), 1, 1, 8)]
        [InlineData(typeof(SwashBuckler), 2, 6, 1)]
        [InlineData(typeof(Archer), 1, 7, 1)]
        public void HeroStats_HeroGetsCorrectStatsAtCreation(Type heroType, int expectedStrength, int expectedDexterity, int expectedIntelligence)
        {
            // Arrange
            HeroClass? hero = Activator.CreateInstance(heroType, "Vetenari") as HeroClass;

            // Act
            var type = heroType;
            var field = type.GetField("heroStats", BindingFlags.NonPublic | BindingFlags.Instance);

            if (field != null)
            {
                var actualValue = field.GetValue(hero) as HeroStats;

                // Assert
                Assert.Equal(new HeroStats(expectedStrength, expectedDexterity, expectedIntelligence).ToString(), actualValue.ToString());
            }
            else
            {
                Assert.Fail($"The 'heroStats' field was not found or is null.");
            }
        }


        [Theory]
        [InlineData(typeof(Wizard), "Ridcully", 2, 2, 13)]
        [InlineData(typeof(Barbarian), "Cohen", 8, 4, 2)]
        [InlineData(typeof(SwashBuckler), "Nobby", 3, 10, 2)]
        [InlineData(typeof(Archer), "Sergeant Colon", 2, 12, 2)]
        public void HeroLevelUp_StatsGetIncreasedAtLevelUp(
        Type heroType, string heroName, int expectedStrength, int expectedDexterity, int expectedIntelligence)
        {
            // Arrange
            HeroClass? hero = Activator.CreateInstance(heroType, heroName) as HeroClass;
            HeroStats expectedStats = new HeroStats(expectedStrength, expectedDexterity, expectedIntelligence);

            // Act
            if (!(hero is null))
                hero.LevelUp();

            var type = heroType;
            var statField = type.GetField("heroStats", BindingFlags.NonPublic | BindingFlags.Instance);

            if (statField != null)
            {
                var actualStats = statField.GetValue(hero) as HeroStats;

                // Assert
                Assert.Equal(expectedStats.ToString(), actualStats.ToString());
            }
            else
            {
                Assert.Fail("The 'heroStats' field was not found or is null.");
            }
        }

        [Theory]
        [InlineData(typeof(Wizard), "Ridcully")]
        [InlineData(typeof(Barbarian), "Cohen")]
        [InlineData(typeof(SwashBuckler), "Nobby")]
        [InlineData(typeof(Archer), "Sergeant Colon")]
        public void HeroLevelUp_LevelGetsIncreasedAfterLevelUp(Type heroType, string heroName)
        {
            // Arrange
            HeroClass? hero = Activator.CreateInstance(heroType, heroName) as HeroClass;
            int expectedLevel = 2;

            // Act
            if (!(hero is null))
                hero.LevelUp();
            var type = heroType;
            var levelField = type.GetField("level", BindingFlags.NonPublic | BindingFlags.Instance);

            if (levelField != null)
            {
                var actualLevel = levelField.GetValue(hero) as int?;

                // Assert
                Assert.Equal(expectedLevel, actualLevel);
            }
            else
            {
                Assert.Fail("The 'level' field was not found or is null.");
            }
        }
    }
}