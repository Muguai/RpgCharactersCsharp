namespace Equipment;
using Hero;
public class Armor : Item
{
    public ArmorType ArmorType { get; private set; }
    public HeroStats ArmorStats { get; private set; }
    public Armor(ArmorType type, Slot bodyType, HeroStats stats, string name, int reqLevel)
    {
        this.ArmorType = type;
        this.ItemSlot = bodyType;
        this.ArmorStats = stats;
        this.ItemName = name;
        this.RequiredLevel = reqLevel;
    }

    public override string ToString(){
        return ItemSlot.ToString() + ": " + ItemName + " Type: " + ArmorType.ToString() + " Stat Increase: " + ArmorStats.ToString() + " Req Level: " + RequiredLevel;
    }
}