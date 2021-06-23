using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Doors : MonoBehaviour
{
    
   

    public void OpenDoor(int _delay)
    {
       Invoke("Open", _delay);
    }

    private void Open()
    {
        GetComponent<Animator>().SetTrigger("Open");
    }



   
}
