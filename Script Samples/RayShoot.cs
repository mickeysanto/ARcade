using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayShoot : MonoBehaviour
{
    public Transform rayOrigin;
    public AudioClip clip;
    private Transform self;
    private LineRenderer line;
    public AudioSource audio;

    void Start()
    {
        audio = GetComponent<AudioSource>();
        self = transform;
        line = GetComponent<LineRenderer>();
        line.enabled = false;
        audio.clip = clip;
    }

    //shoots a laser from the Tower's shooting point to the center of mass of the enemy
    public void shoot(Transform target, float damage, int ID)
    {
        RaycastHit hit;

        if (Physics.Raycast(rayOrigin.position, target.position - rayOrigin.position, out hit, Vector3.Distance(target.position, rayOrigin.position)))
        {
            line.SetPosition(0, rayOrigin.position);
            line.SetPosition(1, new Vector3(hit.point.x+.05f, hit.point.y+.07f, hit.point.z +.05f));
            StartCoroutine(RenderLine());

            Transform other = hit.transform;
            other.SendMessage("UpdateID", ID);
            other.SendMessage("Hit", damage);
        }
    }

    //renders the "laser beam" line
    private IEnumerator RenderLine()
    {
        line.enabled = true;
        audio.Play();
        yield return new WaitForSeconds(.1f);
        line.enabled = false;
    }
}
