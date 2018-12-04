public class TileScore
{
    public TileTypes Type { get; private set; }
    public readonly int score;
    public TileScore(TileTypes type, int tileScore)
    {
        this.Type = type;
        this.score = tileScore;
    }
}

