namespace Hero;
using Equipment;
using System.Text;

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

    /// <summary>
    /// Increases the heros level by 1 and adds levelUpStats to its total stats
    /// </summary>
    public void LevelUp()
    {
        this.level += 1;
        heroStats.Add(levelUpStats);
    }
    /// <summary>
    /// Calculates heroes damage based on its damagingStat and its currently equiped weapon damage
    /// </summary>
    /// <returns>Damage done by hero</returns>
    public int Damage()
    {
        if (equipment[Slot.Weapon] is null)
            return 0;
        Weapon w = (Weapon)equipment[Slot.Weapon];
        return w.WeaponDamage + (1 + TotalStats().getSum(damagingStat) / 100);
    }
    /// <summary>
    /// Equips Weapon To Hero
    /// </summary>
    /// <param name="weapon"></param>
    public void Equip(Weapon weapon)
    {
        if (!Array.Exists(validWeaponTypes, x => x == weapon.WeaponType) || weapon.RequiredLevel > level)
        {
            throw new InvalidWeaponException();
        }

        equipment[Slot.Weapon] = weapon;

    }
    /// <summary>
    /// Equip Armor to Hero
    /// </summary>
    /// <param name="armor"></param>
    public void Equip(Armor armor)
    {
        if (!Array.Exists(validArmorTypes, x => x == armor.ArmorType) || armor.RequiredLevel > level)
        {
            throw new InvalidArmorException();
        }

        equipment[armor.ItemSlot] = armor;

    }
    /// <summary>
    /// Acceptable input => (str, dex, int)
    /// </summary>
    /// <param name="stat"></param>
    /// <returns>The total of a specific stat the hero has (str, dex or int)</returns>
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

    /// <summary>
    /// 
    /// </summary>
    /// <returns>The total of all the heroes stats</returns>
    public HeroStats TotalStats()
    {
        return new HeroStats(SpecificStat("str"), SpecificStat("dex"), SpecificStat("int"));
    }

    /// <summary>
    /// Writes a summary of the hero to the console
    /// </summary>
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
        Console.WriteLine(sb.ToString());
    }
}