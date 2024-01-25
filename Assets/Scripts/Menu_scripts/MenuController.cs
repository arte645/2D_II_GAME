using System;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    private string path;
    private string save;

    public void Play_game()
    {
        SceneManager.LoadScene("Level1");
    }

    public void Continue()
    {
        path = Application.persistentDataPath + "/save.txt";
        var read = new StreamReader(path);
        save = read.ReadLine();
        read.Close();
        var sceneArray = save.ToCharArray();
        var output = "";
        for (var i = 0; i < sceneArray.Length; i++)
        {
            output += Convert.ToChar(Convert.ToInt16(sceneArray[i]) - 1);
        }

        SceneManager.LoadScene(output);
    }

    public void Quit_game()
    {
        Application.Quit();
    }
}
