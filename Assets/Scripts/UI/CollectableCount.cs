using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CollectableCount : MonoBehaviour
{
    public bool counted = false;
    [SerializeField]
    private Texture Unchecked;
    [SerializeField]
    private Texture Checked;
    private RawImage UIImage;
    // Start is called before the first frame update
    void Start()
    {
        UIImage = GetComponent<RawImage>();
        UIImage.texture = Unchecked;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void onCollection()
    {
        UIImage.texture = Checked;
        counted = true;
    }
}
