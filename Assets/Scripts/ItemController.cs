using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemController : MonoBehaviour
{
    public AudioClip soundClip = null;

    private AudioSource audio;

    private void Start() {
        audio = GetComponent<AudioSource>();
        if(audio && soundClip)
            audio.PlayOneShot(soundClip);
    }

    private void KillObject()
    {
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        var isMario = other.gameObject.CompareTag("Mario");
       
        if(isMario)
        {
            if(gameObject.tag == "Coin")
            {
                CounterManager.addScore(100);
                CounterManager.addCoin();
            } else if(gameObject.tag == "MushroomItem")
            {
                CounterManager.addScore(300);
            }
            
            KillObject();
        }
    }
}
