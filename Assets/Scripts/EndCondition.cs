using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndCondition : MonoBehaviour
{
    // Start is called before the first frame update
    public CollectionCheck cc;
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
            if(cc.bFinished == true)
            {
                SceneManager.LoadScene(2);
            }
        }
    }
}
