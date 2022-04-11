using System;
using UnityEngine;
using UnityEngine.UI;

public class TankDamage : MonoBehaviour
{
    [SerializeField]
    private float _health = 100;
    private TankData _tankData;
    [SerializeField]
    private Slider _healthSlider;

    private void Awake()
    {
        _tankData = GetComponentInParent<TankData>();
    }
    public void TryApplyDamage(float damageAmount, Action declareWinner)
    {
        _health -= damageAmount;
        _healthSlider.value = _health / 100;
        if (_health <= 0)
        {
            GameEvents.OnTankDestroyed?.Invoke(_tankData);
            declareWinner?.Invoke();
        }

    }


}
