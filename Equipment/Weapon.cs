namespace Equipment;
public class Weapon : Item{

    public WeaponType WeaponType{get; private set;} 
    public int WeaponDamage{get; private set;} 
    public int AmountOfDice { get; set; }

    public Weapon(WeaponType type,int amountOfDice, int dmg, string name, int reqLevel){
        this.WeaponType = type;
        this.ItemSlot = Slot.Weapon; 
        this.AmountOfDice = amountOfDice;
        this.WeaponDamage = dmg;
        this.ItemName = name;
        this.RequiredLevel = reqLevel;
    }
    public override string ToString(){
        return ItemSlot.ToString() + ": " + ItemName + " Type: " + WeaponType.ToString() +" Amount of dice: " + AmountOfDice +" Damage: " + WeaponDamage + " Req Level: "  + RequiredLevel;
    }

    public int Attack(){
        return 1;
    }
}