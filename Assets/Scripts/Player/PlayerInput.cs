using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    [Header("Debug")]
    [SerializeField] private Vector3 input;
    [SerializeField] private float inputMagnitude;

    private FloatingJoystick joystick;
    private float lastTapTime;
    private float doubleTapTime = 0.2f;
    private float jumpCooldown;

    private void Start()
    {
        joystick = InputSystem.Instance.Joystick;
    }

    /// <summary>
    /// Return normalized vector of player input;
    /// </summary>
    /// <returns></returns>
    public Vector3 GetInputNormalized()
    {
        Vector3 normalizedInput = input.normalized;
        return normalizedInput;
    }

    /// <summary>
    /// Return clamp magnitude of player input;
    /// </summary>
    /// <returns></returns>
    public float GetInputMagnitude()
    {
        inputMagnitude = Mathf.Clamp01(input.magnitude);
        return inputMagnitude;
    }

    public void ReadMoveInput()
    {
        float horizontalInput = joystick.Horizontal;
        float verticalInput = joystick.Vertical;
        input = new Vector3(horizontalInput, 0, verticalInput);
    }

    /// <summary>
    /// Return true if jump button pressed;
    /// </summary>
    /// <returns></returns>
    public bool CheckJumpInput()
    {
        jumpCooldown -= Time.deltaTime;

        if (Input.GetMouseButtonDown(0) && jumpCooldown <= 0)
        {
            float timeFromLastTap = Time.time - lastTapTime;
            lastTapTime = Time.time;

            if (timeFromLastTap <= doubleTapTime)
            {
                lastTapTime = 0;
                jumpCooldown = 1.5f;
                    return true;
            }
        }
            return false;
    }

}
