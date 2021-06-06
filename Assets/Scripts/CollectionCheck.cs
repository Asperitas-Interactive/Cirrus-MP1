using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollectionCheck : MonoBehaviour
{
    public CollectableCount[] counters;
    public bool bFinished = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        int collection = 0;
        foreach(CollectableCount counter in counters)
        {
            if(counter.counted == true)
            {
                collection++;
            }
        }

        if(collection == 5)
        {
            bFinished = true;
        }
    }
}
