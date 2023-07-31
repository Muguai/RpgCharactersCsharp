using System.Collections.Generic;

public class Wizard : HeroClass
{
  
    public Wizard(string name)
    {
        this.heroName = name;
        this.validArmorTypes= new ArmorType[] { ArmorType.Cloth};
        this.validWeaponTypes= new WeaponType[] { WeaponType.Staff, WeaponType.Wand};
        this.heroStats = new HeroStats(1,1,8);
        this.levelUpStats = new HeroStats(1,1,5);
        this.damagingStat = "int";
    }
}