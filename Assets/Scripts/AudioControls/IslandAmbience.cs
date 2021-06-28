using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IslandAmbience : MonoBehaviour
{
    [SerializeField] private AudioSource m_ambientWind;
    [SerializeField] private AudioSource m_ambientBird;
    [SerializeField] private AudioSource[] m_ambientBugs;

    //Start the ambient as you enter
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            m_ambientWind.Play();
        }
    }

    //Play the ambient sounds as the player stays in the area
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            //Randomly play a ambience noise
            foreach (AudioSource a in m_ambientBugs)
            {
                if (a.isPlaying)
                {
                    return;
                }
            }

            int noise = Random.Range(0, m_ambientBugs.Length - 1);

            m_ambientBugs[noise].Play();
        }
    }

    //Turn em all off when you leave
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            //Randomly play a ambience noise
            foreach (AudioSource a in m_ambientBugs)
            {
                a.Stop();
            }
        }
    }
}
