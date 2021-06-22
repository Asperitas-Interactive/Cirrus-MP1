using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Doors : MonoBehaviour
{
    [SerializeField] private GameObject m_cutsceneCamera;
   

    public void OpenDoor()
    {
        GetComponent<Animator>().SetTrigger("Open");
    }

    public void setupCam()
    {
        Invoke("enableCam", 3);
        Invoke("disableCam", 6);
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
