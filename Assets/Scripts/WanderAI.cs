using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WanderAI : MonoBehaviour
{
    
    
    public float radius;
    private Vector3 origin;
    public float maxTimer;

    Vector3 vel = Vector3.zero;
    Vector3 MovePos = Vector3.zero;

    private float timer;

    private void Start()
    {
    }
    private void OnEnable()
    {
        origin = transform.localPosition;
        GetComponent<Rigidbody>().velocity = Vector3.zero;

    }
    private void OnDisable()
    {
        //GetComponent<Rigidbody>().velocity = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        //transform.Translate(vel * Time.deltaTime);
        Vector3 velNor = vel.normalized;

        transform.rotation = Quaternion.LookRotation(GetComponent<Rigidbody>().velocity);

        if ((MovePos - transform.localPosition).x < 0.1f && (MovePos - transform.localPosition).z < 0.1f)
        {
            timer = 0.0f;
        }
        if (timer <= 0.0f)
        {
            MovePos = RandomPositionInCircle(origin, 4);
            GetComponent<Rigidbody>().velocity = (MovePos - transform.localPosition).normalized * 2.0f;
            //transform.rotation = Quaternion.Euler(0f, Mathf.Atan2(vel.x, vel.z) * Mathf.Rad2Deg, 0f);

            timer = maxTimer;
        }
    }

    Vector3 RandomPositionInCircle(Vector3 origin, float radius) 
    {
        Vector3 RandPos = Random.insideUnitSphere * radius;
        RandPos.y = origin.y;
        RandPos += origin;

        return RandPos;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
           
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            //agent.enabled = true;
        }
    }
}
