namespace Equipment;
public abstract class Item{
    protected string itemName = "";
    public Slot ItemSlot {get; protected set;}
}