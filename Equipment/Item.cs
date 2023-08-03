namespace Equipment;
public abstract class Item{
    public string ItemName { get; set; }= "";
    public int RequiredLevel { get; protected set; }
    public Slot ItemSlot {get; protected set;}
}