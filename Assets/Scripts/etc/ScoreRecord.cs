using System.IO;
using UnityEngine;

using System;



public class HighScoreManager : Singleton<HighScoreManager>
{
    private string filePath;

    void Awake()
    {
        filePath = Application.persistentDataPath + "/highscore.json";
        Debug.Log(filePath);
    }

    public void SaveHighScore(long score, long endlessScore)
    {
        HighScore highScore = new HighScore(score, endlessScore);
        string json = JsonUtility.ToJson(highScore);
        File.WriteAllText(filePath, json);
    }

    public long[] LoadHighScore()
    {   
        if (File.Exists(Path.GetFullPath(filePath)))
        {
            string json = File.ReadAllText(Path.GetFullPath(filePath));
            HighScore highScore = JsonUtility.FromJson<HighScore>(json);
            return new long[] {highScore.score, highScore.endlessScore};
        }
        else
        {
            return new long[] {0, 0};
        }
    }
}
