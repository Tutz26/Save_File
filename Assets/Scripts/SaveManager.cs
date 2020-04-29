using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization.Json;

// [System.Serializable]
// public class Stats {
//     public int level;

//     public Stats(int level) {
//         this.level = level;
//     }
// }

[System.Serializable]
public class GameData {
    public int level;

    // public GameData(int level) {
    //     this.level = level;
    // }
    public GameData() {

    }
}

public class SaveManager : MonoBehaviour
{
    GameData gameData;

    public GameObject levelText;

    Text levelTextComponent;

    


    // Start is called before the first frame update
    void Start()
    {
        gameData = new GameData();
        gameData.level = 1;

        levelTextComponent = levelText.GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlusOne() {
        Debug.Log("+1");
        gameData.level++;

        levelTextComponent.text = gameData.level.ToString();
    }

    public void Save() {

        string fileLocation = Application.persistentDataPath + "/savefile.json";
        FileStream file;

        //Revisar si el archivo ya existe
        if (File.Exists(fileLocation))
        {
            //El archivo ya existe, abrirlo
            file = File.OpenWrite(fileLocation);
        } else {
            //El archivo no existe, crearlo
            file = File.Create(fileLocation);
        }

        // BinaryFormatter binaryFormatter = new BinaryFormatter();
        // binaryFormatter.Serialize(file, gameData);

        DataContractJsonSerializer jsonSerializer = new DataContractJsonSerializer(typeof(GameData));
        jsonSerializer.WriteObject(file, gameData);
        
        file.Close();
        Debug.Log("Saved to location: " + fileLocation);
    }

    public void Load() {
        string fileLocation = Application.persistentDataPath + "/savefile.json";
        FileStream file;

        if (File.Exists(fileLocation)) {
            //El archivo ya existe, leerlo
            file = File.OpenRead(fileLocation);
        } else {
            //El archivo no existe, error
            Debug.LogError("File not found: " + fileLocation);
            return;
        }

        // BinaryFormatter binaryFormatter = new BinaryFormatter();
        // gameData = (GameData) binaryFormatter.Deserialize(file);

        DataContractJsonSerializer jsonSerializer = new DataContractJsonSerializer(typeof(GameData));
        gameData = (GameData) jsonSerializer.ReadObject(file);


        file.Close();
        Debug.Log("Loaded file: " + fileLocation);

        levelTextComponent.text = gameData.level.ToString();
    }
}