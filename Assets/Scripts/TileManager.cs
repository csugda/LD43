using System;
using System.Collections.Generic;
using UnityEngine;

public class TileManager
{
    private Tile[,] board;
    public int curBoard { get; private set; }
    //private TileTypes[,] tempBoard =
    //{
    //    {TileTypes.WALL, TileTypes.WALL,TileTypes.RED,TileTypes.GREEN,TileTypes.EMPTY },
    //    {TileTypes.WALL, TileTypes.GREEN,TileTypes.RED,TileTypes.TEAL,TileTypes.RED },
    //    {TileTypes.GREEN, TileTypes.RED,TileTypes.GREEN,TileTypes.GREEN,TileTypes.EMPTY },
    //    {TileTypes.GREEN, TileTypes.GREEN,TileTypes.RED,TileTypes.GREEN,TileTypes.WALL },
    //    {TileTypes.GREEN, TileTypes.GREEN,TileTypes.RED,TileTypes.WALL,TileTypes.WALL }
    //};

    private TextAsset boardFile;
    private TileTypes[,] tempBoard =
    {
        {TileTypes.WALL, TileTypes.WALL,TileTypes.RED,TileTypes.TAN,TileTypes.EMPTY },
        {TileTypes.WALL, TileTypes.GREEN,TileTypes.RED,TileTypes.TEAL,TileTypes.RED },
        {TileTypes.GREEN, TileTypes.RED,TileTypes.RED,TileTypes.GREEN,TileTypes.EMPTY },
        {TileTypes.PURPLE, TileTypes.TAN,TileTypes.TAN,TileTypes.GREEN,TileTypes.WALL },
        {TileTypes.GREEN, TileTypes.GREEN,TileTypes.RED,TileTypes.WALL,TileTypes.WALL }
    };


    public Tile GetTile(Vector2 p)
    {
        return this.board[(int)p.x, (int)p.y];
    }

    public TileScore[,] GetBoard()
    {
        TileScore[,] result = new TileScore[board.GetLength(0), board.GetLength(1)];
        for (int i = 0; i < board.GetLength(0); ++i)
        {
            for (int j = 0; j < board.GetLength(1); ++j)
            {
                //Debug.Log("Tile " + i.ToString() + ", " + j.ToString() + board[i, j].Type);
                //Debug.Log(board[i, j].GetScore().ToString());
                result[i, j] = new TileScore(board[i, j].Type, board[i, j].GetScore());
            }
        }
        return result;
    }

    internal Vector2 boardSize()
    {
        return new Vector2(this.board.GetLength(0), this.board.GetLength(1));
    }


	List<Board> boards;
	public List<TileTypes> loadBoard()
    {
		// load textfile as a single string
		String txtContents = boardFile.text;

		// separate string into individual boards
		String[] boardArrays;
		boardArrays = txtContents.Split('-');

		// separate the resulting strings again by newline char to get individual lines
		boards = new List<Board>();
		foreach (String s in boardArrays)
		{
			boards.Add(new Board(s, this));
		}
		//Debug.Log("lines " + lines.ToString());


		
		//Debug.Log(txtContents);
		
		return null;
    }
    public int boardCount { get { return this.boards.Count; } }
    public TileManager(TextAsset boardFile)
    {
        this.boardFile = boardFile;

        loadBoard();
        loadBoard(0);
        
    }
    public void loadBoard(int index)
    {
        board = boards[index].getBoard();
        curBoard = index;
    }
    public void Swap(Vector2 p1, Vector2 p2)
    {
        Tile temp = board[(int)p2.x, (int)p2.y];
        board[(int)p2.x, (int)p2.y] = board[(int)p1.x, (int)p1.y];
        board[(int)p1.x, (int)p1.y] = temp;
        board[(int)p2.x, (int)p2.y].Move(p2);
        board[(int)p1.x, (int)p1.y].Move(p1);

        if (Math.Abs(p2.x - p1.x) <= 0)
        {
            //Debug.Log("Y CASE");
            //This is the y case
            float lowerP = 0;
            if(p2.y > p1.y)
            {
                for (int i = 1; i < (int)Math.Abs(p2.y - p1.y); i++)
                {
                    Vector2 emptyPositions = board[(int)p1.x, (int)p1.y + i].pos;
                    board[(int)p1.x, (int)p1.y  + i] = new EmptyTile(emptyPositions, this);
                }
            }
            else{
                for (int i = 1; i < (int)Math.Abs(p2.y - p1.y); i++)
                {
                    Vector2 emptyPositions = board[(int)p2.x, (int)p2.y + i].pos;
                    board[(int)p2.x, (int)p2.y + i] = new EmptyTile(emptyPositions, this);
                }
            }
        }
        else { 
            if (Math.Abs(p2.y - p1.y) <= 0)
            {
                //Debug.Log("X CASE");
                //This is the x case
                float lowerP = 0;
                if (p2.x > p1.x)
                {
                    for (int i = 1; i < (int)Math.Abs(p2.x - p1.x); i++)
                    {
                        Vector2 emptyPositions = board[(int)p1.x + i, (int)p1.y].pos;
                        board[(int)p1.x + i, (int)p1.y] = new EmptyTile(emptyPositions, this);
                    }
                }
                else
                {
                    for (int i = 1; i < (int)Math.Abs(p2.x - p1.x); i++)
                    {
                        Vector2 emptyPositions = board[(int)p2.x + i, (int)p2.y].pos;
                        board[(int)p2.x + i, (int)p2.y] = new EmptyTile(emptyPositions, this);
                    }
                }
            }
            else
            {
                //Debug.Log("Z CASE");
                //This is the z case
                float lowerP = 0;
                float p1Z = -p1.x - p1.y;
                float p2Z = -p2.x - p2.y;
                if (p2.y > p1.y)
                {
                    //Debug.Log("P1Z Case");
                    for (int i = 1; i < (int)Math.Abs(p2.y - p1.y); i++)
                    {
                        Vector2 emptyPositions = board[(int)p1.x - i, (int)p1.y  + i].pos;
                        board[(int)p1.x - i, (int)p1.y + i] = new EmptyTile(emptyPositions, this);
                    }
                }
                else
                {
                    //Debug.Log("P2Z Case");
                    for (int i = 1; i < (int)Math.Abs(p2.y - p1.y); i++)
                    {
                        Vector2 emptyPositions = board[(int)p2.x - i, (int)p2.y + i].pos;
                        board[(int)p2.x - i, (int)p2.y + i] = new EmptyTile(emptyPositions, this);
                    }
                }
            }
        }
    }

}

