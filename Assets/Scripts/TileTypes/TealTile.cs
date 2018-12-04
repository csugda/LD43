using UnityEngine;

class TealTile : Tile
{
	public TealTile(Vector2 pos, TileManager manager) : base(pos, manager)
	{
		this.Type = TileTypes.TEAL;
	}
	public override int GetScore()
	{
		float mult = 1f;
		int score = 0;
		bool[] typePresent = new bool[System.Enum.GetValues(typeof(TileTypes)).Length];
		foreach (Tile t in this.Neighbors)
		{
			switch (t.Type)
			{
				case TileTypes.WALL:
					if (!typePresent[0] == true)
					{
						typePresent[0] = true;
						score++;
					}
					break;
				case TileTypes.EMPTY:
					if (!typePresent[1] == true)
					{
						typePresent[1] = true;
						score++;
					}
					break;
				case TileTypes.GREEN:
					if (!typePresent[2] == true)
					{
						typePresent[2] = true;
						score++;
					}
					break;
				case TileTypes.RED:
					if (!typePresent[3] == true)
					{
						typePresent[3] = true;
						score++;
					}
					break;
				case TileTypes.TEAL:
					if (!typePresent[4] == true)
					{
						typePresent[4] = true;
						score++;
					}
					break;
				case TileTypes.TAN:
					if (!typePresent[5] == true)
					{
						typePresent[5] = true;
						score++;
					}
					break;
				case TileTypes.PURPLE:
					if (!typePresent[6] == true)
					{
						typePresent[6] = true;
						score++;
					}
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
