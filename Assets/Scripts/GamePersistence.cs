using System.IO;
using UnityEngine;


public class GamePersistence : MonoBehaviour
{
    public static GamePersistence Instance;
    [HideInInspector]
    public string Name;
    [HideInInspector]
    public int Score;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);

        LoadGameData();
    }



    public void SaveGameData()
    {
        try
        {
            // Create data object
            SaveData data = new SaveData(Name, Score);

            // convert to json string
            string json = JsonUtility.ToJson(data);

            // write json to file
            File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
        }
        catch (IOException e)
        {
            Debug.LogError("IO Error saving file: " + e.Message);
            // Handle general IO errors
        }
        finally
        {
            Debug.Log($"Saved Data: {Name} : {Score}");
        }

    }

    public void LoadGameData()
    {
        try
        {
            string path = Path.Combine(Application.persistentDataPath, "savefile.json");
            if (File.Exists(path))
            {
                SaveData data = JsonUtility.FromJson<SaveData>(File.ReadAllText(path));
                this.Name = data.Name;
                this.Score = data.Score;
            }
        }
        catch (IOException e)
        {
            Debug.LogError("IO Error loading file: " + e.Message);
        }
    }

    [System.Serializable]
    class SaveData
    {
        public string Name;
        public int Score;

        public SaveData(string Name, int Score)
        {
            this.Name = Name;
            this.Score = Score;
        }
    }


} // GamePersistence
