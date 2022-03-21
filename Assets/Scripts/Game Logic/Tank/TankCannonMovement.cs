using UnityEngine;

public class TankCannonMovement : MonoBehaviour
{

    [SerializeField]
    private float _rotationSpeed = 20f;
    [SerializeField]
    private Transform _cannon;
    [SerializeField]
    private Transform _cannonTop;

    [SerializeField]
    private TankBullet _bulletPrefab;

    private TankData _tankData;

    private void Awake()
    {
        _tankData = GetComponentInParent<TankData>();
    }
    private void OnEnable()
    {
        GameEvents.OnTankCannonMovementChanged += ChangeTankCannonMovement;
        GameEvents.OnShootButtonPressed += Shoot;

    }
    private void OnDisable()
    {
        GameEvents.OnTankCannonMovementChanged -= ChangeTankCannonMovement;
        GameEvents.OnShootButtonPressed -= Shoot;
    }

    void ChangeTankCannonMovement(Vector2 input)
    {
        if (!_tankData.IsCurrentlyActive)
            return;

        var data = _cannon.localEulerAngles;

        data.x += -input.y * _rotationSpeed;
        if (data.x <= 22 || data.x >= 305)
            _cannon.localEulerAngles = data;
        _cannonTop.Rotate(0, input.x * _rotationSpeed, 0);
        GameEvents.OnTankRotationChanged?.Invoke(_tankData.CannonTip, _tankData.ForceValue, _tankData.TimeValue);
    }

    public float ClampAngle(float angle, float min, float max)
    {
        if (angle < -360F)
            angle += 360F;
        if (angle > 360F)
            angle -= 360F;
        return Mathf.Clamp(angle, min, max);
    }

    private void Shoot()
    {
        if (!_tankData.IsCurrentlyActive)
            return;
        Debug.Log("bullet");
        TankBullet bullet = Instantiate(_bulletPrefab, _tankData.CannonTip.position, Quaternion.identity, transform);
        bullet.transform.forward = _tankData.CannonTip.transform.forward;
        bullet.SetForceValue(_tankData.ForceValue);
    }

}
