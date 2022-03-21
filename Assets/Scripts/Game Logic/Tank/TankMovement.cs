using System.Collections;
using UnityEngine;

public class TankMovement : MonoBehaviour
{
    [SerializeField]
    private float _movementSpeed = 5.0f;
    [SerializeField]
    private float _rotationSpeed = 200.0f;

    private TankData _tankData;

    private void Awake()
    {
        _tankData = GetComponentInParent<TankData>();
    }

    private void OnEnable()
    {
        Debug.Log("Enable");
        GameEvents.OnTankMovementChanged += ChangeTankMovement;
    }
    private void OnDisable()
    {
        Debug.Log("Disable");
        GameEvents.OnTankMovementChanged -= ChangeTankMovement;
    }


    void ChangeTankMovement(Vector2 input)
    {
        if (!_tankData.IsCurrentlyActive)
            return;
        transform.Rotate(0, input.x * _rotationSpeed, 0);
        transform.Translate(0, 0, input.y * _movementSpeed);
        GameEvents.OnTankRotationChanged?.Invoke(_tankData.CannonTip, _tankData.ForceValue, _tankData.TimeValue);
    }




}
