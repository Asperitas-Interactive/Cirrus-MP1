using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionContentButton : MonoBehaviour
{
    public GameObject[] OptionContent;

    public void Update()
    {
        if (Input.GetKeyDown("escape"))
        {
            Debug.Log("a");
        }
    }

    public void onClick()
    {
        foreach(GameObject go in OptionContent)
        {
            go.SetActive(!go.activeSelf);
        }
    }
}
