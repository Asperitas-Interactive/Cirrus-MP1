using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalRing : MonoBehaviour
{
    public Collectable collectable;
    public Ring[] Rings;
    int rCount = 0;

    // Start is called before the first frame update

    private void OnEnable()
    {
        collectable.gameObject.SetActive(false);
        
    }
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
            foreach(Ring r in Rings)
            {
                if(r.ringCheck == true)
                {
                    rCount++;
                }
            }

            if(rCount == Rings.Length)
            {
                collectable.gameObject.SetActive(true);
                GameObject.FindGameObjectWithTag("Switch2").GetComponent<PuzzleSpawner>().Timer = 0.0f;
            }
        }
    }

    public void Reset()
    {
        foreach (Ring r in Rings)
        {
            r.Reset();
        }

        rCount = 0;
    }
}
