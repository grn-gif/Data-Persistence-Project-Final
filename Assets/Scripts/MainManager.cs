using UnityEngine;
using UnityEngine.UI;

public class MainManager : MonoBehaviour//Beta Folder Script
{
    public Brick BrickPrefab;
    public int LineCount = 6;
    public Rigidbody Ball;

    public Text ScoreText;
    public Text HighscoreText;
    public GameObject GameOverText;
    
    private bool m_Started = false;
    private int m_Points;
    
    private bool m_GameOver = false;

    void Start()
    {
        ScoreText.text = $"{GameManager.GM.GetName()} : Score : {m_Points}";
        SetHighestScore();

        const float step = 0.6f;
        int perLine = Mathf.FloorToInt(4.0f / step);
        
        int[] pointCountArray = new [] {1,1,2,2,5,5};
        for (int i = 0; i < LineCount; ++i)
        {
            for (int x = 0; x < perLine; ++x)
            {
                Vector3 position = new Vector3(-1.5f + step * x, i * 0.3f, 10);
                var brick = Instantiate(BrickPrefab, position, Quaternion.identity, GameObject.Find("Bricks").transform);
                brick.PointValue = pointCountArray[i];
                brick.onDestroyed.AddListener(AddPoint);
            }
        }
    }

    private void Update()
    {
        if (!m_Started)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                m_Started = true;
                float randomDirection = Random.Range(-1.0f, 1.0f);
                Vector3 forceDir = new Vector3(randomDirection, 1, 0);
                forceDir.Normalize();

                Ball.transform.SetParent(null);
                Ball.AddForce(forceDir * 2.0f, ForceMode.VelocityChange);
            }
        }
        else if (m_GameOver)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                ScenesManager.SM.LoadScene(0);
            }
        }
    }

    void AddPoint(int point)
    {
        m_Points += point;
        ScoreText.text = $"{GameManager.GM.GetName()} : Score : {m_Points}";
    }

    public void GameOver()
    {
        if (m_Points > HighscoreManager.instance.GetLowestScore())
        {
            HighscoreManager.instance.AddHighscoreEntry(GameManager.GM.GetName(), m_Points);
            HighscoreManager.instance.SortHighscores();
            SetHighestScore();
            HighscoreManager.instance.SaveHighscores();
        }
        m_GameOver = true;
        GameOverText.SetActive(true);
    }

    public void SetHighestScore()
    {
        // Get the highest score and name
        int highestScore;
        string highestScoreName;
        HighscoreManager.instance.GetHighestScore(out highestScore, out highestScoreName);

        // Update the Text component with the highest score and name
        HighscoreText.text = highestScoreName + " : " + highestScore;
    }
}
