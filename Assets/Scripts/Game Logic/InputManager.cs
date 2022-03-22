using System;
using UnityEngine;

public class InputManager : MonoBehaviour
{

    private Vector2 tankMovementInput = Vector2.zero;
    private Vector2 tankCannonMovementInput = Vector2.zero;

    [SerializeField]
    private VariableJoystick tankMovementJoystick;
    [SerializeField]
    private VariableJoystick tankCannonMovementJoystick;

    private void Update()
    {
        GetInputs();
    }

    private void GetInputs()
    {

        if (tankMovementJoystick.Horizontal != tankMovementInput.x || tankMovementJoystick.Vertical != tankMovementInput.y)
        {
            tankMovementInput.x = tankMovementJoystick.Horizontal * Time.deltaTime;
            tankMovementInput.y = tankMovementJoystick.Vertical * Time.deltaTime;
            GameEvents.OnTankMovementChanged?.Invoke(tankMovementInput);
        }
        if (tankCannonMovementJoystick.Horizontal != tankCannonMovementInput.x || tankCannonMovementJoystick.Vertical != tankCannonMovementInput.y)
        {
            tankCannonMovementInput.x = tankCannonMovementJoystick.Horizontal * Time.deltaTime;
            tankCannonMovementInput.y = tankCannonMovementJoystick.Vertical * Time.deltaTime;
            GameEvents.OnTankCannonMovementChanged?.Invoke(tankCannonMovementInput);
        }
    }
}
