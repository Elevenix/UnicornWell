using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private Canvas pauseMenu;
    [SerializeField] private Canvas deathMenu;
    [SerializeField] private Canvas winMenu;
    [SerializeField] private Canvas firstCinematicMenu;

    [Space(6)]
    [Header("Navigation")]

    [SerializeField] private EventSystem eventSystem;
    [SerializeField] private GameObject firstPauseButton;
    [SerializeField] private GameObject firstDeathButton;
    [SerializeField] private GameObject firstWinButton;

    [Space(6)]
    [Header("Sliders")]
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider angledCamSlider;
    [SerializeField] private Slider screenShakeSlider;

    [Space(6)]
    [Header("Sliders")]
    [SerializeField] private TextMeshProUGUI actualScore;
    [SerializeField] private TextMeshProUGUI bestScore;
    [SerializeField] private GameObject gameObjectBestScore;

    public static LevelManager Instance;

    private bool _isPaused = false;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);


        pauseMenu.enabled = false;
        winMenu.gameObject.SetActive(false);
        deathMenu.enabled = false;
        firstCinematicMenu.gameObject.SetActive(false);

        eventSystem.firstSelectedGameObject = null;
    }

    private void Start()
    {
        Time.timeScale = 1;
    }

    private void SetSlidersValues()
    {
        musicSlider.value = PlayerPrefs.GetFloat("Music");
        angledCamSlider.value = PlayerPrefs.GetFloat("AngledCamera");
        screenShakeSlider.value = PlayerPrefs.GetFloat("ScreenShake");
    }

    public void SetPauseGame()
    {
        if (winMenu.gameObject.activeSelf|| deathMenu.enabled)
            return;

        _isPaused = !_isPaused;
        if (_isPaused)
        {
            Time.timeScale = 0;
            eventSystem.SetSelectedGameObject(firstPauseButton);
        }
        else
        {
            eventSystem.SetSelectedGameObject(null);
            Time.timeScale = 1;
        }
        pauseMenu.enabled = _isPaused;

        SetSlidersValues();
    }

    public void LaunchFirstCinematic()
    {
        firstCinematicMenu.gameObject.SetActive(true);
    }

    public void WinGame()
    {
        Time.timeScale = 0;
        winMenu.gameObject.SetActive(true);
        eventSystem.SetSelectedGameObject(firstWinButton);
    }

    public void SetScores(bool isStoryMode, string scoreText, string bestScoreText)
    {
        if (isStoryMode)
        {
            gameObjectBestScore.SetActive(false);
        }
        else
        {
            bestScore.text = bestScoreText;
        }
        actualScore.text = scoreText;
    }

    public void LoseGame()
    {
        // display lose screen
        deathMenu.enabled = true;
        eventSystem.SetSelectedGameObject(firstDeathButton);
    }

    public void OpenLevel(int i)
    {
        SceneManager.LoadScene(i);
    }

    public void RestartCurrentLevel()
    {
        OpenLevel(SceneManager.GetActiveScene().buildIndex);
    }

    public void OpenLevel(string name)
    {
        SceneManager.LoadScene(name);
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
