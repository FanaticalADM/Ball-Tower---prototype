using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class DataSaveLoad : MonoBehaviour
{
    private static DataSaveLoad instance;
    public static DataSaveLoad Instance;

    private void Awake()
    {
        instance = this;
    }

    public void SaveFile()
    {
        string destination = Application.persistentDataPath + "/HighScore_save.dat";
        FileStream file;

        if (File.Exists(destination)) file = File.OpenWrite(destination);
        else file = File.Create(destination);

        BinaryFormatter bf = new BinaryFormatter();
        
        bf.Serialize(file,ScoreManager.Instance.HighScore);
        file.Close();
    }

    public void LoadFile()
    {
        string destination = Application.persistentDataPath + "/HighScore_save.dat";
        FileStream file;

        if (File.Exists(destination)) file = File.OpenRead(destination);
        else
        {
            Debug.LogError("File not found");
            return;
        }

        BinaryFormatter bf = new BinaryFormatter();
        float data = (float)bf.Deserialize(file);
        file.Close();

        ScoreManager.Instance.HighScore = data;
    }
}
