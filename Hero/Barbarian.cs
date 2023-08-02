namespace Hero;
using Equipment;
public class Barbarian : HeroClass
{

    public Barbarian(string name)
    {
        this.heroName = name;
        this.validArmorTypes = new ArmorType[] { ArmorType.Mail, ArmorType.Plate };
        this.validWeaponTypes= new WeaponType[] { WeaponType.Hatchet, WeaponType.Mace, WeaponType.Sword };
        this.heroStats = new HeroStats(5,2,1);
        this.levelUpStats = new HeroStats(3,2,1);
        this.damagingStat = "str";
        this.className = "Barbarian";
    }
}