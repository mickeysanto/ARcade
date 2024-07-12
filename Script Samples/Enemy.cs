using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public static Action<float, int> giveExp; //gives exp to the tower that killed this enemy
    public static Action<Transform> died; //tells the game manager that one of the enemies has died
    public static Action sfx;
    public float exp, health, speed; //enemy stats
    public int towerID; //ID of tower that last hit the enemy
    private Transform self;
    public ParticleSystem explosion;
    private AudioSource audio;

    private void Start()
    {
        audio = GetComponent<AudioSource>();
        towerID = -1;
        self = transform;
        speed = 2.5f;
    }

    private void Die()
    {
        giveExp?.Invoke(exp, towerID);
        died?.Invoke(self);

        explode();

        Destroy(gameObject);
    }

    //spawns explosion effect at position this enemy died
    public void explode()
    {
        if (explosion != null)
        {
            sfx?.Invoke();
            ParticleSystem explosionEffect = Instantiate(explosion) as ParticleSystem;
            explosionEffect.transform.position = self.position;
            explosionEffect.Play();
        }
    }

    //subtracts health from enemy until death
    void Hit(float damage)
    {
        health -= damage;

        if(health <= 0) 
        {
            Die();
        }
    }

    //kepps track of last tower to hit this enemy
    private void UpdateID(int ID)
    {
        towerID = ID;
    }
}
