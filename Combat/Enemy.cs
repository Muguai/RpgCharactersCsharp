using Equipment;
namespace Combat;
public class Enemy{

    public string MonsterName { get; set; }
    public int DiceAmount { get; set; }

    public int AdditionalDmg { get; set; }
    public Item[] Drops { get; set; }
    
    public int Health { get; set; }
    public int MaxHealth { get; set; }


    public Enemy(string name, int diceAmount, int additionalDmg,int health, Item[] drops){
        MonsterName = name;
        DiceAmount = diceAmount;
        AdditionalDmg = additionalDmg;
        MaxHealth = health;
        Health = health;
        Drops = drops;
    }
}