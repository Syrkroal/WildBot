using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Follow : MonoBehaviour
{
    private NavMeshAgent agent;
    public Transform player;
    private Transform myTransform;
    private BoxCollider boxCollider;
    public float rotationSpeed = 0.2f;
    private float rotStep;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        boxCollider = GetComponent<BoxCollider>();
        myTransform = transform;
        rotStep = rotationSpeed * Time.deltaTime;
        if (!player) {
            player = GameObject.FindWithTag("Player").transform;
        }
    }

    void Update()
    {
        agent.SetDestination(player.position);
    }

    public void SetPlayer(Transform posPlayer)
    {
        player = posPlayer;
    }
}
