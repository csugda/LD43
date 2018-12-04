using UnityEngine;

abstract public class Tile
{
    public  Vector2 pos;
    private float Z  { get { return (-pos.x - pos.y); } }
    private TileManager gm;

    public TileTypes Type { get; protected set; }
    public abstract int GetScore();

    private static Vector2[] neighborOffsets = { new Vector2(-1, 0), new Vector2(1, 0), new Vector2(0, 1),
                                                 new Vector2(0, -1), new Vector2(1, -1), new Vector2(-1, 1) };
    public Tile[] Neighbors
    {
        get
        {
            //Debug.Log(this.pos);
            Tile[] n = new Tile[6];
            for (int i = 0; i < 6; ++i)
            {
                Vector2 offset = this.pos + neighborOffsets[i];
                if (offset.x < 0 || offset.y < 0 || offset.x >= gm.boardSize().x || offset.y >= gm.boardSize().y)
                {
                    //Debug.Log(offset.ToString() + " is out of bounds");
                    n[i] = new WallTile(offset, gm);
                }
                else
                {
                    //Debug.Log(offset.ToString() + " is in bounds");
                    n[i] = gm.GetTile(offset);
                }
				//Debug.Log(offset + " : " + n[i]);
            }
            return n;
        }
    }

    public void Move(Vector2 newPos)
    {
        this.pos = newPos;
    }

    public Tile(Vector2 pos, TileManager manager)
    {
        this.pos = pos;
        this.gm = manager;
    }
}
