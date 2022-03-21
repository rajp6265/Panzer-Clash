using UnityEngine;

public class TankDamage : MonoBehaviour
{
    [SerializeField]
    private float _health = 100;
    private TankData _tankData;

    private void Awake()
    {
        _tankData = GetComponentInParent<TankData>();
    }
    public void TryApplyDamage(float damageAmount)
    {
        _health -= damageAmount;
        if (_health <= 0)
        {
            GameEvents.OnTankDestroyed?.Invoke(_tankData);
        }
    }


}
