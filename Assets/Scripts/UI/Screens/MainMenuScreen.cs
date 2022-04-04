using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuScreen : UIScreen
{
    [SerializeField] private Button _playButton;
    [SerializeField] private Button _exitButton;

    private void Start()
    {
        _playButton.onClick.AddListener(StartGame);
        _exitButton.onClick.AddListener(EndGame);
    }

    private void StartGame()
    {
        UIManager.ChangeScreen(ScreenType.None);
    }
    private void EndGame()
    {
        Application.Quit();
    }
    private void OnDestroy()
    {
        _playButton.onClick.RemoveListener(StartGame);
        _exitButton.onClick.RemoveListener(EndGame);
    }
}
