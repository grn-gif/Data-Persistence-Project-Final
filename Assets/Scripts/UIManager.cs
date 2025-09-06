using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public InputField InputName;
    public Text HighscoreText;

    public void FlushCurSession()
    {
        HighscoreManager.instance.ResetHighscores();
        GameManager.GM.FlushName();
    }

    public void SetCurPlayerName()
    {
        GameManager.GM.SetName(InputName.text);
        GameManager.GM.SaveName();
    }

    public void StartNewGame(int sceneId)
    {
        if (InputName.text != "")
            ScenesManager.SM.LoadScene(sceneId);
        else
        {
            InputName.text = "Player";
            GameManager.GM.SetName(InputName.text);
            GameManager.GM.SaveName();
            ScenesManager.SM.LoadScene(sceneId);
        }
    }

    public void ExitGame()
    {
        ScenesManager.SM.ExitGame();
        GameManager.GM.SaveName();
        HighscoreManager.instance.SaveHighscores();
    }

    public void UpdateUI()
    {
        GameManager.GM.LoadName();
        InputName.text = GameManager.GM.GetName();

        // Get the highest score and name
        int highestScore;
        string highestScoreName;
        HighscoreManager.instance.GetHighestScore(out highestScore, out highestScoreName);

        // Update the Text component with the highest score and name
        HighscoreText.text = highestScoreName + " : " + highestScore;
    }

    public void SortHighscores()
    {
        HighscoreManager.instance.SortHighscores();
        HighscoreManager.instance.SaveHighscores();
    }

    private void Start() => UpdateUI();
}
