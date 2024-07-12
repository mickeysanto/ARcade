using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadedObjects : MonoBehaviour
{
    public int numLoaded; //number of tracked objects currently loaded in scene
    public GameManager manager;

    void Start()
    {
        numLoaded = 0;
    }

    //starts game once all three tracked images have loaded
    public void loaded()
    {
        numLoaded++;

        if(numLoaded >= 3)
        {
            manager.GameStart();
        }
    }
}
