using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadToMenuManager : MonoBehaviour
{
    public void LoadToMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}
