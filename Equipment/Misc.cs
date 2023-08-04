namespace Equipment;
public class Misc : Item{

    public MiscType ItemMiscType { get; set; }
    public int? Amount { get; set; } = 1;
    public Misc(string name, MiscType type, int amount){
        this.ItemName = name;
        this.ItemMiscType = type;
        this.Amount = amount;
    }

    public override string ToString(){
        if(Amount == 1){
             return $"|<{ItemName}>|";
        }else{
             return $"|<{ItemName} - Amount: {Amount}>|";
        }
    }
}