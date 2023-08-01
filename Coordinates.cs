public struct Coordinate{
    public int X { get; set; }
    public int Y { get; set; }

    public Coordinate(int x, int y){
        this.X = x;
        this.Y = y;
    }

    public Coordinate GetAdjacent(string direction){
        Coordinate result;
        switch(direction){
            case "Down":
                result = new Coordinate(X + 1 , Y);
                break;
            case "Up":
                result = new Coordinate(X - 1, Y);
                break;
            case "Right":
                result = new Coordinate(X, Y + 1);
                break;
            case "Left":
                result = new Coordinate(X, Y - 1);
                break;
            default:
                result = new Coordinate(X,Y);
                break;
        }

        return result;
    }
}