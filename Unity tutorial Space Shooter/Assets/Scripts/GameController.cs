using UnityEngine;
using System.Collections;
using System.Linq;
using UnityEngine.Assertions.Must;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public GameObject[] bonus;
    public GameObject playerExplosion;
    public GameObject hazard;
    public Vector3 spawnValues;

    public float bonusChance;
    public float spawnWait;
    public float startWait;
    public float waveWait;
    public int startingLifes;
    public int hazardCount;
    public int maxLifes;

    public GUIText scoreText;
    public GUIText lifesText;
    public GUIText restartText;
    public GUIText gameOverText;
    public GUIText newHighScoreText;
    public GUIText highScoreText;

    private int playersAlive = 2;
    private int highScore = 0;
    private bool gameOver;
    private bool restart;
    private int score;
    private int lifes;

    void Start()
    {
        gameOver = false;
        restart = false;
        restartText.text = "";
        gameOverText.text = "";
        newHighScoreText.text = "";
        score = 0;
        highScore = PlayerPrefs.GetInt("High Score");
        highScoreText.text = "High score: " + highScore;
        lifes = startingLifes;
        UpdateScore();
        UpdateLifes(0, null);
        StartCoroutine(SpawnWaves());
    }

    void Update()
    {
        if (restart)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                Application.LoadLevel(Application.loadedLevel);
            }
        }
    }

    IEnumerator SpawnWaves()
    {
        yield return new WaitForSeconds(startWait);
        while (true)
        {
            for (int i = 0; i < hazardCount; i++)
            {
                Vector3 spawnPosition = new Vector3(Random.Range(-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
                Quaternion spawnRotation = Quaternion.identity;
                if (bonusChance >= Random.Range(0,100))
                    Instantiate(bonus[Random.Range(0, bonus.Count())], spawnPosition, bonus[0].transform.rotation);
                else
                    Instantiate(hazard, spawnPosition, spawnRotation);
                yield return new WaitForSeconds(spawnWait);
            }
            yield return new WaitForSeconds(waveWait);

            if (gameOver)
            {
                restartText.text = "Press 'R' for Restart";
                restart = true;
                break;
            }
        }
    }

    public void AddScore(int newScoreValue)
    {
        score += newScoreValue;
        UpdateScore();
    }

    public void UpdateLifes(int change, Collider player)
    {
        lifes = Mathf.Clamp(lifes + change, 0, maxLifes);
        lifesText.text = "Lifes: " + lifes;
        if (lifes < 1 && player != null)
        {
            Instantiate(playerExplosion, player.transform.position, player.transform.rotation);
            Destroy(player.gameObject);
            --playersAlive;
            if (playersAlive == 0)
            {
                CheckScore();
                GameOver();
            }
        }
    }

    void UpdateScore()
    {
        scoreText.text = "Score: " + score;
    }

    void CheckScore()
    {
        if (highScore < score)
        {
            highScore = score;
            PlayerPrefs.SetInt("High Score", highScore);
            newHighScoreText.text = "New high score achieved!";
        }
    }

    public void GameOver()
    {
        gameOverText.text = "Game Over!";
        gameOver = true;
    }
}