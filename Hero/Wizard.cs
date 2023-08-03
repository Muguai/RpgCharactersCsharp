namespace Hero;
using Equipment;
public class Wizard : HeroClass
{
  
    public Wizard(string name)
    {
        this.heroName = name;
        this.validArmorTypes= new ArmorType[] { ArmorType.Cloth};
        this.validWeaponTypes= new WeaponType[] {WeaponType.Fists, WeaponType.Staff, WeaponType.Wand};
        this.heroStats = new HeroStats(1,1,8);
        this.levelUpStats = new HeroStats(1,1,5);
        this.damagingStat = "int";
        this.className = "Wizard";
    }
}