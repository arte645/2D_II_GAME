using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    private string path;
    private string save;

    void Start()
    {
        path = Application.persistentDataPath + "/save.txt";
        FileInfo fi1 = new FileInfo(path);
        if(fi1.Exists)
        {
            StreamReader read = new StreamReader(path);
            save = read.ReadLine();
            read.Close();
        }
        else
        {
            StreamWriter writer = new StreamWriter(path);
            writer.WriteLine("Level1", true);
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
        SceneManager.LoadScene(save);
    }

    public void Quit_game()
    {
        Application.Quit();
    }
}
