public class HeroStats{
    public int strength {get; private set;} = 1;
    public int dexterity {get; private set;} = 1;
    public int intelligence {get; private set;} = 1;
    public HeroStats(int strength, int dexterity, int intelligence){
        this.intelligence = intelligence;
        this.dexterity = dexterity;
        this.strength = strength;
    }

    public void Add(HeroStats stats){
        this.strength += stats.strength;
        this.dexterity += stats.dexterity;
        this.intelligence += stats.intelligence;
    }

    public int getSum(string stat){
        switch(stat){
            case "str":
                return strength;
            case "int":
                return intelligence;
            case "dex":
                return dexterity;
        }

        return 0;
    }

}