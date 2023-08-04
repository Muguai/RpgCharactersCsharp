namespace Utils;
using Equipment;
public static class HeroUtils{
    public static Item FindItemInInventory(List<Item> inventory, string itemName){
        return inventory.Find(x => x.ItemName.ToLower() == itemName.ToLower())!;
    }
}