using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Doors : MonoBehaviour
{
    [SerializeField] private GameObject m_cutsceneCamera;
   

    public void OpenDoor(int _delay)
    {
       Invoke("Open", _delay);
    }

    private void Open()
    {
        GetComponent<Animator>().SetTrigger("Open");
    }

    public void setupCam()
    {
        enableCam();
        Invoke("disableCam", 3);
    }

    private void enableCam()
    {
        m_cutsceneCamera.SetActive(true);
    }
    private void disableCam()
    {
        m_cutsceneCamera.SetActive(false);
    }
}
