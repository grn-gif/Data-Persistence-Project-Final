using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighscoreUIManager : MonoBehaviour
{
    // A list of Text components to display the highscores
    public List<Text> highscoreTexts;

    void Start()
    {
        // Load the highscores from a JSON file
        HighscoreManager.instance.LoadHighscores();

        // Loop through the highscores and display them in the Text components
        for (int i = 0; i < Mathf.Min(HighscoreManager.instance.highscores.Count, HighscoreManager.instance.maxEntries); i++)
        {
            HighscoreEntry entry = HighscoreManager.instance.highscores[i];
            Text text = highscoreTexts[i];
            text.text = entry.name + ": " + entry.score;
        }
    }

    public void ReturnHome()
        {
            ScenesManager.SM.LoadScene(0);
        }
}