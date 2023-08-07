namespace Hero;
using Equipment;
public class Barbarian : HeroClass
{

    public Barbarian(string name)
    {
        this.heroName = name;
        //A barbarian cant wear leather.... really... ok ill let this on slide
        this.validArmorTypes = new ArmorType[] { ArmorType.Mail, ArmorType.Plate };
        this.validWeaponTypes= new WeaponType[] {WeaponType.Fists, WeaponType.Hatchet, WeaponType.Mace, WeaponType.Sword };
        this.heroStats = new HeroStats(5,2,1);
        this.levelUpStats = new HeroStats(3,2,1);
        this.damagingStat = "str";
        this.className = "Barbarian";
        this.MaxHealth = 20;
        this.Health = this.MaxHealth;
    }
}