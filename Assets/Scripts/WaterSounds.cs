using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterSounds : MonoBehaviour
{
    // Start is called before the first frame update
    public AudioSource watersound;
    public AudioSource overworld;
    public AudioSource challenge;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            watersound.Play();
            overworld.volume = overworld.volume / 2;
            challenge.volume = challenge.volume / 2;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "Player")
        {
            watersound.Stop();
            overworld.volume = overworld.volume * 2;
            challenge.volume = challenge.volume * 2;
        }
    }
}
