/*
 * V2
 */

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Level01Controller : MonoBehaviour
{
    [SerializeField] Text _currentScoreTextView;

    //pause menu
    public static bool GameIsPaused = false;
    public GameObject pauseMenuUI;
    public GameObject reticle;

    int _currentScore;

    void Start()
    {
        Resume(); //calls the function so game isn't pause from the beginning
    }
    // Update is called once per frame
    void Update()
    {
        //for debugging
        if (Input.GetKeyDown(KeyCode.Q))
        {
            IncreaseScore(5);
        }

        /*
         * make script to keep score
         * call said script from right here
         * set equal to _currentScore
         * display the score
         */

        //pause menu
        EscPressed();        
    }

    public void IncreaseScore(int scoreIncrease)
    {
        _currentScore += scoreIncrease;

        _currentScoreTextView.text = "Score: " + _currentScore.ToString();
    }

    //pause menu
    private void EscPressed()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void ExitLevel()
    {
        int highScore = PlayerPrefs.GetInt("HighScore");
        if(_currentScore > highScore)
        {
            PlayerPrefs.SetInt("HighScore", _currentScore);
            Debug.Log("New high score " + _currentScore);
        }
        SceneManager.LoadScene("MainMenu");
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        reticle.SetActive(true);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    void Pause()
    {
        pauseMenuUI.SetActive(true);
        reticle.SetActive(false);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    public void QuitButton()
    {
        GameIsPaused = false;
        ExitLevel();
    }

    public bool IsPaused()
    {
        return GameIsPaused;
    }

}
