using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationHandler : MonoBehaviour
{

    private Animator m_animator;
    private CharacterController m_characterController;
    
    void Start()
    {
        m_animator = GetComponent<Animator>();
        m_characterController = GetComponentInParent<CharacterController>();
    }

    void Update()
    {
        m_animator.SetFloat("Speed", m_characterController.velocity.magnitude);
    }
}
