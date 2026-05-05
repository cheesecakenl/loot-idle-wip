using System.IO;
using UnityEngine;

public class SaveManager : MonoBehaviour, ISaveSystem
{
    public static SaveManager instance = null;

    private string savePath;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);

        savePath = Application.persistentDataPath + "/upgrade-tree-save.json";
    }

    public void Save(SaveData data)
    {
        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(savePath, json);
    }

    public SaveData Load()
    {
        if (File.Exists(savePath))
        {
            string json = File.ReadAllText(savePath);

            SaveData data = JsonUtility.FromJson<SaveData>(json);

            if (data == null) return null;

            return data;
        }

        return null;
    }
}