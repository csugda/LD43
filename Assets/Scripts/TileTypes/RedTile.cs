using UnityEngine;

class RedTile : Tile
{
    public RedTile(Vector2 pos, TileManager manager) : base(pos, manager)
    {
        this.Type = TileTypes.RED;
    }
    public override int GetScore()
    {
		float mult = 1f;
        int score = 0;
        foreach (Tile t in this.Neighbors)
        {
            switch (t.Type)
            {
                case TileTypes.GREEN:
                    score -= 1;
                    break;
                case TileTypes.RED:
                    score += 2;
                    break;
                default:
                    break;
            }
			if (t is MultiplierTile)
            {
				MultiplierTile mt = (MultiplierTile)t;
				mult += mt.getMultiplier(); //FIXME can make this *= if needed
			}
		}
        return (int)(score * mult);
    }
}
