namespace Tiles;
using Spectre.Console;
using Utils;
using Newtonsoft.Json.Linq;
using Combat;
using Equipment;
using Hero;
public abstract class Tile
{
    HeroClass currentHero;
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

        foreach (JToken chunkToken in dialogData["enter"]!)
        {
            string chunk = chunkToken.Value<string>()!;
            await ConsoleUtils.DisplayTextSlowly(chunk);
        }

        bool startCombat = (bool)dialogData["startCombat"]!;
        if (startCombat)
        {
            StartCombat(dialogData, hero);
        }
    }

    protected virtual void StartCombat(JObject dialogData, HeroClass hero)
    {
        //Create enemy
        JObject enemyData = dialogData["enemy"]!.ToObject<JObject>()!;
        string monsterName = enemyData.Value<string>("monsterName")!;
        int diceAmount = enemyData.Value<int>("diceAmount")!;
        int additionalDmg = enemyData.Value<int>("additionalDmg");
        int health = enemyData.Value<int>("health");

        JArray dropsData = (JArray)enemyData["drops"]!;
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

        // Create the enemy instance
        Enemy enemy = new(monsterName, diceAmount, additionalDmg, health, drops.ToArray());
        Combat combat = new();
        combat.StartCombat(hero, enemy);
    }
    public virtual List<string> Options()
    {
        return new List<string> { "" };
    }
    public async virtual Task Exit()
    {
        await Task.Delay(10);
    }
    public virtual void ChooseOptions(string chosenOption)
    {

    }
}