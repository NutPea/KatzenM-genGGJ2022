using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{

    bool isPaused;
    GameObject pauseMenuObject;

    PlayerInput inputActions;
    PlayerController playerController;

    private void Awake()
    {
        inputActions = new PlayerInput();

        inputActions.Keyboard.Pause.performed += ctx => MenuDecision();
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    void Start()
    {
        pauseMenuObject = transform.GetChild(0).gameObject;
    }

    private void OnEnable()
    {
        inputActions.Enable();
    }

    private void OnDisable()
    {
        inputActions.Disable();
    }

    public void MenuDecision()
    {
        if (isPaused)
        {
            Pause();
            isPaused = false;
        }
        else
        {
            Resume();
            isPaused = true;
        }
    }

    public void Resume()
    {
        pauseMenuObject.SetActive(false);
        playerController.blockPlayerInput = false;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        Time.timeScale = 1;
    }

    public void Pause()
    {
        pauseMenuObject.SetActive(true);
        playerController.blockPlayerInput = true;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        Time.timeScale = 0;
    }

    public void Restart()
    {
        Resume();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
