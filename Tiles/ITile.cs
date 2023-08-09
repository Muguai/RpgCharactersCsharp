namespace Tiles;
using Spectre.Console;
using Utils;
using Newtonsoft.Json.Linq;
using Combat;
using Equipment;
using Hero;
public abstract class Tile
{
    protected HeroClass currentHero;
    protected bool enteredBefore = false;

    public async virtual Task Enter(HeroClass hero)
    {
        currentHero = hero;
        await Task.Delay(10);
    }

    protected virtual string GetJsonDialogPath()
    {
        string tileName = GetType().Name;
        string jsonFileName = tileName + ".json";

        string jsonFilePath = Path.Combine(
            Directory.GetCurrentDirectory(),
            "Tiles",
            "Dialog",
            jsonFileName
        );
        return jsonFilePath;
    }

    public virtual async Task EnterFromJson(HeroClass hero)
    {
        string jsonFilePath = GetJsonDialogPath();
        string jsonContent = File.ReadAllText(jsonFilePath);
        JObject dialogData = JObject.Parse(jsonContent);

        if (enteredBefore)
        {
            await DisplayDialogData(dialogData, "altEnter");
            return;
        }

        bool repeatEnter = (bool)dialogData["repeatEnter"]!;

        if(!repeatEnter)
            enteredBefore = true;

        await DisplayDialogData(dialogData, "enter");

        bool startCombat = (bool)dialogData["startCombat"]!;
        bool startSkillCheck = (bool)dialogData["startSkillCheck"]!;

        if (startCombat)
        {
            JObject enemyData = dialogData["enemy"]!.ToObject<JObject>()!;
            await StartCombat(enemyData, hero);
        }
        else if (startSkillCheck)
        {
            JObject skillCheckData = dialogData["skillCheck"]!.ToObject<JObject>()!;
            await StartSkillCheck(skillCheckData, hero);
        }
    }

    protected async virtual Task StartSkillCheck(JObject skillCheckData, HeroClass hero)
    {

        await DisplayDialogData(skillCheckData, "BeforeDialog");


        JArray dropsData = (JArray)skillCheckData["drops"]!;
        List<Item> drops = ConstructDrops(dropsData);

        int goal = skillCheckData.Value<int>("goal");
        string statToCheck = skillCheckData.Value<string>("statToCheck")!;

        bool success = SkillCheck.StartSkillCheck(hero, statToCheck, goal);

        if (success)
        {
            await DisplayDialogData(skillCheckData, "SuccessDialog");
            if (drops.Count > 0)
            {
                AnsiConsole.Write(
                new FigletText("You got:")
                .Justify(Justify.Center)
                .Color(Color.Aqua));
                foreach (Item i in drops)
                {
                    if (i.GetType() == typeof(Misc))
                    {
                        Misc itemMisc = (Misc)i;
                        string amount = " " + itemMisc.Amount + "X";
                        AnsiConsole.Write(
                            new FigletText(i.ItemName + amount)
                            .Justify(Justify.Center)
                            .Color(Color.Aqua));
                        hero.AddToInventory(itemMisc);
                        continue;
                    }

                    AnsiConsole.Write(
                            new FigletText(i.ItemName )
                            .Justify(Justify.Center)
                            .Color(Color.Aqua));
                    if (i.GetType() == typeof(Armor))
                    {
                        Armor itemArmor = (Armor)i;
                        hero.AddToInventory(itemArmor);
                    }
                    else if (i.GetType() == typeof(Weapon))
                    {
                        Weapon itemWeapon = (Weapon)i;
                        hero.AddToInventory(itemWeapon);
                    }   
                }
            }
            ConsoleUtils.PressEnterToContinue();
        }
        else
        {
            int damage = skillCheckData.Value<int>("damage");
            await DisplayDialogData(skillCheckData, "FailDialog");
            await ConsoleUtils.DisplayTextSlowly("You take " + damage + " damage");
            hero.Health -= damage;
            if (hero.Health <= 0)
            {
                HeroUtils.GameOver();
            }
        }
        AnsiConsole.Clear();
    }

    private async Task DisplayDialogData(JObject dialogData, string dialogName)
    {
        JArray dialogJ = (JArray)dialogData[dialogName]!;
        string[] dialog = dialogJ.ToObject<string[]>()!;

        foreach (string s in dialog)
        {
            await ConsoleUtils.DisplayTextSlowly(s);
        }
    }

    protected async virtual Task StartCombat(JObject enemyData, HeroClass hero)
    {
        //Create enemy

        await DisplayDialogData(enemyData, "BeforeDialog");
        AnsiConsole.Clear();


        string monsterName = enemyData.Value<string>("monsterName")!;
        int diceAmount = enemyData.Value<int>("diceAmount")!;
        int additionalDmg = enemyData.Value<int>("additionalDmg");
        int health = enemyData.Value<int>("health");


        JArray dropsData = (JArray)enemyData["drops"]!;
        List<Item> drops = ConstructDrops(dropsData);

        Enemy enemy = new(monsterName, diceAmount, additionalDmg, health, drops.ToArray());
        Combat combat = new();
        combat.StartCombat(hero, enemy);

        await DisplayDialogData(enemyData, "KillDialog");

        AnsiConsole.Clear();
    }

    private List<Item> ConstructDrops(JArray dropsData)
    {
        List<Item> drops = new();

        foreach (JToken dropData in dropsData)
        {
            string itemType = dropData.Value<string>("type")!;
            switch (itemType)
            {
                case "Weapon":
                    WeaponType weaponType = Enum.Parse<WeaponType>(dropData.Value<string>("weaponType")!);
                    int amountOfDice = dropData.Value<int>("amountOfDice");
                    int weaponDamage = dropData.Value<int>("weaponDamage");
                    int requiredWeaponLevel = dropData.Value<int>("requiredLevel");
                    string weaponName = dropData.Value<string>("name")!;
                    Weapon weapon = new(weaponType, amountOfDice, weaponDamage, weaponName, requiredWeaponLevel);
                    drops.Add(weapon);
                    break;
                case "Armor":
                    ArmorType armorType = Enum.Parse<ArmorType>(dropData.Value<string>("armorType")!);
                    Slot bodyType = Enum.Parse<Slot>(dropData.Value<string>("slot")!);

                    JArray statsArray = dropData.Value<JArray>("stats")!;
                    int[] stats = statsArray.ToObject<int[]>()!;
                    HeroStats armorStats = new(stats[0], stats[1], stats[2]);

                    string armorName = dropData.Value<string>("name")!;
                    int requiredArmorLevel = dropData.Value<int>("requiredLevel");
                    Armor armor = new(armorType, bodyType, armorStats, armorName, requiredArmorLevel);
                    drops.Add(armor);
                    break;
                case "Misc":
                    MiscType miscType = Enum.Parse<MiscType>(dropData.Value<string>("miscType")!);
                    int amount = dropData.Value<int>("amount");
                    string miscName = dropData.Value<string>("name")!;
                    Misc misc = new(miscName, miscType, amount);
                    drops.Add(misc);
                    break;
                default:
                    break;
            }
        }

        return drops;

    }
    public virtual List<string> Options()
    {
        return new List<string> { "" };
    }
    public async virtual Task Exit()
    {
        string jsonFilePath = GetJsonDialogPath();
        string jsonContent = File.ReadAllText(jsonFilePath);
        JObject dialogData = JObject.Parse(jsonContent);

        await DisplayDialogData(dialogData, "exit");
    }
    public virtual void ChooseOptions(string chosenOption)
    {

    }
}