namespace Equipment;
using Hero;
public class Armor : Item
{
    public ArmorType ArmorType { get; private set; }
    public HeroStats ArmorStats { get; private set; }
    public Armor(ArmorType type, Slot bodyType, HeroStats stats, string name)
    {
        this.ArmorType = type;
        this.ItemSlot = bodyType;
        this.ArmorStats = stats;
        this.itemName = name;
    }
}