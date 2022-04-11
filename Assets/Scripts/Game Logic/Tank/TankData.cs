using UnityEngine;

public class TankData : MonoBehaviour
{
    [SerializeField]
    private float _parabolicTimeValue;
    [SerializeField]
    private Color _tankColor;
    public Color TankColor => _tankColor;
    public float TimeValue => _parabolicTimeValue;
    [SerializeField]
    private float _forceValue;
    public float ForceValue => _forceValue;

    private bool _isCurrentlyActivate;
    public bool IsCurrentlyActive => _isCurrentlyActivate;

    [SerializeField]
    private Transform _cannonTip;

    public Transform CannonTip => _cannonTip;

    public void SetCurrentTankData(bool status)
    {
        _isCurrentlyActivate = status;
    }
}

