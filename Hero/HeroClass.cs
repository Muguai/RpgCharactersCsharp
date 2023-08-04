namespace Hero;
using Equipment;
using System.Text;
using Spectre.Console;

public abstract class HeroClass
{
    protected string heroName = "";
    protected string className = "";
    protected int level = 1;
    protected string damagingStat = "";
    protected HeroStats heroStats = null!;
    protected HeroStats levelUpStats = null!;
    protected ArmorType[] validArmorTypes = null!;
    protected WeaponType[] validWeaponTypes = null!;
    protected Dictionary<Slot, Item> equipment = new Dictionary<Slot, Item>()
    {
        {Slot.Weapon, null!},
        {Slot.Head, null!},
        {Slot.Body, null!},
        {Slot.Legs, null!}
    };

    public void LevelUp()
    {
        this.level += 1;
        heroStats.Add(levelUpStats);
    }
    public int Damage()
    {
        if (equipment[Slot.Weapon] == null)
            return 0;
        Weapon w = (Weapon)equipment[Slot.Weapon];
        return w.WeaponDamage + (1 + TotalStats().getSum(damagingStat) / 100);
    }
    public void Equip(Weapon weapon)
    {
        if (!Array.Exists(validWeaponTypes, x => x == weapon.WeaponType) || weapon.RequiredLevel > level)
        {
            throw new InvalidWeaponException();
        }

        equipment[Slot.Weapon] = weapon;

    }
    public void Equip(Armor armor)
    {
        if (!Array.Exists(validArmorTypes, x => x == armor.ArmorType) || armor.RequiredLevel > level)
        {
            throw new InvalidArmorException();
        }

        equipment[armor.ItemSlot] = armor;

    }
    public int SpecificStat(string stat)
    {
        int total = 0;
        foreach (KeyValuePair<Slot, Item> i in equipment)
        {
            if (i.Value == null)
                continue;
            if (i.Key == Slot.Weapon)
                continue;

            Armor a = (Armor)i.Value;
            total += a.ArmorStats.getSum(stat);
        }

        return heroStats.getSum(stat) + total;
    }

    public HeroStats TotalStats()
    {
        return new HeroStats(SpecificStat("str"), SpecificStat("dex"), SpecificStat("int"));
    }

    public void Display()
    {
        StringBuilder sb = new StringBuilder();
        sb.AppendFormat(heroName + "'s Overview");
        sb.AppendLine();
        sb.AppendFormat("Class: " + className);
        sb.AppendLine();
        sb.AppendFormat("Level: " + level);
        sb.AppendLine();
        sb.AppendFormat("Strength: " + SpecificStat("str"));
        sb.AppendLine();
        sb.AppendFormat("Dexterity: " + SpecificStat("dex"));
        sb.AppendLine();
        sb.AppendFormat("Intelligence: " + SpecificStat("int"));
        sb.AppendLine();
        sb.AppendFormat("Damage: " + Damage());
        //Console.WriteLine(sb.ToString());

        BarChart barChart = new BarChart()
        .Width(150)
        .Label($"[green slowblink]Level {level}[/]")
        .CenterLabel()
        .AddItem("Str", SpecificStat("str"), Color.Red)
        .AddItem("Dex", SpecificStat("dex"), Color.Blue)
        .AddItem("Int", SpecificStat("int"), Color.Purple);


        // Create the layout
        var layout = new Layout("Root")
            .SplitColumns(
                new Layout("Right")
                .SplitRows(new Layout("Left")
                .SplitRows(new Layout("left1"),
                 new Layout("left2").SplitRows(new Layout("right1"),
                 new Layout("right2"))))

               );

        string weaponName = equipment[Slot.Weapon] == null ? " None " : equipment[Slot.Weapon].ToString()!;
        string headName = equipment[Slot.Head] == null ? "Head: None" : equipment[Slot.Head].ToString()!;
        string bodyName = equipment[Slot.Body] == null ? "Body: None" : equipment[Slot.Body].ToString()!;
        string legName = equipment[Slot.Legs] == null ? "Legs: None" : equipment[Slot.Legs].ToString()!;

        Panel charactherInfoPanel = new Panel(
                Align.Center(
                    new Rows(
            new Text($"Name: {heroName}"),
            new Text(" "),
            new Text($"Class: {className}"),
            new Text(" "),
            new Text(" "),
            new Text($"---Equipped---"),
            new Text(" "),
            new Text($"{weaponName}"),
            new Text(" "),
            new Text($"{headName}"),
            new Text(" "),
            new Text($"{bodyName}"),
            new Text(" "),
            new Text($"{legName}")
        ), VerticalAlignment.Middle));

        charactherInfoPanel.Header("Characther Info");
        charactherInfoPanel.HeaderAlignment(Justify.Center);
        layout["Left1"].Update(charactherInfoPanel.Expand());

        Panel inventoryPanel = new Panel(
               Align.Center(new Markup(""), VerticalAlignment.Top));


        inventoryPanel.Header("Inventory");
        inventoryPanel.HeaderAlignment(Justify.Center);

        layout["right1"].Update(inventoryPanel.Expand());

        Panel statPanel = new Panel(
                Align.Center(
                    barChart, VerticalAlignment.Middle));

        statPanel.Header("Stats");
        statPanel.HeaderAlignment(Justify.Center);
        layout["right2"].Update(
            statPanel.Expand()
            );


        layout["left1"].MinimumSize(3);
        layout["right1"].MinimumSize(5);

        layout["right2"].MinimumSize(5);
        layout["Right"].MinimumSize(5);
        AnsiConsole.Write(layout);

        var name = AnsiConsole.Ask<string>($"Type Any Key To Continue{layout["left2"].Size}..");
    }
}