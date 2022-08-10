using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Author: Christopher Cruz
//UI button that controls the weapon switch
public class SwitchButton : MonoBehaviour
{
    public bool hasPressed;
    public Button reloadButton;

    //Pointer Variable and array varaible for weapon switch
    public int weaponId;
    public GameObject[] weapon = new GameObject[3];
    public GameObject HandgunCount;
    public GameObject ArCount;
    public GameObject sgCount;

    void Start()
    {
        Swap(0);
        ArCount.SetActive(false);
        sgCount.SetActive(false);
    }
    void Update()
    {
        if(hasPressed == true)
        {
            if(weaponId >= weapon.Length - 1)
            {
                weaponId = 0;
                hasPressed = false;
            }
            else
            {
                weaponId++;
                hasPressed = false;
            }
        }
        if(weaponId == 0)
        {
            Swap(0);
            HandgunCount.SetActive(true);
            ArCount.SetActive(false);
            sgCount.SetActive(false);
            reloadButton.interactable = true;
        }
        if(weaponId == 1)
        {
            Swap(1);
            HandgunCount.SetActive(false);
            ArCount.SetActive(true);
            sgCount.SetActive(false);
            reloadButton.interactable = false;
        }
        if(weaponId == 2)
        {
            Swap(2);
            HandgunCount.SetActive(false);
            ArCount.SetActive(false);
            sgCount.SetActive(true);
            reloadButton.interactable = false;
        }
    }
    public void Swap(int index)
    {
        for(int i = 0; i < weapon.Length; i++)
        {
            weapon[i].SetActive(false);
        }
        weapon[index].SetActive(true);
        weaponId = index;

    }
    public void Switching()
    {
        hasPressed = true;
    }

}
