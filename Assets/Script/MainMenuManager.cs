using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{

    [Header("Main Menu Panel List")]
    public GameObject MainPanel;
    public GameObject HTPPanel;
    public GameObject TimerPanel;
    public GameObject SelecBall;
    // Start is called before the first frame update
    void Start()
    {
        MainPanel.SetActive(true);
        HTPPanel.SetActive(false);
        TimerPanel.SetActive(false);
        SelecBall.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SinglePlayerButton()
    {
        TimerPanel.SetActive(true);
        MainPanel.SetActive(false);
        GameData.instance.isSinglePlayer = true;
        SoundManager.instance.UIClickSfx();
    }

    public void MultiPlayerButton()
    {
        TimerPanel.SetActive(true);
        MainPanel.SetActive(false);
        GameData.instance.isSinglePlayer = false;
        SoundManager.instance.UIClickSfx();
    }

    public void BackButton()
    {
        HTPPanel.SetActive(false);
        TimerPanel.SetActive(false);
        MainPanel.SetActive(true);
        SelecBall.SetActive(false);
        SoundManager.instance.UIClickSfx();
    }

    public void SetTimerButton(float Timer)
    {
        GameData.instance.gameTimer = Timer;
        HTPPanel.SetActive(false);
        TimerPanel.SetActive(false);
        MainPanel.SetActive(false);
        SelecBall.SetActive(true);
        SoundManager.instance.UIClickSfx();
    }

    public void SelectBallButton(string WarnaBola)
    {
        GameData.instance.baal = WarnaBola;
        SelecBall.SetActive(false);
        HTPPanel.SetActive(true);
        TimerPanel.SetActive(false);
        MainPanel.SetActive(false);
        SoundManager.instance.UIClickSfx();
    }

    public void StartBtn()
    {
        SceneManager.LoadScene("Gameplay");
        SoundManager.instance.UIClickSfx();
    }

    public void ExitGame()
    {
        Application.Quit();
    }

}
