using UnityEngine;

class WallTile : Tile
{
    public WallTile (Vector2 pos, TileManager manager) : base(pos, manager)
    {
        this.Type = TileTypes.WALL;
    }
    public override int GetScore()
    {
        return 0;
    }
}

