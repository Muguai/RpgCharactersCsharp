namespace Equipment;
public class Weapon : Item{

    public WeaponType WeaponType{get; private set;} 
    public int WeaponDamage{get; private set;} 

    Weapon(WeaponType type, int dmg, string name){
        this.WeaponType = type;
        this.ItemSlot = Slot.Weapon; 
        this.WeaponDamage = dmg;
        this.itemName = name;
    }
}