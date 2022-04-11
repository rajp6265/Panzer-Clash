using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Threading.Tasks;

public class GamePlayScreen : UIScreen
{
    [SerializeField]
    private Button _shootButton;

    [SerializeField]
    private TextMeshProUGUI _resultText;
    private void OnEnable()
    {
        _shootButton.onClick.AddListener(OnShootPressed);
        GameEvents.OnTankDestroyed += DeclareResult;
    }
    private void OnDisable()
    {
        _shootButton.onClick.RemoveListener(OnShootPressed);
        GameEvents.OnTankDestroyed -= DeclareResult;
    }

    private async void OnShootPressed()
    {
        GameEvents.OnShootButtonPressed?.Invoke();
        await Task.Delay(2000);
    }

    private async void DeclareResult(TankData tankData)
    {
        Debug.Log(tankData);
        Debug.Log(tankData.transform.name);
        _resultText.text = $"{tankData.name} is the winner";
        await Task.Delay(2000);
        GameOver();
    }

    private void GameOver()
    {
        UIManager.ChangeScreen(ScreenType.EndScreen);
    }

}
