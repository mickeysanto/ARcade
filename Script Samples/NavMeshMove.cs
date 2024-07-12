using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

//moves Enemy NavMeshAgents
public class NavMeshMove : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform target;
    private bool canMove;
    private Vector3 targetPos;
    private Animator animator;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        target = GameObject.Find("HOMEBASE").transform;
        targetPos = target.transform.position;
        agent.SetDestination(targetPos);
        animator = GetComponent<Animator>();

    }
}
