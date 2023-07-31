using System.Collections.Generic;

public class Barbarian : HeroClass{
    List<string> validWeaponTypes = new List<string>();
    List<string> validArmorTypes = new List<string>();

    public Barbarian(string name){
        this.heroName = name;
    }
}