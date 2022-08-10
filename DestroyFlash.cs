using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Author:Christopher Cruz
//destroys prefab after time
public class DestroyFlash : MonoBehaviour
{
    public GameObject destroyFlash;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(destroyFlash, .5f);
    }
}
