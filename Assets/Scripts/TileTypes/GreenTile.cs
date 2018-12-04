using UnityEngine;

public class GreenTile : Tile
{

    public GreenTile(Vector2 pos, TileManager manager) : base(pos, manager) {
        this.Type = TileTypes.GREEN;
    }

    public override int GetScore()
    {
        float mult = 1f;
        int score = 0;
        foreach (Tile t in this.Neighbors)
        {
            if (t.GetType().Equals(this.GetType()))
            {
                score += 1;
            }
            //if multiplierTile
			if (t is MultiplierTile)
			{
				MultiplierTile mt = (MultiplierTile)t;
				mult += mt.getMultiplier(); //FIXME can make this *= if needed
			}
            
        }
        return (int)(score * mult);
    }
}

