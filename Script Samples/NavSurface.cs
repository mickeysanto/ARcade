using System;
using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;

//bakes a navmesh surface on object
public class NavSurface : MonoBehaviour
{
    public static Action Baked;
    private NavMeshSurface surface;

    public void Start()
    {
        surface = GetComponent<NavMeshSurface>();
        surface.BuildNavMesh();
        Baked?.Invoke();
        //Debug.Log("BAKED");
    }
}
