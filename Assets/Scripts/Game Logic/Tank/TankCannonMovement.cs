using System;
using UnityEngine;

public class TankCannonMovement : MonoBehaviour
{

    [SerializeField]
    private float _rotationSpeed = 40f;
    [SerializeField]
    private Transform _cannon;
    [SerializeField]
    private Transform _cannonTop;

    [SerializeField]
    private TankBullet _bulletPrefab;

    private TankData _tankData;

    private Vector3 finalPosition;
    private void Awake()
    {
        _tankData = GetComponentInParent<TankData>();
    }
    private void Start()
    {
        GameEvents.OnTankCannonMovementChanged += ChangeTankCannonMovement;
        GameEvents.OnShootButtonPressed += Shoot;
        GameEvents.OnSurfaceFound += SurfaceFound;

    }

    private void OnDestroy()
    {
        GameEvents.OnTankCannonMovementChanged -= ChangeTankCannonMovement;
        GameEvents.OnShootButtonPressed -= Shoot;
        GameEvents.OnSurfaceFound -= SurfaceFound;
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
        TankBullet bullet = Instantiate(_bulletPrefab, _tankData.CannonTip.position, Quaternion.identity, transform);
        bullet.transform.forward = _tankData.CannonTip.transform.forward;
        bullet.SetData(_tankData.ForceValue, _tankData.TimeValue, _tankData);
    }

    private void SurfaceFound(TrailSphere obj)
    {
        finalPosition = obj.transform.position;
    }

}
