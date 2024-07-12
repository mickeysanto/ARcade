using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    public void spawn(Object prefab, Vector3 position, Transform target, float speedMultiplier, float damageMultiplier, float rangeMultiplier)
    {
        GameObject proj = Instantiate(prefab, position, Quaternion.identity) as GameObject;
        Projectile stats = proj.GetComponent<Projectile>();

        stats.target = target;
        stats.speed *= speedMultiplier;
        stats.damage *= damageMultiplier;
        stats.range *= rangeMultiplier;
        Debug.Log(damageMultiplier);
    }
}
