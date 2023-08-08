namespace Tiles;
public abstract class Tile{
    public async virtual Task Enter(){
        await Task.Delay(10);
    }
    public virtual List<string> Options(){
        return new List<string> { ""};
    };
    public async virtual Task Exit(){
        await Task.Delay(10);
    }
    public virtual void ChooseOptions(string chosenOption){

    }
}