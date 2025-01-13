using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameInput : MonoBehaviour
{
    public PlayerInputActions player_input_actions;

    public event EventHandler OnJumpAction;
    public event EventHandler OnShootActionStart;
    public event EventHandler OnShootActionEnd;
    public event EventHandler OnSwitchSpell;


    private void Awake()
    {
        player_input_actions = new PlayerInputActions();
        player_input_actions.Player.Enable();

        player_input_actions.Player.Jump.performed += jump_performed;
        player_input_actions.Player.Shoot.performed += shoot_performed;
        player_input_actions.Player.Shoot.canceled += Shoot_canceled;
        player_input_actions.Player.SwitchSpell.performed += switchspell_performed;
    }

    private void switchspell_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnSwitchSpell?.Invoke(this, EventArgs.Empty);
    }

    private void Shoot_canceled(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnShootActionEnd?.Invoke(this, EventArgs.Empty);
    }

    private void shoot_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnShootActionStart?.Invoke(this, EventArgs.Empty);
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
