using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameInput : MonoBehaviour
{
    public PlayerInputActions player_input_actions;

    public event EventHandler OnJumpAction;
    public event EventHandler OnShootAction;


    private void Awake()
    {
        player_input_actions = new PlayerInputActions();
        player_input_actions.Player.Enable();

        player_input_actions.Player.Jump.performed += jump_performed;
        player_input_actions.Player.Shoot.performed += shoot_performed;
    }

    private void shoot_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnShootAction?.Invoke(this, EventArgs.Empty);
    }

    private void jump_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnJumpAction?.Invoke(this, EventArgs.Empty);
    }

    public Vector2 GetMovementVector()
    {
        Vector2 input_vector = player_input_actions.Player.Move.ReadValue<Vector2>();
        input_vector = input_vector.normalized;
        return input_vector;
    }

    public Vector2 GetLookVector()
    {
        Vector2 input_vector = player_input_actions.Player.Look.ReadValue<Vector2>();
        return input_vector;
    }
}
