using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Author:  Christopher Cruz
//Used to rotate health and ammo
public class rotateObject : MonoBehaviour
{
    public float rotateSpeed = 25.0f;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.up * rotateSpeed * Time.deltaTime);
    }
}
