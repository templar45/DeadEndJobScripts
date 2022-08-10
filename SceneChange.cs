using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

//Author:Christopher Cruz
public class SceneChange : MonoBehaviour
{
    //simple script that used to change my game scene and main menu
    public string thisLevel;
    void Start()
    {
        //sets the string varaible to return or get a string name in the scene manager
        thisLevel = SceneManager.GetActiveScene().name;

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void Restart()
    {
        //gets the current string name of the scene and reloads it
        /*SceneManager.LoadScene(thisLevel);*/
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void LoadLevel(string levelName)
    {
        //used to pick a scene to load by typing it in the inspector since I set the parameters of levelname to a string
        SceneManager.LoadScene(levelName);

    }

    public void TitleScreen()
    {
        //loads scene with the string name startmenu
        SceneManager.LoadScene("MainMenu");

    }
}
