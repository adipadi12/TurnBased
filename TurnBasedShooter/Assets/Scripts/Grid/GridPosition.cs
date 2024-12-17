using System;

public struct GridPosition : IEquatable<GridPosition>//structs are used when we need a copy of values instead of class which is a reference type
{ //works like a Vector3
   //we could use Vector2Int but it takes x and y as parameters and we need x and z. it can be confusing hence we make our own struct
    public int x;
    public int z;

    public GridPosition(int x, int z)
    {
        this.x = x;
        this.z = z;
    }

    public override bool Equals(object obj)
    {
        return obj is GridPosition position &&
               x == position.x &&
               z == position.z;
    }

    public bool Equals(GridPosition other)
    {
        return this == other;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(x, z);
    }

    public override string ToString()
    {
        return "x: " + x + "; z: " + z; //overriden here so int values can be passed in debug.log as well
    }

    public static bool operator ==(GridPosition a, GridPosition b)
    {
        return a.x == b.x && a.z == b.z;
    }

    public static bool operator != (GridPosition a, GridPosition b) {
        return !(a == b);  //defining this in the struct so we can compare in the unit script
    }

    public static GridPosition operator +(GridPosition a, GridPosition b)
    {
        return new GridPosition(a.x + b.x, a.z + b.z);
    }

    public static GridPosition operator -(GridPosition a, GridPosition b)
    {
        return new GridPosition(a.x - b.x, a.z - b.z);
    }
}
