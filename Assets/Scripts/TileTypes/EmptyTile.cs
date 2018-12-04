using UnityEngine;

class EmptyTile : Tile
{
    public EmptyTile(Vector2 pos, TileManager manager) : base(pos, manager)
    {
        this.Type = TileTypes.EMPTY;
    }
    public override int GetScore()
    {
        return 0;
    }
}
