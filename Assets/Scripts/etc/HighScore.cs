using System;

[Serializable]
public class HighScore
{
    public long score;
    public long endlessScore;

    public HighScore(long score, long endlessScore)
    {
        this.score = score;
        this.endlessScore = endlessScore;
    }
}
