using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SpawnEnemies : MonoBehaviour
{
    private GameManager gameManager;
    public GameObject enemy;
    private bool canSpawn;
    private Transform self;
    public bool isSpawning;
    public float distance; //distance away from spawner an enemy is spawned
    public int spaceRange; //spacing distance enemies are spawned from eachother
    public float speed, health; //enemy stats

    void Start()
    {
        gameManager = GetComponent<GameManager>();
        canSpawn = false;
        self = transform;
        isSpawning = false;
        speed = .6f;
        health = 1.5f;
        distance = 4f;
        spaceRange = 2;
    }

    public void startWave(int totalEnemies, float waitTime)
    {
        StartCoroutine(WaveSpawn(totalEnemies, waitTime));
    }

    //spawns a wave of enemies in generally the same area from two random directions
    private IEnumerator WaveSpawn(int totalEnemies, float waitTime)
    {
        int direction = Random.Range(0, 4); //direction the enemy wave will spawn from in relation to the spawner
        int directionAlt = 0;

        do
        {
            directionAlt = Random.Range(0, 4); //direction alternate

        } while (direction == directionAlt);

        isSpawning = true;
        int altChance = 0;

        for (int i = 0; i < totalEnemies; i++)
        {
            spawn(enemy, findLocation(distance, spaceRange, direction));

            //spawns enemy at alt direction 3/4 times
            if (altChance != 0)
            {
                spawn(enemy, findLocation(distance, spaceRange, directionAlt));
                altChance--;
            }
            else
            {
                altChance = 3;
            }

            yield return new WaitForSeconds(waitTime);
        }

        gameManager.canSpawn = true;
        isSpawning = false;
    }

    //spawns a single enemy
    private void spawn(Object prefab, Vector3 position)
    {
        GameObject bot = Instantiate(prefab, position, Quaternion.identity) as GameObject;
        bot.transform.parent = self;

        Enemy enemy = bot.GetComponent<Enemy>();
        enemy.health = health;

        NavMeshMove move = bot.GetComponent<NavMeshMove>();
        move.agent.speed = speed;
    }
    
    //location at which an enemy will spawn (random)
    private Vector3 findLocation(float distance, int spaceRange, int direction)
    {
        Vector3 location = new Vector3(self.position.x, self.position.y, self.position.z);
        int space = Random.Range(-spaceRange, spaceRange+1);

        switch (direction)
        {
            case 0:
                location.x += distance;
                location.z += space;
                break;
            case 1:
                location.x -= distance;
                location.z += space;
                break;
            case 2:
                location.z += distance;
                location.x += space;
                break;
            case 3:
                location.z -= distance;
                location.x += space;
                break;
        }

        return location;
    }
}
