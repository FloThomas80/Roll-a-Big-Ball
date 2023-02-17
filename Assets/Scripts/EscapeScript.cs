using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Rigidbody))]
public class EscapeScript : MonoBehaviour
{

    [SerializeField] GameObject Exit;
    [SerializeField] NavMeshAgent Indiana;
    [SerializeField] GameObject GiantBall;
    private bool _running = false;
    private Animator _animator;

    public void Start()
    {
        _animator = GetComponent<Animator>();
    
    }
    public void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Ball")
        { 
            Indiana.SetDestination(Exit.transform.position);
            _running = true;
            _animator.SetBool("Running", _running);

        }
    }


    public void OnTriggerExit(Collider col)
    {
        if (col.tag == "Ball")
        { 
            Indiana.ResetPath();
        _running = false;
        _animator.SetBool("Running", _running);
        }
    }



}
