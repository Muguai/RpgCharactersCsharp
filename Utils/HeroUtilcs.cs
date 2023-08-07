namespace Utils;
using Equipment;
public static class HeroUtils{
    //This gets fucky if there is multiple items with the same name. but im just gonna ignore that
    public static Item FindItemInInventory(List<Item> inventory, string itemName){
        return inventory.Find(x => x.ItemName.ToLower() == itemName.ToLower())!;
    }
}