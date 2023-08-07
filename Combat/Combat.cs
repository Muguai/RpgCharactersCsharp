using Hero;
using Equipment;
using Spectre.Console;
using Utils;
public class Combat
{

    HeroClass heroInBattle;
    Enemy enemyInBattle;

    bool isPlayerDead = false;
    bool isEnemyDead = false;
    bool shouldRun = false;


    private List<string> actions = new List<string>(){
        ConsoleUtils.PadCenterText("Attack"),
        ConsoleUtils.PadCenterText("Run")
    };
    public bool StartCombat(HeroClass hero, Enemy enemy)
    {
        heroInBattle = hero;
        enemyInBattle = enemy;

        AnsiConsole.Write(
        new FigletText("Your Attacked by " + enemy.MonsterName)
            .Justify(Justify.Center)
            .Color(Color.Aqua));
        CombatLoop();
        EndCombat();
        return isPlayerDead;
    }

    private void EndCombat()
    {

        if (enemyInBattle.Drops.Length > 0 && shouldRun != true)
        {
            AnsiConsole.Write(
                new FigletText(enemyInBattle.MonsterName + " Dropped:")
                .Justify(Justify.Center)
                .Color(Color.Aqua));
            foreach (Item i in enemyInBattle.Drops)
            {
                string amount = "";
                if (i.GetType() == typeof(Misc))
                {
                    Misc itemMisc = (Misc)i;
                    amount = " X" + itemMisc.Amount;
                }
                AnsiConsole.Write(
                    new FigletText(i.ItemName + amount)
                    .Justify(Justify.Center)
                    .Color(Color.Aqua));
                heroInBattle.AddToInventory(i);
            }

        }
    }

    private void CombatLoop()
    {
        AnsiConsole.Write(
        new FigletText("Actions")
            .Justify(Justify.Center)
            .Color(Color.Aqua));
        //Give Player Attack/Run OPtion
        var title = ConsoleUtils.PadCenterText("");
        var combatOption = AnsiConsole.Prompt(
          new SelectionPrompt<string>()
          .Title(title)
          .PageSize(10)
          .AddChoices(actions));

        

        //Do players choice
        if (combatOption == actions[0])
        {
            PlayerAttack();
            if (CheckEndOfCombat())
                return;
        }
        else if (combatOption == actions[1])
        {
            shouldRun = Run();
        }

        // optional Escape if run is succesful

        if (shouldRun)
            return;


        //Enemy attacks
        DamagePlayer();

        //End combat if enemy or player is dead otherwise continue;

        if (CheckEndOfCombat())
            return;

        AnsiConsole.Clear();
        CombatLoop();
    }

    private void PlayerAttack()
    {

        AnsiConsole.Write(
            new FigletText("You Attack")
                .Justify(Justify.Center)
                .Color(Color.Aqua));

        int result = Dice.RollDice(heroInBattle.GetAmountOfDice());

        AnsiConsole.Write(
            new FigletText("+")
                .Justify(Justify.Center)
                .Color(Color.Aqua));
        AnsiConsole.Write(
           new FigletText(heroInBattle.Damage().ToString())
                .Justify(Justify.Center)
               .Color(Color.Aqua));

        AnsiConsole.Write(
           new FigletText(result + " + " + heroInBattle.Damage() + " = " + (result + heroInBattle.Damage()))
                .Justify(Justify.Center)
               .Color(Color.Aqua));
        enemyInBattle.Health -= result + heroInBattle.Damage();

        BreakdownChart breakChart = new BreakdownChart()
            .Width(150)
            .HideTags()
            .AddItem("Enemy Health", Math.Max(enemyInBattle.Health, 0), Color.Red)
            .AddItem("Enemys Lost Health", enemyInBattle.MaxHealth - Math.Max(enemyInBattle.Health, 0), Color.DarkRed);

        AnsiConsole.WriteLine(" ");
        AnsiConsole.WriteLine(ConsoleUtils.PadCenterText("Enemy Health " + enemyInBattle.Health + "/" + enemyInBattle.MaxHealth ));
        AnsiConsole.WriteLine(" ");
    
        ConsoleUtils.AddEmptyStringPadding((int)breakChart.Width!);
        AnsiConsole.Write(breakChart);

        if (enemyInBattle.Health < 1)
        {
            
            AnsiConsole.Write(
            new FigletText("You defeated " + enemyInBattle.MonsterName)
                .Justify(Justify.Center)
                .Color(Color.Aqua));
            isEnemyDead = true;
        }

        ConsoleUtils.EmptyPressEnterToContinue();

    }

    private bool CheckEndOfCombat()
    {
        return isPlayerDead || isEnemyDead;
    }

    private void DamagePlayer()
    {
        AnsiConsole.Write(
            new FigletText("Monster Attack")
                .Justify(Justify.Center)
                .Color(Color.Red));

        int result = Dice.RollDice(enemyInBattle.DiceAmount);

        AnsiConsole.Write(
            new FigletText("+")
                .Justify(Justify.Center)
                .Color(Color.Red));
        AnsiConsole.Write(
           new FigletText(enemyInBattle.AdditionalDmg.ToString())
               .Justify(Justify.Center)
               .Color(Color.Red));

        AnsiConsole.Write(
           new FigletText(result + " + " + enemyInBattle.AdditionalDmg + " = " + (result + enemyInBattle.AdditionalDmg))
               .Justify(Justify.Center)
               .Color(Color.Red));
        heroInBattle.Health -= result + enemyInBattle.AdditionalDmg;

        BreakdownChart breakChart = new BreakdownChart()
            .Width(150)
            .HideTags()
            .AddItem("Your Health", Math.Max(heroInBattle.Health, 0), Color.Red)
            .AddItem("Your Lost Health", heroInBattle.MaxHealth - Math.Max(heroInBattle.Health, 0), Color.DarkRed);

        AnsiConsole.WriteLine(" ");
        AnsiConsole.WriteLine(ConsoleUtils.PadCenterText("Your Health " + heroInBattle.Health + "/" + heroInBattle.MaxHealth ));
        AnsiConsole.WriteLine(" ");
       
        ConsoleUtils.AddEmptyStringPadding((int)breakChart.Width!);
        AnsiConsole.Write(breakChart);

        if (heroInBattle.Health < 1)
        {
            AnsiConsole.Write(
            new FigletText("You got defeated by " + enemyInBattle.MonsterName)
                .Justify(Justify.Center)
                .Color(Color.Red));
            isPlayerDead = true;
        }

        ConsoleUtils.EmptyPressEnterToContinue();
    }

    private bool Run()
    {
        double dexModifier = heroInBattle.TotalStats().getSum("dex") * 0.1;

        double baseRunChance = 0.25 + dexModifier;

        double randomValue = new Random().NextDouble();

        if (randomValue < baseRunChance)
        {
            AnsiConsole.Write(
                new FigletText("You Successfully Ran Away")
                .Justify(Justify.Center)
                .Color(Color.Aqua));
            ConsoleUtils.EmptyPressEnterToContinue();
            return true;
        }
        else
        {
            AnsiConsole.Write(
                new FigletText("You Failed to Run Away")
                .Justify(Justify.Center)
                .Color(Color.Red));
            ConsoleUtils.EmptyPressEnterToContinue();
            return false;
        }
    }
}