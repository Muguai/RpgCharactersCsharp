namespace Equipment;
public abstract class Item{
    public string ItemName { get; set; } = "";
    public int RequiredLevel { get; protected set; } = 1;
    public Slot ItemSlot {get; protected set;} = Slot.None;
}