using UnityEngine;

public class GameSceneData : MonoBehaviour
{
    private void OnEnable()
    {
        GameEvents.OnGameStarted?.Invoke();
    }
}
