namespace Equipment;
public class Weapon : Item{

    public WeaponType WeaponType{get; private set;} 
    public int WeaponDamage{get; private set;} 

    public Weapon(WeaponType type, int dmg, string name, int reqLevel){
        this.WeaponType = type;
        this.ItemSlot = Slot.Weapon; 
        this.WeaponDamage = dmg;
        this.itemName = name;
        this.RequiredLevel = reqLevel;
    }
    public override string ToString(){
        return ItemSlot.ToString() + ": " + itemName + " Type: " + WeaponType.ToString() + " Damage: " + WeaponDamage + " Req Level: "  + RequiredLevel;
    }
}