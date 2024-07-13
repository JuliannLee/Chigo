using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [Header("Game Over")]
    [SerializeField] private GameObject gameOverScreen;
    [SerializeField] private AudioClip gameOverSound;

    [Header("Pause")]
    [SerializeField] private GameObject pauseScreen;

    [Header("Main Menu")]
    [SerializeField] private GameObject mainMenuScreen;

    [Header("Game Finish")]
    [SerializeField] private GameObject gameFinishScreen;

    [Header("Settings")]
    [SerializeField] private GameObject settingsScreen;

    private bool isMainMenuActive = false;
    private GameObject currentActiveScreen = null;

    private void Awake()
    {
        HideAllScreens();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !isMainMenuActive)
        {
            // If pause screen already active unpause and vice versa
            PauseGame(!pauseScreen.activeInHierarchy);
        }
    }

    #region Game Over
    public void GameOver()
    {
        if (!isMainMenuActive)
        {
            ShowScreen(gameOverScreen);
            SoundManager.instance.PlaySound(gameOverSound);
        }
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void MainMenu()
    {
        HideAllScreens();
        ShowMainMenuScreen();
        isMainMenuActive = true;
    }

    public void LoadMainMenuScene()
    {
        SceneManager.LoadScene(0);
    }

    private void HideGameOverScreen()
    {
        gameOverScreen.SetActive(false);
    }

    public void Quit()
    {
        Application.Quit();

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
    #endregion

    #region Pause
    public void PauseGame(bool status)
    {
        if (!isMainMenuActive)
        {
            ShowScreen(pauseScreen, status);
            Time.timeScale = status ? 0 : 1;
        }
    }
    #endregion

    #region Game Finish
    public void GameFinish()
    {
        ShowScreen(gameFinishScreen);
        Time.timeScale = 0; // Pause the game when it finishes
    }
    #endregion

    #region Settings
    public void OpenSettings()
    {
        ShowScreen(settingsScreen);
    }

    public void CloseSettings()
    {
        HideScreen(settingsScreen);
    }

    public void BackToMainMenu()
    {
        HideAllScreens();
        ShowMainMenuScreen();
        isMainMenuActive = true;
    }
    #endregion

    #region Sound Settings
    public void SoundVolume()
    {
        SoundManager.instance.ChangeSoundVolume(0.2f);
    }

    public void MusicVolume()
    {
        SoundManager.instance.ChangeMusicVolume(0.2f);
    }
    #endregion

    #region Helper Methods
    private void HideAllScreens()
    {
        gameOverScreen.SetActive(false);
        pauseScreen.SetActive(false);
        mainMenuScreen.SetActive(false);
        gameFinishScreen.SetActive(false);
        settingsScreen.SetActive(false);
        Time.timeScale = 1; // Ensure timescale is reset
    }

    private void ShowScreen(GameObject screen, bool status = true)
    {
        if (currentActiveScreen != null)
        {
            currentActiveScreen.SetActive(false);
        }
        screen.SetActive(status);
        currentActiveScreen = status ? screen : null;
    }

    private void HideScreen(GameObject screen)
    {
        if (currentActiveScreen == screen)
        {
            screen.SetActive(false);
            currentActiveScreen = null;
        }
    }

    public void ShowMainMenuScreen()
    {
        mainMenuScreen.SetActive(true);
    }

    public void HideMainMenuScreen()
    {
        mainMenuScreen.SetActive(false);
    }
    #endregion
}
