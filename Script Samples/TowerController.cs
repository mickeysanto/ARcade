using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using UnityEngine.UIElements;

public class TowerController : MonoBehaviour
{
    public TowerStats tower;
    public GetTarget targeter;
    private RayShoot ray;
    public Transform towerHead;

    private bool canShoot;
    private Transform self;
    public Transform target = null;
    private Vector3 lastPos; //last known position of most current target

    private void Start()
    {
        self = transform;
        tower = GetComponent<TowerStats>();
        targeter = self.parent.GetComponent<GetTarget>();
        target = null;
        ray = GetComponent<RayShoot>();
        canShoot = true;
        lastPos = Vector3.zero;
    }

    //if the tower has a valid target it shoots at it
    private void Update()
    {
        target = targeter.target;

        if(target != null) 
        {
            lastPos = target.position;

            if(canShoot) 
            {
                attack();
            }

            towerHead.LookAt(lastPos);
        }
    }

    private void attack()
    {
        ray.shoot(target, tower.damage, tower.ID);
        StartCoroutine(AttackCoolDown());
    }

    //pauses Tower shooting by attackSpeed amount
    private IEnumerator AttackCoolDown()
    {
        canShoot = false;
        yield return new WaitForSeconds(tower.attackSpeed);
        canShoot = true;
    }
}
