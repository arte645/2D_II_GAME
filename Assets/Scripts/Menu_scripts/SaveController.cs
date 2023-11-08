using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveController : MonoBehaviour
{
    void Start()
    {
        string path = Application.persistentDataPath + "/save.gamesave";
        
        StreamWriter writer = new StreamWriter(path);
        writer.Write(SceneManager.GetActiveScene().name);
    }
}
