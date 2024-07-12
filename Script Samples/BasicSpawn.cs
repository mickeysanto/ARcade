using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class BasicSpawn : MonoBehaviour
{
    public GameObject enemy;
    private Transform self;
    public Transform target;

    void Start()
    {
        self = transform;
        StartCoroutine(spawn());
    }

    public IEnumerator spawn()
    {
        while(true) 
        {
            GameObject bot = Instantiate(enemy, self.position, Quaternion.identity) as GameObject;
            bot.transform.parent = self;
            bot.transform.LookAt(target);

            yield return new WaitForSeconds(4);
        }
    }
}
