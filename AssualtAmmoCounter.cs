using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Author: Christopher Cruz
public class AssualtAmmoCounter : MonoBehaviour
{
    public Text arAmmoCount;
    public ShootingScript checkPlayer;

    void Update()
    {
        arAmmoCount.text = checkPlayer.currentARrounds + "";
    }
}
