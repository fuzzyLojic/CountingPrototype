using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public bool GameOver { get; set; }
    private int score;
    private int startTime = 60;
    private int remainingTime;
    private TargetManager targetManager;
    private bool isPaused;

    [Header("Menus and Text")]
    [SerializeField] GameObject titleMenu;
    [SerializeField] GameObject gameText;
    [SerializeField] GameObject pauseMenu;
    [SerializeField] GameObject gameOverMenu;
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] TextMeshProUGUI timeText;
    [SerializeField] Button pauseButton;
    [Header("Music")]
    [SerializeField] AudioClip[] songs;
    private AudioClip chosenSong;
    private AudioSource songSource;


    // Start is called before the first frame update
    void Start()
    {
        GameOver = true;
        targetManager = gameObject.GetComponent<TargetManager>();
        songSource = gameObject.GetComponent<AudioSource>();
        chosenSong = songs[Random.Range(0, songs.Length)];
        Debug.Log(chosenSong.name);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape)){
            TogglePause();
        }
    }

    public void IncrementScore(int value){
        if(!GameOver){
            score += value;
            scoreText.text = $"Score: {score}";
        }
    }

    private void Timer(){
        remainingTime--;
        timeText.text = $"Time: {remainingTime}";
        if(remainingTime == 0){
            EndGame();
        }
    }

    public void StartGame(){
        GameOver = false;
        songSource.clip = chosenSong;
        songSource.Play();
        score = 0;
        scoreText.text = $"Score: {score}";
        remainingTime = startTime;
        timeText.text = $"Time: {remainingTime}";
        titleMenu.SetActive(false);
        gameText.SetActive(true);
        targetManager.StartGame();
        InvokeRepeating("Timer", 0, 1);
    }

    private void EndGame(){
        GameOver = true;
        CancelInvoke();
        pauseButton.gameObject.SetActive(false);
        gameOverMenu.SetActive(true);
    }

    public void TogglePause(){
        if(isPaused){
            isPaused = false;
            Time.timeScale = 1;
            pauseMenu.SetActive(false);
            pauseButton.gameObject.SetActive(true);
        }
        else{
            isPaused = true;
            Time.timeScale = 0;
            pauseButton.gameObject.SetActive(false);
            pauseMenu.SetActive(true);
        }
    }

    public void Restart(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
