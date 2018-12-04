using System;
using System.IO;
using UnityEngine;

public class Board {
	private Tile[,] board;
	public Board(String boardArray, TileManager tm)
	{
        board = new Tile[7, 7];

        //StringReader sr = new StringReader(boardArray);
        using (StringReader reader = new StringReader(boardArray))
        {
            // Loop over the lines in the string.
            int count = 0;
            string line;
            int i = 0;
            while ((line = reader.ReadLine()) != null)
            {
                //Debug.Log(line);
                int j = 0;
                foreach (String tile in line.Split(' '))
                {
                    //Debug.Log(tile);
                    //if (!Enum.IsDefined(typeof(TileTypes), tile))
                    board[i, j] = makeTile(new Vector2((float)i, (float)j), (TileTypes)Enum.Parse(typeof(TileTypes), tile), tm);
                    //Debug.Log(board[i, j]);
                    ++j;
                }
                ++i;
            }

        }
    }

	public Tile[,] getBoard()
	{
		return board;
	}

	public Tile makeTile(Vector2 pos, TileTypes type, TileManager tm)
	{
		Tile t;
		switch (type)
		{
			case TileTypes.WALL:
				t = new WallTile(pos, tm);
				break;
			case TileTypes.EMPTY:
				t = new EmptyTile(pos, tm);
				break;
			case TileTypes.GREEN:
				t = new GreenTile(pos, tm);
				break;
			case TileTypes.RED:
				t = new RedTile(pos, tm);
				break;
			case TileTypes.TEAL:
				t = new TealTile(pos, tm);
				break;
			case TileTypes.PURPLE:
				t = new PurpleTile(pos, tm);
				break;
			case TileTypes.TAN:
				t = new TanTile(pos, tm);
				break;
			default:
				t = new WallTile(pos, tm);
				break;
		}
		//Debug.Log(pos + " : " + t);
		return t;
	}
}