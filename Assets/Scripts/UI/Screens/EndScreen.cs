using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndScreen : UIScreen
{
    [SerializeField] private Button _restartButton;
    [SerializeField] private Button _exitButton;

    private void Start()
    {
        _restartButton.onClick.AddListener(RestartGame);
        _exitButton.onClick.AddListener(EndGame);
    }

    private void RestartGame()
    {
        UIManager.ChangeScreen(ScreenType.None);
    }
    private void EndGame()
    {
        Application.Quit();
    }
    private void OnDestroy()
    {
        _restartButton.onClick.RemoveListener(RestartGame);
        _exitButton.onClick.RemoveListener(EndGame);
    }
}
