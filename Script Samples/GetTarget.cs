using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GetTarget : MonoBehaviour
{
    public GameObject towerBody;
    public bool hasTarget;
    public Transform target;
    public List<Transform> targets;

    void Start()
    {
        hasTarget = false;
        targets = new List<Transform>();
    }

    private void Update()
    {
        if (target == null)
        {
            targets.RemoveAll(x => x == null);
            hasTarget = false;       
        }

        if(!hasTarget)
        {
            if(targets.Count > 0)
            {
                hasTarget = true;
                target = targets[0];           
            }
            else
            {
                target = null;
                hasTarget = false;
            }
        }
    }

    private void OnEnable()
    {
        Enemy.died += changeTargets;
    }

    private void OnDisable()
    {
        Enemy.died -= changeTargets;
    }

    private void changeTargets(Transform enemy)
    {
        if(enemy == null)
        {
            targets.RemoveAll(x => x == null);
        }
        else
        {
            int idx = targets.IndexOf(enemy);

            if (idx != -1)
            {
                targets.RemoveAt(idx);

                if (enemy == target)
                {
                    hasTarget = false;
                }
            }
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        if(collision.GetComponent<Enemy>() != null)
        {
            targets.Add(collision.transform);
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        Transform other = collision.transform;
        changeTargets(other);
    }
}
