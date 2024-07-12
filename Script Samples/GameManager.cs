using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public SpawnEnemies spawner;
    public int level; //current level of the game
    public float intermissionTime; //time between each level
    public float waveBreak; //time between spawning new waves of enemies
    public int enemiesPerWave; //amount of enemies spawned in a wave
    public int numWaves; //number of waves spawned per level
    public float speedMultiplier, healthMultiplier; //multipliers for the enemy stats
    public TextMeshProUGUI timerText; //timer UI element
    public TextMeshProUGUI healthText; //health UI element (displays lives left)
    public TextMeshProUGUI scoreText; //health UI element (displays lives left)
    public bool canSpawn;
    public int enemiesDead = 0;
    public int totalEnemies = 0;
    public ScoreKeeper scoreKeeper;
    private int difficultyUp;
    public int lives; //number of hits the home base can take before game over
    private bool isPaused;
    private AudioSource audio;

    public void Start()
    {
        spawner = GetComponent<SpawnEnemies>();
        audio = GetComponent<AudioSource>();
        canSpawn = false;
        scoreKeeper.score = 0;
        difficultyUp = 0;
        lives = 25;
        isPaused = false;
        //GameStart();
    }

    public void GameStart()
    {
        level = 1;
        waveBreak = 4f;
        healthMultiplier = 1;
        speedMultiplier = 1;
        enemiesPerWave = 5;
        numWaves = 2;
        intermissionTime = 10f;
        enemiesDead = 0;
        totalEnemies = enemiesPerWave * numWaves;
        scoreKeeper.resetScore();
        StartCoroutine(RunGame());  
    }

    //runs the game and loops until player loss
    private IEnumerator RunGame()
    {
        Debug.Log("in game");
        healthText.text = string.Format(">Lives: {0:00}", lives);
        scoreText.text = string.Format(">Score: {0:00}", scoreKeeper.score);

        while (true)
        {
            canSpawn = false;
            StartCoroutine(Timer(intermissionTime));

            yield return new WaitUntil(() => canSpawn == true);

            totalEnemies = (enemiesPerWave + (int)(enemiesPerWave * .75)) * numWaves;

            for (int i = 0; i < numWaves; i++)
            {
                canSpawn = false;
                spawner.startWave(enemiesPerWave, 2.4f);
                yield return new WaitUntil(() => canSpawn == true);
                yield return new WaitForSeconds(waveBreak);
            }

            yield return new WaitUntil(() => enemiesDead == totalEnemies);

            UpdateGame();

            yield return 0;
        }
    }

    //changes the game up differently
    private void UpdateGame()
    {
        level++;
        enemiesDead = 0;

        //every 3 out of 4 levels it either upgrades an enemy, adds more enemies per wave, or a combination
        //on every fourth level it adds another whole wave as a challenge then goes back to 2
        if(difficultyUp == 0)
        {
            enemiesPerWave += 1;
        }
        else if(difficultyUp == 1)
        {
            enemiesPerWave += 2;
            upgradeEnemies();

        }
        else if (difficultyUp == 2)
        {
            enemiesPerWave += 1;
            upgradeEnemies();
            numWaves++;
        }
        else if (difficultyUp == 3)
        {
            numWaves--;
            difficultyUp = -1;
        }

        difficultyUp++;
        totalEnemies = enemiesPerWave * numWaves;
    }

    //randomly chooses to upgrade enemy health or speed
    private void upgradeEnemies()
    {
        int key = Random.Range(0,2);

        if(key == 1)
        {
            spawner.health *= 1.2f;
        }
        else
        {
            spawner.speed *= 1.15f;
        }
    }

    //timer between levels
    private IEnumerator Timer(float time)
    {
        if (timerText == null)
        {
            Debug.Log("timerText is null");
            yield break;
        }

        while (true)
        {
            timerText.text = string.Format(">Level {0:00} starts in: ", level) + string.Format("{0:00}", time);

            if (time <= 0)
            {
                break;
            }

            time -= Time.deltaTime;

            yield return 0;
        }

        timerText.text = "";
        canSpawn = true;
    }

    private void OnEnable()
    {
        Enemy.giveExp += enemyDeath;
        MenuFuncs.pause += togglePause;
    }

    private void OnDisable()
    {
        Enemy.giveExp -= enemyDeath;
        MenuFuncs.pause -= togglePause;
    }

    //helps keep track of how many enemies have died by towers in the current level and also adds to the score
    private void enemyDeath(float i, int j)
    {
        enemiesDead++;
        scoreKeeper.score++;
        scoreText.text = string.Format(">Score: {0:000}", scoreKeeper.score);
    }

    //if enemy reaches this object subtract from the score and check for game loss
    private void OnTriggerEnter(Collider other)
    {
        Enemy enemy = other.GetComponent<Enemy>();

        if (enemy != null)
        {
            audio.Play();
            enemiesDead++;
            lives--;
            healthText.text = string.Format(">Lives: {0:00}", lives);

            enemy.explode();

            Destroy(enemy.gameObject);

            if(lives <= 0)
            {
                gameLost();
            }
        }
    }

    //once lives are zero transitions to the GameOver screen
    private void gameLost()
    {
        scoreKeeper.compareScore();

        SceneManager.LoadScene("GameOver", LoadSceneMode.Single);
    }

    //pauses the game
    public void togglePause()
    {
        isPaused = !isPaused;
        Time.timeScale = isPaused ? 0 : 1;
    }
}
