using UnityEngine;

abstract public class MultiplierTile : Tile
{

	private float mult;
	public MultiplierTile(Vector2 pos, TileManager manager, float mult) : base(pos, manager)
	{
		this.mult = mult;
	}
	public float getMultiplier()
	{
		return this.mult;
	}
}
