using System;
using UnityEngine;
using UnityEngine.UI;

public class DisplayManager : MonoBehaviour
{
    public int[] maxmovesarray;
    public int maxMoves { get { return maxmovesarray[game.curBoard]; } }
    private int swaps;
    private TileManager game;
    [System.Serializable]
    public class SpriteSet
    {
        public Sprite normalSprite;
        public Sprite highlightSprite;
        public Sprite selectedSprite;
    }
    public SpriteSet[] sprites;
    //public Sprite[] highlightSprites;
    public GameObject TilePrefab, BlankPrefab;
    public TextAsset boardFile;
    public GameObject[] infoPanels;
    public String scoreString;
    public Text scoreText;
    public String movesString;
    public Text movesText;
    // Use this for initialization
    void Start()
    {
        if (sprites.Length != System.Enum.GetValues(typeof(TileTypes)).Length)
            throw new System.MissingFieldException("sprites must have exactly one value for each tile type. Expected " + System.Enum.GetValues(typeof(TileTypes)).Length + " found " + sprites.Length);
        /*if (highlightSprites.Length != System.Enum.GetValues(typeof(TileTypes)).Length)
            throw new System.MissingFieldException("highlightSprites must have exactly one value for each tile type. Expected " + System.Enum.GetValues(typeof(TileTypes)).Length + " found " + highlightSprites.Length);
    */    
        game = new TileManager(boardFile);
        swaps = maxMoves;
        //gameOverMenu.SetActive(false);
        UpdateTiles();
    }
    public bool updateEachFrame = false;
    private void Update()
    {
        if (updateEachFrame)
            UpdateTiles();
    }

    public int height, width;

    private void UpdateTiles()
    {
        foreach (Transform child in transform)
            GameObject.Destroy(child.gameObject);
        foreach (GameObject panel in infoPanels)
        {
            if (panel != null)
                panel.SetActive(false);
        }
        int score = 0;   
        TileScore[,] board = game.GetBoard();
        for (int i = 0; i < board.GetLength(0); ++i)
        {
            for (int j = 0; j < board.GetLength(1); ++j)
            {
                int y = i - (board.GetLength(0) / 2);
                int x = j - (board.GetLength(1) / 2);
                //TODO: If possible this should auto-resize the grid cells and adjust the width and height in order to make it work for dynamic grids.
                TileScore tile = board[i, j];
                GameObject tileGM;
                if (tile.Type != TileTypes.EMPTY && tile.Type != TileTypes.WALL)
                {
                    
                    tileGM = Instantiate(TilePrefab, this.transform);
                    Button tileButton = tileGM.GetComponent<Button>();
                    tileGM.GetComponentInChildren<Text>().text = tile.score.ToString();
                    SpriteState temp = tileButton.spriteState;
                    temp.highlightedSprite = sprites[(int)tile.Type].highlightSprite;
                    temp.disabledSprite = sprites[(int)tile.Type].selectedSprite;
                    tileButton.spriteState = temp;

                    infoPanels[(int)tile.Type].SetActive(true);

                    Vector2 holdij = new Vector2(i, j);
                    tileButton.onClick.AddListener(() => this.SelectTile(holdij, tileGM));
                }
                else
                {
                    tileGM = Instantiate(BlankPrefab, this.transform);
                }
				tileGM.name = tile.Type.ToString();
                tileGM.GetComponent<Image>().sprite = sprites[(int)tile.Type].normalSprite;
                tileGM.GetComponent<RectTransform>().localPosition = new Vector3((((x * width)) + (.5f * width * y)), (y * height), 0);
                score += tile.score;
            }
        }
        scoreText.text = scoreString + score.ToString();
        movesText.text = movesString + "\n" + swaps.ToString();
        

        gameOverMenu.SetActive(swaps == 0);
		this.GetComponent<AudioSource>().Play();
        //if (gameOverMenu.activeSelf)
        gameOverText.text = gameOverString + score.ToString();
    }

    private GameObject selectedButton;
    private Vector2 selectedPos;
    public GameObject gameOverMenu;
    public String gameOverString;
    public Text gameOverText;
    private void SelectTile(Vector2 pos, GameObject tile)
    {
        //Debug.Log("selected tile" + tile);
        //Debug.Log(selectedButton);
        if (selectedButton != null )
        {            
            if (ValidSelection(selectedPos, pos) && swaps > 0)
            { 
                game.Swap(selectedPos, pos);
				this.gameObject.GetComponent<AudioSource>().Play();
				//Debug.Log("Swap tiles!");
                this.swaps -= 1;
                
                this.UpdateTiles();
            }
            else
            {
                //Debug.Log("Cannot swap these tiles");
                SelectTile(selectedButton);
            }
        }
        else
        {
            SelectTile(tile);

            selectedButton = tile;

            selectedPos = pos;
        }
    }

    private void SelectTile(GameObject tile)
    {
        Button tileButton = tile.GetComponent<Button>();
        Image tileImage = tile.GetComponent<Image>();
        SpriteState temp = tileButton.spriteState;
        Sprite tempS = tileImage.sprite;
        tileImage.sprite = temp.disabledSprite;
        temp.disabledSprite = tempS;
        tileButton.spriteState = temp;
    }


    private float posZ(Vector2 pos)
    {
        return -pos.x - pos.y;
    }
    private bool ValidSelection(Vector2 selectedPos, Vector2 newPos)
    {
        return (selectedPos != newPos && (selectedPos.y == newPos.y || selectedPos.x == newPos.x || posZ(selectedPos) == posZ(newPos)));
    }


    public void nextBoard()
    {
        if (game.curBoard < game.boardCount-1)
        {
            game.loadBoard(game.curBoard + 1);
            this.swaps = maxMoves;
            this.UpdateTiles();
        }
    }
    public void prevBoard()
    {
        if (game.curBoard >0)
        {
            game.loadBoard(game.curBoard - 1);
            this.swaps = maxMoves;

            this.UpdateTiles();
        }
    }
    public void resetBoard()
    {
        game.loadBoard(game.curBoard);
        this.swaps = maxMoves;

        this.UpdateTiles();
    }
}
