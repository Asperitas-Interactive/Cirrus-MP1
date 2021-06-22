using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawner : MonoBehaviour
{

    [SerializeField] private Transform[] m_spawnLocations;


    private CharacterController m_controller;
    public int m_currentSpawnLocation { get; set; }
    
    void Start()
    {
        m_controller = GetComponent<CharacterController>();
        m_currentSpawnLocation = 0;
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.A))
        {
            ToSpawnLoc(0);
        }
    }

    private void LateUpdate()
    {
        if (!m_controller.enabled)
        {
            m_controller.enabled = true;
        }
    }

    public void ToSpawnLoc(int _delay)
    {
        m_controller.enabled = false;
        Invoke("spawnInvokation", _delay);
    }

    public void SetSpawnLocation(int _loc)
    {
        m_currentSpawnLocation = _loc;
    }

    private void spawnInvokation()
    {
        transform.position = m_spawnLocations[m_currentSpawnLocation].position;
    }
}
