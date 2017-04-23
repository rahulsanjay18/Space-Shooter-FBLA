using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class GameControllerScript : MonoBehaviour
{

    public GameObject[] enemies;    // enemies to spawn from, arranged in level order
    public float spawnWait;         // time between spawn
    public float spawnValues;       // x coordinate range it spawns at
    public int score;               // score of player

    //GUI components
    public GUIText statusText;
    public GUIText scoreText;
    public GUIText earthHealthText;
    public GUIText numLivesText;
    public GUIText captionUnderText;

    // deals with logistics of level
    private int nextTime;
    public int levelDuration;
    private int earthHealth;
    private int lives;
    private string[] captionArray = { "A Rocky Beginning", "The Invasion", "The Final Wave", "Infinite Barrage", "Press \'s\' to go to scoreboard" };

    public GameObject player;

    // booleans that help check for special conditions
    private bool isCoroutineRunning;
    private bool isGameOver;
    private bool isScoreLoaded;
    private bool isLevel3Done;

    // celebratory music at end of level 3
    public AudioSource celebMusic;

    // Use this for initialization
    void Start()
    {
        isGameOver = false;

        isCoroutineRunning = false;

        isScoreLoaded = false;

        isLevel3Done = false;

        score = 0;
        updateScore();

        lives = 3;
        updateLives();

        earthHealth = 10;
        updateEarthHealth();

        StartCoroutine(SpawnEnemyWaves());
    }

    // Update is called once per frame
    void Update()
    {
        // checks if lives are lost or game is over
        if (!isGameOver)
        {
            if (!player.gameObject.activeSelf && !isCoroutineRunning)
            {
                isCoroutineRunning = true;
                lives--;
                updateLives();
                StartCoroutine(PlayerDeath());
            }
            else if (lives <= 0)
            {
                DestroyAllObjects();
                StopAllCoroutines();
                statusText.text = "Game Over. 0 lives left.";
                captionUnderText.text = captionArray[enemies.Length + 1];
                isGameOver = true;
            }
            else if (earthHealth <= 0)
            {
                DestroyAllObjects();
                StopAllCoroutines();
                statusText.text = "Game Over. Earth health 0.";
                captionUnderText.text = captionArray[enemies.Length + 1];
                isGameOver = true;
            }
            if (Input.GetKey(KeyCode.Escape))
                Application.Quit();
        }

        // controls for if the scoreboard is loaded or not
        if (!isScoreLoaded)
        {
            if (isGameOver && Input.GetKey(KeyCode.S))
            {
                SceneManager.LoadScene("Scoreboard", LoadSceneMode.Single);
                isScoreLoaded = true;
            }
        }

        // checks for special case when level 3 is done
        if(isLevel3Done && Input.GetKey(KeyCode.S))
        {
            SceneManager.LoadScene("Scoreboard", LoadSceneMode.Single);
            isScoreLoaded = true;
            
        }

    }

    // adds score
    public void addScore(int addTo)
    {
        score += addTo;
        updateScore();
    }

    // updates view
    void updateScore()
    {
        scoreText.text = "" + score;
    }

    // updates view
    void updateLives()
    {
        numLivesText.text = "Lives: " + lives;
    }

    // subtracts earth health
    public void boundaryHit()
    {
        earthHealth--;
        updateEarthHealth();
    }

    // updates view
    void updateEarthHealth()
    {
        earthHealthText.text = "" + earthHealth + "/10";
    }
    
    // spawns enemies
    IEnumerator SpawnEnemyWaves()
    {
        // loops so that each level, a new enemy has the chance to appear
        for (int i = 1; i <= enemies.Length; i++)
        {
            // update display
            statusText.text = "Level " + i;
            captionUnderText.text = captionArray[i - 1];

            // pause for player to read and get ready for next level
            yield return new WaitForSeconds(5);

            // reset view to begin level
            statusText.text = "";
            captionUnderText.text = "";

            // set level duration
            nextTime = levelDuration * (i + 1);

            // spawns random enemies
            while (Time.timeSinceLevelLoad < nextTime)
            {
                int num = Random.Range(0, i);
                Vector3 spawnPosition = new Vector3(Random.Range(-spawnValues, spawnValues), 19, 0);
                Quaternion spawnRotation = Quaternion.identity;
                Instantiate(enemies[num], spawnPosition, spawnRotation);
                yield return new WaitForSeconds(spawnWait);
            }

            // clears field when level is done
            DestroyAllObjects();
        }

        // congradulatory text and music
        statusText.text = "You Beat the Game!";
        isLevel3Done = true;
        captionUnderText.text = captionArray[enemies.Length + 1] + ", press nothing to continue...";
        this.GetComponent<AudioSource>().Pause();
        this.celebMusic.enabled = true;
        this.celebMusic.Play();

        // allow player time to decide if they wish to continue
        yield return new WaitForSeconds(7);

        // set up for infinite mode
        this.celebMusic.Stop();
        this.GetComponent<AudioSource>().Play();
        isLevel3Done = false;
        statusText.text = "Level " + (enemies.Length + 1);
        captionUnderText.text = captionArray[enemies.Length];
        yield return new WaitForSeconds(5);
        statusText.text = "";
        captionUnderText.text = "";

        // infinetly attack player
        while (true)
        {
            int num = Random.Range(0, enemies.Length);
            Vector3 spawnPosition = new Vector3(Random.Range(-spawnValues, spawnValues), 19, 0);
            Quaternion spawnRotation = Quaternion.identity;
            Instantiate(enemies[num], spawnPosition, spawnRotation);
            yield return new WaitForSeconds(spawnWait);
        }

    }


    // sequence of events for when player dies
    IEnumerator PlayerDeath()
    {
        statusText.text = "You Died! Respawning...";
        DestroyAllObjects();
        yield return new WaitForSeconds(2);
        player.SetActive(true);
        isCoroutineRunning = false;
        statusText.text = "";
    }

    // clear everything
    void DestroyAllObjects()
    {
        List<GameObject> enemiesOnScene = new List<GameObject>();

        enemiesOnScene.AddRange(GameObject.FindGameObjectsWithTag("Asteroid"));
        enemiesOnScene.AddRange(GameObject.FindGameObjectsWithTag("Enemy"));
        enemiesOnScene.AddRange(GameObject.FindGameObjectsWithTag("AstEnemy"));


        for (int i = 0; i < enemiesOnScene.Count; i++)
        {
            Destroy(enemiesOnScene[i]);
        }
    }

    void Awake()
    {
        DontDestroyOnLoad(this);
    }
}
