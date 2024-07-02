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

    private bool isMainMenuActive = false;

    private void Awake()
    {
        gameOverScreen.SetActive(false);
        pauseScreen.SetActive(false);
        mainMenuScreen.SetActive(false);
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
    // Activate game over screen
    public void GameOver()
    {
        if (!isMainMenuActive)
        {
            gameOverScreen.SetActive(true);
            SoundManager.instance.PlaySound(gameOverSound);
        }
    }

    // Restart level
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    // Main Menu
    public void MainMenu()
    {
        HidePauseScreen(); // Ensure the pause screen is hidden
        HideGameOverScreen(); // Ensure the game over screen is hidden
        ShowMainMenuScreen(); // Show the main menu screen
        isMainMenuActive = true; // Set the main menu active flag to true
    }

    // Method to load the main menu scene
    public void LoadMainMenuScene()
    {
        SceneManager.LoadScene(0);
    }

    // Method to hide the game over screen
    private void HideGameOverScreen()
    {
        gameOverScreen.SetActive(false);
    }

    // Quit game/exit play mode if in Editor
    public void Quit()
    {
        Application.Quit(); // Quits the game (only works in build)

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false; // Exits play mode (will only be executed in the editor)
#endif
    }
    #endregion

    #region Pause
    public void PauseGame(bool status)
    {
        if (!isMainMenuActive)
        {
            // If status == true pause | if status == false unpause
            pauseScreen.SetActive(status);

            // When pause status is true change timescale to 0 (time stops)
            // when it's false change it back to 1 (time goes by normally)
            Time.timeScale = status ? 0 : 1;
        }
    }

    // Method to hide the pause screen
    private void HidePauseScreen()
    {
        pauseScreen.SetActive(false);
        Time.timeScale = 1; // Ensure timescale is reset
    }

    // Method to show Main Menu screen
    public void ShowMainMenuScreen()
    {
        mainMenuScreen.SetActive(true);
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
