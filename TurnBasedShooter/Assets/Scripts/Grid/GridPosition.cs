public struct GridPosition //structs are used when we need a copy of values instead of class which is a reference type
{ //works like a Vector3
   //we could use Vector2Int but it takes x and y as parameters and we need x and z. it can be confusing hence we make our own struct
    public int x;
    public int z;

    public GridPosition(int x, int z)
    {
        this.x = x;
        this.z = z;
    }

    public override string ToString()
    {
        return "x: " + x + "; z: " + z; //overriden here so int values can be passed in debug.log as well
    }
}
