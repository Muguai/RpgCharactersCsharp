namespace Equipment;
public abstract class Item{
    protected string itemName = "";
    public int RequiredLevel { get; protected set; }
    public Slot ItemSlot {get; protected set;}
}