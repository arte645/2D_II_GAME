using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveController : MonoBehaviour
{
    void Start()
    {
        string path = Application.persistentDataPath + "/save.txt";
        
        StreamWriter writer = new StreamWriter(path);
        string scene = SceneManager.GetActiveScene().name.ToString();
        string output="";

        char[] sceneArray = scene.ToCharArray();
        for(int i=0;i<sceneArray.Length;i++)
            output+=Convert.ToChar(Convert.ToInt16(sceneArray[i]) + 1).ToString();
            Debug.Log(output);
        writer.WriteLine(output);
    }
}
