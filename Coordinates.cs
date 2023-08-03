public struct Coordinate
{
    public int X { get; set; }
    public int Y { get; set; }

    public Coordinate(int x, int y)
    {
        this.X = x;
        this.Y = y;
    }

    public Coordinate getAdjacent(string dir)
    {
        Coordinate cord = new Coordinate(this.X, this.Y);
        switch (dir)
        {
            case "Up":
                cord = new Coordinate(X - 1, Y);
                break;
            case "Down":
                cord = new Coordinate(X + 1, Y);
                break;
            case "Left":
                cord = new Coordinate(X, Y - 1);
                break;
            case "Right":
                cord = new Coordinate(X, Y + 1);
                break;
        }
        return cord;
    }

}