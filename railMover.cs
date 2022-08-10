using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Author: Christopher Cruz
public class railMover : MonoBehaviour
{
    public RailSystem rail;
    public PlayMode mode;

    public float railSpeed = 2.5f;
    public bool isBackwards;
    public bool isLooping;
    public bool isBouncing;
    public bool isPaused;

    private int currentSeg;
    private float transition;
    private bool isDone;
    private ShootingScript enemyCheck;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        enemyCheck = GameObject.FindGameObjectWithTag("Player").GetComponent<ShootingScript>();
        
        if(enemyCheck.noEnemies == true)
        {
            isPaused = false;
        }
        if(enemyCheck.noEnemies == false)
        {
            isPaused = true;
        }


        if(!rail)
        {
            return;
        }
        if(!isDone)
        {
            Play(!isBackwards);
        }
    }
    //old code for encounter trigger
    /*
    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Encounter")
        {
            isPaused = true;
            Destroy(other.gameObject);
        }
    }
    void OnTriggerStay(Collider other)
    {
        if(other.gameObject.tag == "Exit"&& enemyCheck.noEnemies == true)
        {
            enemyCheck.noEnemies = false;
            isPaused = false;
            Destroy(other.gameObject);
        }
    }
    */
    private void Play(bool forward = true)
    {
        float m = (rail.nodes[currentSeg + 1].position - rail.nodes[currentSeg].position).magnitude;
        float s = (Time.deltaTime * 1 / m) * railSpeed;
        transition += (forward) ? s : -s;
        if(isPaused == false)
        {
            if(transition > 1)
            {
                transition = 0;
                currentSeg++;
                if(currentSeg == rail.nodes.Length - 1)
                {
                    if(isLooping)
                    {
                        if(isBouncing)
                        {
                            transition = 1;
                            currentSeg = 0;
                            isBackwards = !isBackwards;
                        }
                        else
                        {
                            currentSeg = rail.nodes.Length -2;
                        }
                    }
                    else
                    {
                        isDone = true;
                        return;
                    }
                }
            }
            transform.position = rail.positionOnRail(currentSeg, transition, mode);
            transform.rotation = rail.rRotation(currentSeg, transition);
        }
    }
}
