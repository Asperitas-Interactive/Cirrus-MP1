using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vel : MonoBehaviour
{

    [SerializeField]
    Vector3 force;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<Rigidbody>().velocity += force;
    }
}
