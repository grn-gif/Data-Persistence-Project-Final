using System.Collections.Generic;
using UnityEngine;
using System.IO;

// The HighscoreManager class is responsible for sorting, saving, loading, and resetting the highscores
public class HighscoreManager : MonoBehaviour
{
    // A list of highscore entries
    public List<HighscoreEntry> highscores = new List<HighscoreEntry>();

    // The maximum number of highscores to display
    public int maxEntries = 10;

    // The name of the file to save the highscores to
    public string saveFileName = "highscores.json";

    // The static instance of the HighscoreManager
    public static HighscoreManager instance;

    void Awake()
    {
        // If there is no instance, set this as the instance
        if (instance == null)
        {
            instance = this;
        }
        // If there is already an instance, destroy this object
        else if (instance != this)
        {
            Destroy(gameObject);
        }

        // Don't destroy the object when changing scenes
        DontDestroyOnLoad(gameObject);
        LoadHighscores();
    }

    // A method to sort the highscores by score
    public void SortHighscores()
    {
        highscores.Sort((x, y) => y.score.CompareTo(x.score));
    }

    // A method to save the highscores to a JSON file
    public void SaveHighscores()
    {
        string jsonString = JsonUtility.ToJson(this, true);
        File.WriteAllText(saveFileName, jsonString);
    }

    // A method to load the highscores from a JSON file
    public void LoadHighscores()
    {
        if (File.Exists(saveFileName))
        {
            string jsonString = File.ReadAllText(saveFileName);
            JsonUtility.FromJsonOverwrite(jsonString, this);
        }
        else
        {
            highscores = new List<HighscoreEntry>()
            {
                new HighscoreEntry { name = "Alice", score = 5 },
                new HighscoreEntry { name = "Bob", score = 4 },
                new HighscoreEntry { name = "Charlie", score = 3 },
                new HighscoreEntry { name = "Dave", score = 2 },
                new HighscoreEntry { name = "Eve", score = 1 },
            };
        }
    }

    // A method to reset the highscores to the default values
    public void ResetHighscores()
    {
        highscores.Clear();
        highscores = new List<HighscoreEntry>()
        {
            new HighscoreEntry { name = "Alice", score = 5 },
            new HighscoreEntry { name = "Bob", score = 4 },
            new HighscoreEntry { name = "Charlie", score = 3 },
            new HighscoreEntry { name = "Dave", score = 2 },
            new HighscoreEntry { name = "Eve", score = 1 },
        };
        SaveHighscores();
        LoadHighscores();
    }

    // A method to create a new highscore entry
    public void AddHighscoreEntry(string name, int score)
    {
        HighscoreEntry entry = new HighscoreEntry
        {
            name = name,
            score = score
        };
        highscores.Add(entry);
    }

    // A function to get the highest score and name
    public void GetHighestScore(out int highestScore, out string highestScoreName)
    {
        highestScore = 0;
        highestScoreName = "";

        // Loop through the highscores and find the highest score
        foreach (HighscoreEntry entry in highscores)
        {
            if (entry.score > highestScore)
            {
                highestScore = entry.score;
                highestScoreName = entry.name;
            }
        }
    }

    // A function to get the highest score
    public int GetHighestScore()
    {
        int highestScore = 0;

        // Loop through the highscores and find the highest score
        foreach (HighscoreEntry entry in highscores)
        {
            if (entry.score > highestScore)
            {
                highestScore = entry.score;
            }
        }

        return highestScore;
    }

    // A method to get the lowest score
    public int GetLowestScore()
    {
        int lowestScore = int.MaxValue;

        // Loop through the highscores and find the lowest score
        foreach (HighscoreEntry entry in highscores)
        {
            if (entry.score < lowestScore)
            {
                lowestScore = entry.score;
            }
        }

        return lowestScore;
    }
}