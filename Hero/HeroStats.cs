namespace Hero;
public class HeroStats{
    public int Strength {get; private set;} = 1;
    public int Dexterity {get; private set;} = 1;
    public int Intelligence {get; private set;} = 1;
    public HeroStats(int strength, int dexterity, int intelligence){
        this.Intelligence = intelligence;
        this.Dexterity = dexterity;
        this.Strength = strength;
    }

    public void Add(HeroStats stats){
        this.Strength += stats.Strength;
        this.Dexterity += stats.Dexterity;
        this.Intelligence += stats.Intelligence;
    }

    public int getSum(string stat){
        switch(stat){
            case "str":
                return Strength;
            case "int":
                return Intelligence;
            case "dex":
                return Dexterity;
        }

        return 0;
    }

    public override string ToString()
    {
        return "Str: " + Strength + " Int: " + Intelligence + " Dex: " + Dexterity;
    }

}