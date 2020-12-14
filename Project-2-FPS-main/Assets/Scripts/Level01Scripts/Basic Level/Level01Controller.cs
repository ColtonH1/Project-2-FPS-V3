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

    //RaycastShoot raycastShoot;

    void Start()
    {
        SetHighScore();
        EnemyController.ResetScore();
        Resume(); //calls the function so game isn't pause from the beginning
    }
    // Update is called once per frame
    void Update()
    {
        if(RaycastShoot.GetIfShot())
        {
            IncreaseScore(EnemyController.GetScore());
        }

        /*if(Player.IsPlayerDead())
        {
            _currentScore = 0;
            Debug.Log("Is player dead, l01cont " + _currentScore);
        }*/


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
        Debug.Log("Before inc, l01cont " + _currentScore);
        _currentScore = scoreIncrease;
        Debug.Log("After inc, l01cont " + _currentScore);

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
        SetHighScore();
        SceneManager.LoadScene("MainMenu");
    }

    public void SetHighScore()
    {
        int highScore = PlayerPrefs.GetInt("HighScore");
        if (_currentScore > highScore)
        {
            PlayerPrefs.SetInt("HighScore", _currentScore);
            Debug.Log("New high score " + _currentScore);
        }
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

    public void Died()
    {
        reticle.SetActive(false);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        Time.timeScale = 0f;
    }

}
