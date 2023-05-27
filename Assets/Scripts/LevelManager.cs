using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public string sceneName;

    private Pellet[] No_Of_Pellets;

   public void Home_Button(string mainScreen)
    {
        SceneManager.LoadScene(mainScreen);

    }
    public void Next_Button(string sceneName)
    {
        SceneManager.LoadScene(sceneName);

    }

    void OnEnable()
    {
        No_Of_Pellets = FindObjectsOfType<Pellet>();
    }

    void Update()
    {
        foreach (Pellet Pellets in No_Of_Pellets)
        {
            if (Pellets != null)
            {
                return;
            }
        }
        SceneManager.LoadScene(sceneName);
    }
}