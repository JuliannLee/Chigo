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

    private bool isMainMenuActive = false;

    private void Awake()
    {
        gameOverScreen.SetActive(false);
        pauseScreen.SetActive(false);
        mainMenuScreen.SetActive(false);
        gameFinishScreen.SetActive(false);
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
            gameOverScreen.SetActive(true);
            SoundManager.instance.PlaySound(gameOverSound);
        }
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void MainMenu()
    {
        HidePauseScreen();
        HideGameOverScreen();
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
            pauseScreen.SetActive(status);
            Time.timeScale = status ? 0 : 1;
        }
    }

    private void HidePauseScreen()
    {
        pauseScreen.SetActive(false);
        Time.timeScale = 1;
    }

    public void ShowMainMenuScreen()
    {
        mainMenuScreen.SetActive(true);
    }
    #endregion

    #region Game Finish
    public void GameFinish()
    {
        gameFinishScreen.SetActive(true);
        Time.timeScale = 0; // Pause the game when it finishes
    }

    public void HideGameFinishScreen()
    {
        gameFinishScreen.SetActive(false);
        Time.timeScale = 1; // Ensure timescale is reset
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
}
