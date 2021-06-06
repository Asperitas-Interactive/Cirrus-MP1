using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadingPlat : MonoBehaviour
{
    [SerializeField]
    private float maxFallTimer = 2.0f;
    private bool fadestart = false;
    private bool unfadeStart = false;
    private bool faded = false;
    private float fadeTimer;
    private Vector3 baseScale;
    // Start is called before the first frame update
    void Start()
    {
        fadeTimer = maxFallTimer;
        baseScale = transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        if (fadestart == true)
        { 
           if (faded == false)
           {
              Fade();
           }
        }
        
        if(unfadeStart == true)
        {
            fadeTimer -= Time.deltaTime;

            if (fadeTimer <= 0.0f)
            {
                UnFade();
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        fadestart = true;
        fadeTimer = maxFallTimer;
    }

    private void Fade()
    {
        transform.localScale = new Vector3(Mathf.Clamp(transform.localScale.x - 1 * Time.deltaTime, 0.001f, baseScale.x),
            transform.localScale.y,
            Mathf.Clamp(transform.localScale.z - 1 * Time.deltaTime, 0.001f, baseScale.z));

        if(transform.localScale.x == 0.001f && transform.localScale.z == 0.001f)
        {
            gameObject.GetComponent<BoxCollider>().enabled = false;
            faded = true;
            fadestart = false;
            unfadeStart = true;
            fadeTimer = maxFallTimer;
        }
    }

    private void UnFade()
    {
        transform.localScale = new Vector3(Mathf.Clamp(transform.localScale.x + 10 * Time.deltaTime, 0.001f, baseScale.x),
            transform.localScale.y,
            Mathf.Clamp(transform.localScale.z + 10 * Time.deltaTime, 0.001f, baseScale.z));

        if (transform.localScale.x >= 0.001f && transform.localScale.z >= 0.001f)
        {
            gameObject.GetComponent<BoxCollider>().enabled = true;
        }

            if (transform.localScale.x == baseScale.x && transform.localScale.z == baseScale.z)
        {
            fadeTimer = maxFallTimer;
            unfadeStart = false;
            fadestart = false;
            faded = false;
        }
    }
}
