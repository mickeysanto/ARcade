using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    public Transform target;
    private Vector3 destination;
    public float speed;
    private Transform self;

    void Start()
    {
        destination = target.position;
        self = transform;
    }

    void Update()
    {
        if(self.position == destination)
        {
            Destroy(gameObject);
        }

        self.position = Vector3.MoveTowards(self.position, destination, speed * Time.deltaTime);
    }
}
