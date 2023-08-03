namespace Hero;
using Equipment;
public class Archer : HeroClass
{
    public Archer(string name)
    {
        this.heroName = name;
        this.validArmorTypes = new ArmorType[] { ArmorType.Mail, ArmorType.Plate };
        //How is an archer gonna defend himself on close range if he only got a bow. Im giving em daggers at least
        this.validWeaponTypes = new WeaponType[] { WeaponType.Fists, WeaponType.Bow, WeaponType.Dagger };
        this.heroStats = new HeroStats(1,7,1);
        this.levelUpStats = new HeroStats(1,5,1);
        this.damagingStat = "dex";
        this.className = "Archer";
    }
}