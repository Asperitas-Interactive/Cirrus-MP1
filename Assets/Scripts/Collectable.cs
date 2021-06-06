using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class Collectable : MonoBehaviour
{
    // Start is called before the first frame update
    public CollectableCount UIImage;
    public AudioSource collectSound;
    bool disappear = false;
    float timer = 0.0f;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        if(disappear && timer < 0.0f)
        {
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            UIImage.onCollection();
            collectSound.Play();
            transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.SetActive(false);
            transform.GetChild(0).gameObject.transform.GetChild(1).gameObject.transform.GetChild(1).gameObject.GetComponent<ParticleSystem>().Play();
            transform.GetChild(0).gameObject.transform.GetChild(1).gameObject.transform.GetChild(0).gameObject.GetComponent<ParticleSystem>().Stop();
            disappear = true;
            timer = 1.0f;
            gameObject.GetComponent<SphereCollider>().enabled = false;
        }
    }
}
