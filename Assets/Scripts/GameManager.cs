using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public List<GameObject> targets;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI livesText;
    public TextMeshProUGUI gameOverText;
    public Button restartButton;
    public GameObject titleScreen;
    public GameObject pauseMenu;
    private AudioSource backgroundMusic;
    private float spawnRate = 1;
    private int score;
    public bool isGameActive;
    public int lives = 3;
    public bool isPaused = false;

    // Start is called before the first frame update
    void Start()
    {
        backgroundMusic = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        // pause game
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            isPaused = !isPaused;
        }
        PauseGameOption(isPaused);
        // player's lives
        SetLives(lives);
        if (lives <= 0)
        {
            GameOver();
        }
    }

    private IEnumerator SpawnTarget(float spawnRate)
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

    public void GameOver()
    {
        isGameActive = false;
        gameOverText.gameObject.SetActive(true);
        restartButton.gameObject.SetActive(true);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void StartGame(int difficulty)
    {
        // set the game's difficulty
        spawnRate /= difficulty;
        // is the game over or not ???
        isGameActive = true;
        // initalize the score
        score = 0;
        UpdateScore(0);
        // start spawning the targets
        StartCoroutine(SpawnTarget(spawnRate));
        // diactivate the title screen
        titleScreen.gameObject.SetActive(false);
    }

    public void SetLives(int lives)
    {
        livesText.text = "Lives: " + lives;
    }

    public void ChangeVolume(float volume)
    {
        backgroundMusic.volume = volume;
    }

    public void PauseGameOption(bool isPause)
    {
        if (isPause)
        {
            Time.timeScale = 0;
            
        }
        else
        {
            Time.timeScale = 1;
        }
        pauseMenu.SetActive(isPause);
    }
}
