using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject pause_menu;
    [SerializeField] private GameInput game_input;

    private bool isPaused;

    private void Awake()
    {
        isPaused = false;
        game_input.OnPause += pauseGame;
    }

    public void pauseGame(object sender, System.EventArgs e)
    {
        if(!isPaused)
        {
            pause_menu.SetActive(true);
            Time.timeScale = 0f;
            isPaused = true;
            game_input.setPause(true);
        }
    }

    public void resumeGame()
    {
        pause_menu.SetActive(false);
        Time.timeScale = 1.0f;
        isPaused = false;
        game_input.setPause(false);
    }

    public void backToMainMenu()
    {
        Time.timeScale = 1.0f;
        isPaused = false;
        game_input.setPause(false);
        SceneManager.LoadScene(0);
    }

    public void quitGame()
    {
        Application.Quit();
    }

    private void OnDestroy()
    {
        game_input.OnPause -= pauseGame;
    }
}
