using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
    public GameObject enemy;
    public Transform[] enemyPosition;
    BoxCollider trigger;

    // Start is called before the first frame update
    void Start()
    {
        trigger = GetComponent<BoxCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            EnemySpawner();
            trigger.enabled = false;
        }
    }
    void EnemySpawner()
    {
        foreach(var sp in enemyPosition)
        {
            Instantiate(enemy, sp.position, sp.rotation);
        }
    }
}
