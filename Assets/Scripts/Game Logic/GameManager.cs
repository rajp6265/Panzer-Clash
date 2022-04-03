using UnityEngine;
using System.Collections.Generic;

public class GameManager : Singleton<GameManager>
{
    [SerializeField]
    private List<TankData> _tanks = new List<TankData>();
    private int _currentIndex = -1;
    [SerializeField]
    private TankData _currentTankData;

    private void OnEnable()
    {
        GameEvents.OnGameStarted += StartGame;
        GameEvents.OnCurrentRoundCompleted += StartNextTurn;
        GameEvents.OnTankDestroyed += DisableAllTanks;
    }
    private void OnDisable()
    {
        GameEvents.OnGameStarted -= StartGame;
        GameEvents.OnCurrentRoundCompleted -= StartNextTurn;
        GameEvents.OnTankDestroyed -= DisableAllTanks;
        _currentIndex = -1;
    }

    private void StartGame()
    {
        int randomNumber = Random.Range(0, _tanks.Count);
        _currentIndex = randomNumber;
        _currentTankData = _tanks[_currentIndex];
        EnableNextPlayer();
    }

    private void StartNextTurn()
    {
        int nextIndex = _currentIndex++;
        nextIndex = _currentIndex % _tanks.Count;
        _currentIndex = nextIndex;
        _currentTankData = _tanks[_currentIndex];
        EnableNextPlayer();
    }

    private void EnableNextPlayer()
    {
        for (int i = 0; i < _tanks.Count; i++)
        {
            _tanks[i].SetCurrentTankData(i == _currentIndex ? true : false);
        }
    }

    private void EnableTheTank(TankData tankData)
    {
        tankData.SetCurrentTankData(true);
    }

    private void DisableAllTanks(TankData tankData)
    {
        foreach (TankData item in _tanks)
        {
            item.SetCurrentTankData(false);
        }
        _currentIndex = -1;
    }

}
