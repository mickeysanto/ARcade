using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float damage, range, speed;
    public Vector3 startingPosition, direction;
    public Transform target;
    private Transform self;
    //private Rigidbody2D rb;

    private void Start()
    {
        //rb = GetComponent<Rigidbody2D>();
        self = transform;
    }

    private void Awake()
    {
        startingPosition = transform.position;
        direction = direction.normalized;
    }

    /*private void FixedUpdate()
    {
        rb.velocity = Vector2.ClampMagnitude(rb.velocity, speed);
        rb.AddForce(direction * speed * Time.fixedDeltaTime, ForceMode2D.Impulse);
        Debug.Log(rb.velocity.magnitude);
    }*/

    private void Update()
    {
        //projectile disappears at its max range
        if (Vector3.Distance(startingPosition, self.position) >= range)
        {
            Destroy(gameObject);
        }

        self.position += target.position * speed * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log("1");
        Destroy(gameObject);
    }
}
