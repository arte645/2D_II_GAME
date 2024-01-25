using System;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveController : MonoBehaviour
{
    public void UpdateSave()
    {
        var path = Application.persistentDataPath + "/save.txt";

        var writer = new StreamWriter(path);
        var scene = SceneManager.GetActiveScene().name.ToString();
        var output = "";

        var sceneArray = scene.ToCharArray();
        for (var i = 0; i < sceneArray.Length; i++)
        {
            output += Convert.ToChar(Convert.ToInt16(sceneArray[i]) + 1).ToString();
        }

        Debug.Log(output);
        writer.WriteLine(output, true);
    }
}
