public class Weapon : Item{

    public WeaponType weaponType{get; private set;} 
    public int weaponDamage{get; private set;} 

    Weapon(WeaponType type, int dmg, string name){
        this.weaponType = type;
        this.equipType = EquipmentType.Weapon; 
        this.weaponDamage = dmg;
        this.itemName = name;
    }
}