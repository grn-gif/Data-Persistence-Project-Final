using System.IO;
using UnityEngine;

public class GameManager : MonoBehaviour//Beta Script Folder
{
    public static GameManager GM { get; private set; }

    private string playerName;

    [System.Serializable]
    class SaveData
    {
        public string playerName;
    }

    public void FlushName()
    {
        File.WriteAllText(Application.persistentDataPath + "current-name.json", "{}");
        LoadName();
    }

    public void SaveName()
    {
        SaveData data = new SaveData();
        data.playerName = playerName;

        string json = JsonUtility.ToJson(data);
        File.WriteAllText(Application.persistentDataPath + "current-name.json", json);
    }

    public void LoadName()
    {
        string path = Application.persistentDataPath + "current-name.json";

        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            playerName = data.playerName;
        }
    }

    public void SetName(string name) => playerName = name;

    public string GetName() { return playerName; }

    private void Awake()
    {
        if (GM != null)
        {
            Destroy(gameObject);
            return;
        }

        GM = this;
        DontDestroyOnLoad(gameObject);
        LoadName();
    }
}
