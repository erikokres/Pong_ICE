using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    public static GameManager Instance;

    [Header("Game Settings")]
    public int player1Score;
    public int player2Score;
    public float timer;
    public bool isOver;
    public bool goldenGoal;
    public bool isSpawnPowerUp;
    public GameObject ballSpawned;
    public string warnabola;

    [Header("Prefabs")]
    public GameObject[] BallPrefabs;
    public GameObject[] powerUp;

    [Header("Panels")]
    public GameObject PausePanel;
    public GameObject GameOverPanel;

    [Header("InGame UI")]
    public TextMeshProUGUI timerTxt;
    public TextMeshProUGUI player1ScoreTxt;
    public TextMeshProUGUI player2ScoreTxt;
    public GameObject goldenGoalUI;

    [Header("Game Over UI")]
    public GameObject player1WinUI;
    public GameObject player2WinUI;
    public GameObject youWin;
    public GameObject youLose;
    // Start is called before the first frame update
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }
    void Start()
    {
        Time.timeScale = 1;
        PausePanel.SetActive(false);
        GameOverPanel.SetActive(false);

        player2WinUI.SetActive(false);
        player1WinUI.SetActive(false);
        youWin.SetActive(false);
        youLose.SetActive(false);
        goldenGoalUI.SetActive(false);

        timer = GameData.instance.gameTimer;
        isOver = false;
        goldenGoal = false;
        warnabola = GameData.instance.baal;

        SpawnBall();
    }

    void Update()
    {
        player1ScoreTxt.text = player1Score.ToString();
        player2ScoreTxt.text = player2Score.ToString();

        if (timer > 0f)
        {
            timer -= Time.deltaTime;
            // float munites = Mathf.FloorToInt(timer / 60);
            // float seconds = Mathf.FloorToInt(timer % 60);
            // timerTxt.text = string.Format("(0:00):(1:00)", munites, seconds);


            int munites = (int)(timer / 60);
            int seconds = (int)(timer % 60); //modulus
            timerTxt.text = "";
            if (munites > 0) { timerTxt.text += "0" + munites + ":"; }
            else if (munites == 0) { timerTxt.text += "00:"; }
            if (seconds > 0) { timerTxt.text += seconds; }
            else if (seconds <= 10) { timerTxt.text += "0" + seconds; }


            if (seconds % 20 == 0 && !isSpawnPowerUp)
            {
                StartCoroutine("SpawnPowerUp");
            }
        }
        if (timer <= 0f && !isOver)
        {
            timerTxt.text = "00:00";
            if (player1Score == player2Score)
            {
                if (!goldenGoal)
                {
                    goldenGoal = true;

                    Ball[] ball = FindObjectsOfType<Ball>();
                    for (int i = 0; i < ball.Length; i++)
                    {
                        Destroy(ball[i].gameObject);
                    }
                    goldenGoalUI.SetActive(true);

                    SpawnBall();
                }
            }
            else
            {
                GameOver();
            }
        }
    }

    public IEnumerator SpawnPowerUp()
    {
        isSpawnPowerUp = true;
        Debug.Log("Power Up");
        int rand = Random.Range(0, powerUp.Length);
        Instantiate(powerUp[rand], new Vector3(Random.Range(-3.2f, 3.2f), Random.Range(-2.35f, 2.35f), 0), Quaternion.identity);
        yield return new WaitForSeconds(1);
        isSpawnPowerUp = false;
    }



    public void PauseGame()
    {
        Time.timeScale = 0;
        PausePanel.SetActive(true);
        SoundManager.instance.UIClickSfx();
    }

    public void ResumeGame()
    {
        PausePanel.SetActive(false);
        Time.timeScale = 1;
        SoundManager.instance.UIClickSfx();
    }

    public void BackToMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("MainMenu");
        SoundManager.instance.UIClickSfx();
    }

    public void Restart()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Gameplay");
        SoundManager.instance.UIClickSfx();
    }

    public void SpawnBall()
    {
        Debug.Log("Muncul Bola");
        StartCoroutine("DelaySpawn");
    }

    public void GameOver()
    {
        SoundManager.instance.UIClickSfx();
        isOver = true;
        Debug.Log("Game Over");
        Time.timeScale = 0f;

        GameOverPanel.SetActive(true);

        if (!GameData.instance.isSinglePlayer)
        {
            if (player1Score > player2Score)
            {
                player1WinUI.SetActive(true);
            }
            if (player1Score < player2Score)
            {
                player2WinUI.SetActive(true);
            }
        }
        else
        {
            if (player1Score > player2Score)
            {
                youWin.SetActive(true);
            }
            if (player1Score < player2Score)
            {
                youLose.SetActive(true);
            }
        }
    }

    private IEnumerator DelaySpawn()
    {
        yield return new WaitForSeconds(3);
        if (ballSpawned == null && warnabola == "Putih")
        {
            ballSpawned = Instantiate(BallPrefabs[0], Vector3.zero, Quaternion.identity);
        }
        if (ballSpawned == null && warnabola == "Merah")
        {
            ballSpawned = Instantiate(BallPrefabs[1], Vector3.zero, Quaternion.identity);
        }
        if (ballSpawned == null && warnabola == "Kuning")
        {
            ballSpawned = Instantiate(BallPrefabs[2], Vector3.zero, Quaternion.identity);
        }
        //Instantiate(BallPrefabs, Vector3.zero, Quaternion.identity);
    }

}
