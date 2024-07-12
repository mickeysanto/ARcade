using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//sets the tower's range indicator to the correct scale to display its shooting range
public class RangeIndicator : MonoBehaviour
{
    private SphereCollider bounds;
    private Transform self;
    private Vector3 vec;
    void Start()
    {
        bounds = transform.parent.GetComponent<SphereCollider>();
        self = transform;
        vec = new Vector3(.047f, .047f, 0);
    }

    void FixedUpdate()
    {
        self.localScale = (vec * bounds.radius);
    }
}
