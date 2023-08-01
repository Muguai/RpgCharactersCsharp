public class Armor : Item
{
    public ArmorType ArmorType { get; private set; }
    public HeroStats ArmorStats { get; private set; }
    public Armor(ArmorType type, EquipmentType bodyType, HeroStats stats, string name)
    {
        this.ArmorType = type;
        this.EquipType = bodyType;
        this.ArmorStats = stats;
        this.itemName = name;
    }
}