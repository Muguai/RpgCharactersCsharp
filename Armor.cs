public class Armor : Item
{
    public ArmorType armorType { get; private set; }
    public HeroStats armorStats { get; private set; }
    public Armor(ArmorType type, EquipmentType bodyType, HeroStats stats, string name)
    {
        this.armorType = type;
        this.equipType = bodyType;
        this.armorStats = stats;
        this.itemName = name;
    }
}