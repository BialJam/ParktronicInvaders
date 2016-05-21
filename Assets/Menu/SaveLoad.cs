using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveLoad
{
    public static List<Score> scoresSaves = new List<Score>();

    public static void Load()
    {
        if (File.Exists(Application.persistentDataPath + "/savedGames.gd"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/savedGames.gd", FileMode.Open);
            scoresSaves = (List<Score>)bf.Deserialize(file);
            file.Close();
        }
    }

    public static void Save()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/savedGames.gd");
        bf.Serialize(file, scoresSaves);
        file.Close();
    }

    public static void AddScore(Score score)
    {
        scoresSaves.Add(score);
        scoresSaves.Sort();

        if (scoresSaves.Count > 3)
        {
            scoresSaves.RemoveAt(scoresSaves.Count - 1);
        }

        Save();
    }
}
[System.Serializable]
public class Score
{
    public int scorePoints;
    public string playerName;
}