using Hero;
using Equipment;
using Spectre.Console;
using Utils;
public class Combat{

    HeroClass heroInBattle;
    Enemy enemyInBattle;

    bool isPlayerDead = false;
    bool isEnemyDead = false;

    private List<string> actions = new List<string>(){
        "Attack",
        "Run"
    };
    public bool StartCombat(HeroClass hero, Enemy enemy){
        heroInBattle = hero;
        enemyInBattle = enemy;

        AnsiConsole.Write(
        new FigletText("Your Attacked by " + enemy.MonsterName)
            .LeftJustified()
            .Color(Color.Aqua));
        CombatLoop();
        EndCombat();
        return isPlayerDead;
    }

    private void EndCombat(){
        if(enemyInBattle.Drops.Length > 0){
            AnsiConsole.Write(
                new FigletText(enemyInBattle.MonsterName + " Dropped:")
                .LeftJustified()
                .Color(Color.Aqua));
            foreach(Item i in enemyInBattle.Drops){
                string amount = "";
                if(i.GetType() == typeof(Misc)){
                    Misc itemMisc = (Misc)i;
                    amount =  " X" + itemMisc.Amount ;
                }
                AnsiConsole.Write(
                    new FigletText(i.ItemName + amount)
                    .LeftJustified()
                    .Color(Color.Aqua));
                heroInBattle.AddToInventory(i);
            }
            
        }
    }

    private void CombatLoop(){
        //Give Player Attack/Run OPtion

        var combatOption = AnsiConsole.Prompt(
          new SelectionPrompt<string>()
          .Title("Actions:")
          .PageSize(10)
          .AddChoices(actions));

        bool shouldRun = false;
        //Do players choice
        switch(combatOption){
            case "Attack":
                PlayerAttack();
                if(CheckEndOfCombat())
                    return;
                break;
            case "Run":
                shouldRun = Run();
                break;
        }

        // optional Escape if run is succesful
     
        if(shouldRun)
            return;
        
        
        //Enemy attacks
        DamagePlayer();

        //End combat if enemy or player is dead otherwise continue;
        
        if(CheckEndOfCombat())
            return;
        
        
        CombatLoop();
    }

    private void PlayerAttack(){

        AnsiConsole.Write(
            new FigletText("You Attack")
                .LeftJustified()
                .Color(Color.Aqua));

        int result = Dice.RollDice(heroInBattle.GetAmountOfDice());

        AnsiConsole.Write(
            new FigletText("+")
                .LeftJustified()
                .Color(Color.Aqua));
         AnsiConsole.Write(
            new FigletText(heroInBattle.Damage().ToString())
                .LeftJustified()
                .Color(Color.Aqua));

         AnsiConsole.Write(
            new FigletText(result + " + " + heroInBattle.Damage() + " = " + (result + heroInBattle.Damage()))
                .LeftJustified()
                .Color(Color.Aqua));
        enemyInBattle.Health -= result + heroInBattle.Damage();

        BreakdownChart breakChart = new BreakdownChart()
            .Width(150)
            .AddItem("Enemy Health",  Math.Max(enemyInBattle.Health, 0), Color.Red)
            .AddItem("Enemys Lost Health",  enemyInBattle.MaxHealth - Math.Max(enemyInBattle.Health, 0), Color.DarkRed);

        AnsiConsole.Write(breakChart);

        if(enemyInBattle.Health  < 1){
            AnsiConsole.Write(
            new FigletText("You defeated " + enemyInBattle.MonsterName)
                .LeftJustified()
                .Color(Color.Aqua));
            isEnemyDead = true;
        }

        ConsoleUtils.EmptyPressEnterToContinue();

    }

    private bool CheckEndOfCombat(){
        return isPlayerDead || isEnemyDead;
    }

    private void DamagePlayer(){
        AnsiConsole.Write(
            new FigletText("Monster Attack")
                .LeftJustified()
                .Color(Color.Red));

        int result = Dice.RollDice(enemyInBattle.DiceAmount);

        AnsiConsole.Write(
            new FigletText("+")
                .LeftJustified()
                .Color(Color.Red));
         AnsiConsole.Write(
            new FigletText(enemyInBattle.AdditionalDmg.ToString())
                .LeftJustified()
                .Color(Color.Red));

         AnsiConsole.Write(
            new FigletText(result + " + " + enemyInBattle.AdditionalDmg + " = " + (result + enemyInBattle.AdditionalDmg))
                .LeftJustified()
                .Color(Color.Red));
        heroInBattle.Health -= result + enemyInBattle.AdditionalDmg;

        BreakdownChart breakChart = new BreakdownChart()
            .Width(150)
            .AddItem("Your Health",  Math.Max(heroInBattle.Health, 0), Color.Red)
            .AddItem("Your Lost Health",  heroInBattle.MaxHealth - Math.Max(heroInBattle.Health, 0), Color.DarkRed);

        AnsiConsole.Write(breakChart);

        if(heroInBattle.Health  < 1){
            AnsiConsole.Write(
            new FigletText("You got defeated by " + enemyInBattle.MonsterName)
                .LeftJustified()
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
            .LeftJustified()
            .Color(Color.Aqua));
        ConsoleUtils.EmptyPressEnterToContinue();
        return true;
    }
    else
    {
        AnsiConsole.Write(
            new FigletText("You Failed to Run Away")
            .LeftJustified()
            .Color(Color.Red));
        ConsoleUtils.EmptyPressEnterToContinue();
        return false;
    }
}
}