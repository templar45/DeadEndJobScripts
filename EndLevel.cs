using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Author: Christopher Cruz
public class EndLevel : MonoBehaviour
{
    //variables used for to scale the canvas up and down
    private GameObject mainMenu;
    private GameObject backGround;
    private GameObject gameOver;
    private GameObject restartLevel;
    private GameObject myPlayer;
    //public EndLevel myfinishLine;
    private  GameObject CanvasObject;
    private GameObject nextLevel;
    public Button shootButton;
    public Button reloadButton;
    public Image reticle;
    void Start()
    {
        //sets varaibles to gameobjects with name
        restartLevel = GameObject.Find("Restart");
        mainMenu = GameObject.Find("MainMenu");
        backGround = GameObject.Find("LevelComplete");
        gameOver = GameObject.Find("GameOver");
        nextLevel = GameObject.Find("NextLevel");

        //finds gameoject with tag and sets it to corresponding variables
        //myPlayer = GameObject.FindGameObjectWithTag("Player");
        CanvasObject = GameObject.FindGameObjectWithTag("Canvas");

        //set the intial vector scale to zero and UI only appears when needed
        gameOver.transform.localScale = new Vector3(0, 0, 0);
        restartLevel.transform.localScale = new Vector3(0, 0, 0);
        mainMenu.transform.localScale = new Vector3(0, 0, 0);
        backGround.transform.localScale = new Vector3(0, 0, 0);
        nextLevel.transform.localScale = new Vector3(0, 0, 0);
    }

    //called when player hits this gameobject
    public void LevelComplete()
    {
        //UI appaears or scaled up
        restartLevel.transform.localScale = new Vector3(1, 1, 1);
        mainMenu.transform.localScale = new Vector3(1, 1, 1);
        backGround.transform.localScale = new Vector3(2.5f, 2.5f, 2.5f);
        nextLevel.transform.localScale = new Vector3(1, 1, 1);
        //disables button functionality 
        reloadButton.interactable = false;
        shootButton.interactable = false;
        //disables reticle
        reticle.enabled = false;
        //freezes scene
        Time.timeScale = 0;
        
    }
    //called when the playerhealth script reached zero
    public void GameOver()
    {
        gameOver.transform.localScale = new Vector3(2.5f, 2.5f, 2.5f);
        restartLevel.transform.localScale = new Vector3(1, 1, 1);
        mainMenu.transform.localScale = new Vector3(1, 1, 1);
        //disables button functtionality
        reloadButton.interactable = false;
        shootButton.interactable = false;
        //disables reticle
        reticle.enabled = false;
        //freezes scene
        Time.timeScale = 0;
    }

    //when restart or main menu is selected the the UI scales are set to disappear
    public void Exit()
    {
        //scale is set back to zero when restarting or going back to main menu
        mainMenu.transform.localScale = new Vector3(0, 0, 0);
        gameOver.transform.localScale = new Vector3(0, 0, 0);
        restartLevel.transform.localScale = new Vector3(0, 0, 0);
        backGround.transform.localScale = new Vector3(0, 0, 0);
        nextLevel.transform.localScale = new Vector3(0, 0, 0);
        //stores time when player reloads scene, chooses next level, or goes to menu
        Time.timeScale = 1;
    }

        private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            LevelComplete();
        }
    }
}
