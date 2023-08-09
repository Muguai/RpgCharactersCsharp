namespace Combat;
using Hero;
using Spectre.Console;
using Utils;
//fuck it this gets to be in combat too
public static class SkillCheck
{
    public static bool StartSkillCheck(HeroClass hero, string skillToCheck, int goal)
    {
        int skillCheckAmount = 5;
        AnsiConsole.Write(
            new FigletText(skillToCheck + " Skill Check - " + goal)
            .Justify(Justify.Center)
            .Color(Color.Red));

        AnsiConsole.WriteLine(ConsoleUtils.PadCenterText("roll 1d6 + 1d6 for each " + skillCheckAmount + " " + skillToCheck));
        
        ConsoleUtils.EmptyPressEnterToContinue();

        double calculate = hero.SpecificStat(skillToCheck) / skillCheckAmount;
        int result = Dice.RollDice(1 + (int)Math.Floor(calculate));

        AnsiConsole.Write(
               new FigletText("Your rolled " + result + " VS " + goal)
               .Justify(Justify.Center)
               .Color(Color.Red));

        ConsoleUtils.EmptyPressEnterToContinue();

        if (result < goal)
        {
            return false;
        }
        else
        {
            return true;
        }

    }
}