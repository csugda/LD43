using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class PurpleTile : MultiplierTile {

	public PurpleTile(Vector2 pos, TileManager manager) : base(pos, manager, 1)
	{
		this.Type = TileTypes.PURPLE;
	}
	public override int GetScore()
	{
		return 0;
	}
}
