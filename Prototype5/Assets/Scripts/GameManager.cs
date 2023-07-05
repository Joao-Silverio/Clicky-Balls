using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Unity.VisualScripting.Antlr3.Runtime;

public class GameManager : MonoBehaviour
{
    public List<GameObject> targets;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI livesText;
    public TextMeshProUGUI gameOverText;
    public Button restartButton;
    public GameObject titleScreen;
    public GameObject pauseScreen;
    private int score;
    private int lives;
    private float spawnRate = 1.0f;
    public bool isGameActive = true;
    private bool paused;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyUp(KeyCode.P)) 
        {
            ChangePaused();
        }
    }

    void ChangePaused() //Set Pause Screen to true or false
    {
        if (!paused)
        {
            paused = true;
            pauseScreen.SetActive(true);
            Time.timeScale = 0; //Stop the physics calculations
        }
        else
        {
            paused = false;
            pauseScreen.SetActive(false);
            Time.timeScale = 1;
        }
    }

    public void StartGame(int difficulty) //Set variables at the start
    {
        isGameActive = true;
        score = 0;
        spawnRate /= difficulty;
        lives = 5 - difficulty;
        StartCoroutine(SpawnTarget());
        UpdateScore(0);
        UpdateLives();
        titleScreen.SetActive(false);
    }

    IEnumerator SpawnTarget() //Countdown to spawn targets
    {
        while (isGameActive)
        {
            yield return new WaitForSeconds(spawnRate);
            int index = Random.Range(0, targets.Count);
            Instantiate(targets[index]);
        }
    }

    public void UpdateScore(int scoreToAdd)
    {
        score += scoreToAdd;
        scoreText.text = "Score: " + score;
    }

    public void UpdateLives()
    {
        if (lives > 0)
        {
            lives--;
            livesText.text = "Lives: " + lives;
        }
        else if (lives <= 0)
        {
            GameOver();
        }

    }

    public void GameOver()
    {
        gameOverText.gameObject.SetActive(true);
        restartButton.gameObject.SetActive(true);
        isGameActive = false;
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
