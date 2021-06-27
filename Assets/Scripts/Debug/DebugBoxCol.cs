using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugBoxCol : MonoBehaviour
{
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        if(GetComponent<BoxCollider>() != null)
        {
            Gizmos.DrawCube(transform.position, new Vector3(1, 1, 1));
            Debug.Log(transform.position);
        }
    }
}
