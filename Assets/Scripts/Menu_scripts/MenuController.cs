using UnityEngine;
using System.IO;
using System;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    private string path;
    private string save;

    void Start()
    {
        path = Application.persistentDataPath + "/save.txt";
        Debug.Log(path);
        FileInfo fi1 = new FileInfo(path);
        if(fi1.Exists && fi1.Length!=0)
        {
            StreamReader read = new StreamReader(path);
            save = read.ReadLine();
            read.Close();
        }
        else
        {
            StreamWriter writer = new StreamWriter(path);
            string scene = "Level1";
            char[] sceneArray = scene.ToCharArray();
            string output="";
            for(int i=0;i<sceneArray.Length;i++)
                output+=Convert.ToChar(Convert.ToInt16(sceneArray[i]) + 1).ToString();
            writer.WriteLine(output, true);
            save = "Level1";
            writer.Close();
        }
    }
    
    public void Play_game()
    {
        SceneManager.LoadScene("Level1");
    }

    public void Continue()
    {
        path = Application.persistentDataPath + "/save.txt";
        StreamReader read = new StreamReader(path);
        save = read.ReadLine();
        read.Close();
        char[] sceneArray = save.ToCharArray();
        string output="";
        for(int i=0;i<sceneArray.Length;i++)
            output+=Convert.ToChar(Convert.ToInt16(sceneArray[i]) - 1);
        SceneManager.LoadScene(output);
    }

    public void Quit_game()
    {
        Application.Quit();
    }
}
