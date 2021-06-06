using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class water : MonoBehaviour
{
    // Start is called before the first frame update

    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Player"))
            other.gameObject.transform.GetChild(4).gameObject.GetComponent<Animator>().SetBool("inWater", true);
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Player"))
        other.gameObject.transform.GetChild(4).gameObject.GetComponent<Animator>().SetBool("inWater", false);
    }
}
