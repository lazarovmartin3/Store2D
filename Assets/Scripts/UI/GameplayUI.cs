using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class GameplayUI : MonoBehaviour
{
    public static GameplayUI instance;

    public Image hp_bar;
    public TextMeshProUGUI money_txt;
    public GameObject start_menu;
    public Button start_game_btn, quit_game_btn;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        start_game_btn.onClick.AddListener(StartGame);
        quit_game_btn.onClick.AddListener(QuitGame);
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            PauseGame();
        }
    }

    public void UpdateHP(float current_amount)
    {
        hp_bar.transform.localScale = new Vector2(current_amount, 1);
    }

    public void UpdateMoneyAmount(int amount)
    {
        money_txt.text = amount.ToString();
    }

    public void OpenStartMenu()
    {
        start_menu.SetActive(true);
    }

    private void StartGame()
    {
        start_menu.SetActive(false);
        GameManager.instance.StartGame();
    }

    private void QuitGame()
    {
        Application.Quit();
    }

    private void PauseGame()
    {
        Time.timeScale = 0;
        start_menu.SetActive(true);
        start_game_btn.onClick.RemoveAllListeners();
        start_game_btn.onClick.AddListener(ResumeGame);
    }

    private void ResumeGame()
    {
        Time.timeScale = 1;
        start_menu.SetActive(false);
        start_game_btn.onClick.RemoveAllListeners();
        start_game_btn.onClick.AddListener(StartGame);
    }
}
