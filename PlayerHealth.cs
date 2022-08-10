using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{

    public Slider healthBar;
    public float currentHealth = 100f;
    public float maxHealth = 100f;
    public SceneManager myManager;
    //Variables used for gameover UI and health Restoring
    private ShootingScript healCheck;
    private EndLevel deadCheck;

    //modified by:Christopher Cruz
    void Start()
    {
        
    }
    //checks if player has recieved a health pack and updates the Health IU
    void Update()
    {
        healCheck = GameObject.FindGameObjectWithTag("Player").GetComponent<ShootingScript>();
        healthBar.value = currentHealth / maxHealth;
        if(healCheck.hasHealed == true)
        {
            Heal(currentHealth);
            //after player heals it sets the bool back to false to prevent contineous healing
            healCheck.hasHealed = false;
        }
    }
    //Checks if player is at zero and when it does it calls the GameOver function in the EndLevel script to display the UI
    public void TakeDamage(float damageAmount)
    {
        currentHealth -= damageAmount;
        if(currentHealth <= 0)
        {
            deadCheck = GameObject.FindGameObjectWithTag("Finishline").GetComponent<EndLevel>();
            deadCheck.GameOver();
            //replaced old code since we have a Game over UI
            //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);   
        } 
    }

    //Modified code to ensure health pack restores the health and doesnt go over the max
    public void Heal(float heal)
    {
        heal = 25f;
        currentHealth += heal;
        if(currentHealth > 100)
        {
            currentHealth = maxHealth;
        }
        Debug.Log("Players Health:" +currentHealth);
    }

}
