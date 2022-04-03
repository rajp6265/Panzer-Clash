using System;
using UnityEngine;

public class GameEvents : MonoBehaviour
{
    public static Action<Vector2> OnTankMovementChanged;
    public static Action<Vector2> OnTankCannonMovementChanged;
    public static Action<Transform, float, float> OnTankRotationChanged;
    public static Action OnShootButtonPressed;
    public static Action<TankData> OnTankDestroyed;
    public static Action OnGameStarted;
    public static Action OnCurrentRoundCompleted;

    public static Action<TrailSphere> OnSurfaceFound;
}
