using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Author: Christopher Cruz
//Used to animates canvas to flash white whenever the player shoots to illustrate a muzzle flashing from a gun.

[RequireComponent(typeof(Image))]

public class MuzzleFlash : MonoBehaviour
{
    Image _image = null;
    Coroutine _currentFlashRoutine = null;
    
    private void Awake()
    {
        _image = GetComponent<Image>();
    }
    public void StartFlash(float secondsForOneFlash, float maxAplha, Color newColor)
    {
        _image = GetComponent<Image>();
        maxAplha = Mathf.Clamp(maxAplha, 0, 1);
        if(_currentFlashRoutine != null)
        {
            StopCoroutine(_currentFlashRoutine);
        }
        _currentFlashRoutine = StartCoroutine(Flash(secondsForOneFlash, maxAplha));
    }
    
    IEnumerator Flash(float secondsForOneFlash, float maxAplha)
    {
        float flashInDuration = secondsForOneFlash / 2;
        for(float t = 0; t <= flashInDuration; t += Time.deltaTime)
        {
            Color colorThisFrame = _image.color;
            colorThisFrame.a = Mathf.Lerp(0, maxAplha, t / flashInDuration);
            _image.color = colorThisFrame;
            yield return null;
        }

        float flashOutDuration = secondsForOneFlash / 2;
        for(float t = 0; t <= flashOutDuration; t += Time.deltaTime)
        {
            Color colorThisFrame = _image.color;
            colorThisFrame.a = Mathf.Lerp(maxAplha, 0, t / flashOutDuration);
            _image.color = colorThisFrame;
            yield return null;
        }
        //returns flash values back to white and alpha to zero after
        _image.color = new Color32(255, 255, 255 ,0);
    }
}
