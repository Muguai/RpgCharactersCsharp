using System.Collections.Generic;
using System.Linq;
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
    protected Dictionary<EquipmentType, Item> equipment = new Dictionary<EquipmentType, Item>()
    {
        {EquipmentType.Weapon, null!},
        {EquipmentType.Head, null!},
        {EquipmentType.Body, null!},
        {EquipmentType.Legs, null!}
    };

    public void LevelUp()
    {
        this.level += 1;
        heroStats.Add(levelUpStats);
    }
    public int Damage()
    {
        if (equipment[EquipmentType.Weapon] == null)
            return 0;
        Weapon w = (Weapon)equipment[EquipmentType.Weapon];
        return w.WeaponDamage + (1 + heroStats.getSum(damagingStat) / 100);
    }
    public void Equip(Weapon weapon)
    {
        if (!Array.Exists(validWeaponTypes, x => x == weapon.WeaponType))
        {
            //error
            return;
        }

        equipment[EquipmentType.Weapon] = weapon;

    }
    public void Equip(Armor armor)
    {
        if (!Array.Exists(validArmorTypes, x => x == armor.ArmorType))
        {
            //error
            return;
        }

        equipment[armor.EquipType] = armor;

    }
    public int SpecificStat(string stat)
    {
        int total = 0;
        foreach (KeyValuePair<EquipmentType, Item> i in equipment)
        {
            if (i.Value == null)
                continue;
            if (i.Key == EquipmentType.Weapon)
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
        Console.WriteLine(sb.ToString());
    }
}