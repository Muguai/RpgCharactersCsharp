namespace Hero;
using Equipment;
public class SwashBuckler : HeroClass
{
    
    public SwashBuckler(string name)
    {   
        this.heroName = name;
        this.validArmorTypes = new ArmorType[] { ArmorType.Leather, ArmorType.Mail};
        this.validWeaponTypes = new WeaponType[] { WeaponType.Dagger, WeaponType.Sword};
        this.heroStats = new HeroStats(2,6,1);
        this.levelUpStats = new HeroStats(1,4,1);
        this.damagingStat = "dex";
        this.className = "SwashBuckler";
    }
}