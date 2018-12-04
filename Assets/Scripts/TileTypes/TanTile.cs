using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TanTile : Tile
{
    public override int GetScore()
    {
        float mult = 1;
        Queue<Tile> tilesFound = new Queue<Tile>();
        List<Tile> tilesExplored = new List<Tile>();
        foreach (Tile n in this.Neighbors)
        {
            if (n.Type == TileTypes.TAN)
                tilesFound.Enqueue(n);
            if (n is MultiplierTile)
            {
                MultiplierTile mt = (MultiplierTile)n;
                mult += mt.getMultiplier(); //FIXME can make this *= if needed
            }
        }
        while(tilesFound.Count > 0)
        {
            Tile active = tilesFound.Dequeue();
            foreach (Tile n in active.Neighbors)
                if (n != this && n.Type == TileTypes.TAN && !tilesExplored.Contains(n) && !tilesFound.Contains(n))
                    tilesFound.Enqueue(n);
            tilesExplored.Add(active);
        }
        return (int)(tilesExplored.Count * mult);
    }

    public TanTile (Vector2 pos, TileManager manager) : base(pos, manager)
    {
        this.Type = TileTypes.TAN;
    }
}
