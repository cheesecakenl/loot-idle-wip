using System.IO;
using UnityEngine;

public class SaveManager
{
    private static string savePath = Application.persistentDataPath + "/upgrade-tree-save.json";

    public static void Save(SaveData data)
    {
        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(savePath, json);
    }

    public static SaveData Load()
    {
        if (File.Exists(savePath))
        {
            string json = File.ReadAllText(savePath);
            return JsonUtility.FromJson<SaveData>(json);
        }

        return null;
    }
}