using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ring : MonoBehaviour
{
    public bool ringCheck = false;
    [SerializeField]
    private Material blueRing;
    [SerializeField]
    private Material redRing;
    // Start is called before the first frame update
    private void OnEnable()
    {
        ringCheck = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            ringCheck = true;
            gameObject.GetComponent<MeshRenderer>().material = redRing;
        }
    }

    public void Reset()
    {
        ringCheck = false;
        gameObject.GetComponent<MeshRenderer>().material = blueRing;
    }
}
