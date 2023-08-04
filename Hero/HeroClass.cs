namespace Hero;
using Equipment;
using System.Text;
using Spectre.Console;
using System.Linq;
using Utils;

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
    protected int health;
    protected int maxHealth;
    protected Dictionary<Slot, Item> equipment = new Dictionary<Slot, Item>()
    {
        {Slot.Weapon, null!},
        {Slot.Head, null!},
        {Slot.Body, null!},
        {Slot.Legs, null!}
    };

    public List<Item> Inventory { get; private set; } = new List<Item>();

    //Just realising this is stupid cause might cause errors if a weapon and a misc item has the same name
    //Might fix later but for now i will just make sure to give each item a distinct name
    public void AddToInventory(Item item)
    {
        if (Inventory.Contains(item))
        {
            Item tempItem = HeroUtils.FindItemInInventory(Inventory, item.ItemName);
            if (tempItem.GetType() == typeof(Misc))
            {
                Misc tempItem2 = (Misc)tempItem;
                Misc tempItemPar = (Misc)item;
                tempItem2.Amount += tempItemPar.Amount;
            }
        }
        else
        {
            Inventory.Add(item);
        }
    }

    public void RemoveFromInventory(Item item)
    {
        if (Inventory.Contains(item))
        {
            Item tempItem = HeroUtils.FindItemInInventory(Inventory, item.ItemName);
            if (tempItem.GetType() == typeof(Misc))
            {
                Misc tempItem2 = (Misc)tempItem;
                Misc tempItemPar = (Misc)item;
                tempItem2.Amount += tempItemPar.Amount;
                if (tempItem2.Amount <= 0)
                    Inventory.Remove(item);
            }
            else
            {
                Inventory.Remove(item);
            }
        }
    }

    public void LevelUp()
    {
        this.level += 1;
        heroStats.Add(levelUpStats);
    }
    public int Damage()
    {
        if (equipment[Slot.Weapon] is null)
            return 0;
        Weapon w = (Weapon)equipment[Slot.Weapon];
        return w.WeaponDamage + (1 + TotalStats().getSum(damagingStat) / 100);
    }
    public void Equip(Weapon weapon)
    {
        if (!Array.Exists(validWeaponTypes, x => x == weapon.WeaponType) || weapon.RequiredLevel > level)
        {
             AnsiConsole.WriteLine("You cant Equip " + weapon.WeaponType.ToString() + " Weapons ");
             ConsoleUtils.PressEnterToContinue();
        }
        RemoveFromInventory(weapon);
        equipment[Slot.Weapon] = weapon;

    }
    public void Equip(Armor armor)
    {
        if (!Array.Exists(validArmorTypes, x => x == armor.ArmorType) || armor.RequiredLevel > level)
        {
            AnsiConsole.WriteLine("You cant Equip " + armor.ArmorType.ToString() + " Armors ");
            ConsoleUtils.PressEnterToContinue();
        }
        RemoveFromInventory(armor);
        equipment[armor.ItemSlot] = armor;
    }
    public void EquipFromInventory(string equipName)
    {
        Item tempItem = HeroUtils.FindItemInInventory(Inventory, equipName);

        if(tempItem is null){
            AnsiConsole.WriteLine("You aint got that item ");
            ConsoleUtils.PressEnterToContinue();
            return;
        }

        if(tempItem.GetType() == typeof(Misc)){
            AnsiConsole.WriteLine("You cant Equip misc Items ");
            ConsoleUtils.PressEnterToContinue();
        }else if(tempItem.GetType() == typeof(Armor)){
            Armor armor = (Armor)tempItem;
            Item toInventory = equipment[armor.ItemSlot];
             if(!(toInventory is null)){
                Inventory.Add((Armor)toInventory);
            }
            Equip(armor);

        }else if(tempItem.GetType() == typeof(Weapon)){
            Weapon weapon = (Weapon)tempItem;
            Item toInventory = equipment[Slot.Weapon];
            if(!(toInventory is null)){
                Inventory.Add((Weapon)toInventory);
            }

            Equip(weapon);
        }
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
    {;

        // Create the layout
        var layout = new Layout("Root")
            .SplitColumns(
                new Layout("Right")
                .SplitRows(new Layout("Left")
                .SplitRows(new Layout("Left1"),
                 new Layout("Left2").SplitRows(new Layout("Right1"),
                 new Layout("Right2"))))

               );
        
        // ------------ Characther Info ------------------

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

        // ------------ Inventory ------------------

        int itemsPerColumn = 8;
        var tables = GenerateInventoryTables(Inventory);

        var columns = (int)Math.Ceiling((double)tables.Count / itemsPerColumn);
        var grid = new Grid().Collapse();

        for (int col = 0; col < columns; col++)
        {
            grid.AddColumn(new GridColumn());
        }

        for (int row = 0; row < itemsPerColumn; row++)
        {
            var rowTables = new List<Table>();
            for (int col = 0; col < columns; col++)
            {
                var index = col * itemsPerColumn + row;
                if (index < tables.Count)
                {
                    rowTables.Add(tables[index]);
                }
            }
            grid.AddRow(rowTables.ToArray());
        }


        Panel inventoryPanel = new Panel(Align.Center(
                    grid, VerticalAlignment.Middle));
        inventoryPanel.Header("Inventory");
        inventoryPanel.HeaderAlignment(Justify.Center);

        layout["Right1"].Update(inventoryPanel.Expand());

        // ------------ Stats ------------------
        
        BarChart barChart = new BarChart()
        .Width(150)
        .Label($"[green slowblink]Level {level}[/]")
        .CenterLabel()
        .AddItem("Str", SpecificStat("str"), Color.Orange1)
        .AddItem("Dex", SpecificStat("dex"), Color.Blue)
        .AddItem("Int", SpecificStat("int"), Color.Purple);

         BreakdownChart breakChart = new BreakdownChart()
            .Width(150)
            .AddItem("Health", health, Color.Red)
            .AddItem("Lost Health", maxHealth - health, Color.DarkRed);

        var grid2 = new Grid().Collapse();
        grid2.AddColumn(new GridColumn().PadLeft(5));
        grid2.AddColumn(new GridColumn().PadLeft(5)); 
        grid2.AddRow(barChart, breakChart);

        Panel statPanel = new Panel(
                Align.Center(
                    grid2, VerticalAlignment.Middle));

        statPanel.Header("Stats");
        statPanel.HeaderAlignment(Justify.Center);
        layout["Right2"].Update(
            statPanel.Expand()
            );


        layout["Left1"].Size(20);
        layout["Right1"].MinimumSize(30);

        layout["Right2"].Size(10);
        layout["Right"].MinimumSize(5);
        AnsiConsole.Write(layout);
        ConsoleUtils.PressEnterToContinue();

    }



    static List<Table> GenerateInventoryTables(List<Item> inventoryList)
    {
        var tables = new List<Table>();

        foreach (var item in inventoryList)
        {
            var table = new Table { Border = TableBorder.Rounded };

            switch (item.GetType().Name)
            {
                case nameof(Misc):
                    Misc tempItem = (Misc)item;
                    table.AddColumn("Misc");
                    table.AddRow(tempItem.ItemName);
                    table.AddRow("Amount: " + tempItem.Amount);
                    break;
                case nameof(Weapon):
                    Weapon tempItem2 = (Weapon)item;
                    table.AddColumn("Weapon");
                    table.AddRow(tempItem2.ItemName);
                    table.AddRow("Type: " + tempItem2.WeaponType.ToString());
                    table.AddRow("Damage: " + tempItem2.WeaponDamage);
                    table.AddRow("Req Level: " + tempItem2.RequiredLevel);
                    break;
                case nameof(Armor):
                    Armor tempItem3 = (Armor)item;
                    table.AddColumn("Armor");
                    table.AddRow(tempItem3.ItemName);
                    table.AddRow("Type: " + tempItem3.ArmorType.ToString());
                    table.AddRow("Slot: " + tempItem3.ItemSlot.ToString());
                    table.AddRow(tempItem3.ArmorStats.ToString());
                    table.AddRow("Req Level: " + tempItem3.RequiredLevel);
                    ;
                    break;
            }
            tables.Add(table);


        }

        return tables;
    }
}