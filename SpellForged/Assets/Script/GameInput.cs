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
    public event EventHandler OnPause;
    public event EventHandler OnToggleInventory;

    private bool isPaused;
    private bool isInventoryOpen;


    private void Awake()
    {
        player_input_actions = new PlayerInputActions();
        player_input_actions.Player.Enable();

        isPaused = false;
        isInventoryOpen = false;

        player_input_actions.Player.Jump.performed += jump_performed;
        player_input_actions.Player.Shoot.performed += shoot_performed;
        player_input_actions.Player.Shoot.canceled += Shoot_canceled;
        player_input_actions.Player.SwitchSpell.performed += switchspell_performed;
        player_input_actions.Player.Pause.performed += pause_performed;
        player_input_actions.Player.ToggleInventory.performed += toggleinventory_performed;
        
    }

    private void toggleinventory_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        if (isPaused)
            return;

        OnToggleInventory?.Invoke(this, EventArgs.Empty);
    }

    private void pause_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        if (isPaused || isInventoryOpen)
            return;

        OnPause?.Invoke(this, EventArgs.Empty);
    }

    private void switchspell_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        if (isPaused || isInventoryOpen)
            return;

        OnSwitchSpell?.Invoke(this, EventArgs.Empty);
    }

    private void Shoot_canceled(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        if (isPaused || isInventoryOpen)
            return;

        OnShootActionEnd?.Invoke(this, EventArgs.Empty);
    }

    private void shoot_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        if (isPaused || isInventoryOpen)
            return;

        OnShootActionStart?.Invoke(this, EventArgs.Empty);
    }

    private void jump_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        if (isPaused || isInventoryOpen)
            return;

        OnJumpAction?.Invoke(this, EventArgs.Empty);
    }

    public Vector2 GetMovementVector()
    {
        if (isInventoryOpen)
            return Vector2.zero;

        Vector2 input_vector = player_input_actions.Player.Move.ReadValue<Vector2>();
        input_vector = input_vector.normalized;
        return input_vector;
    }

    public Vector2 GetLookVector()
    {
        if (isInventoryOpen)
            return Vector2.zero;

        Vector2 input_vector = player_input_actions.Player.Look.ReadValue<Vector2>();
        return input_vector;
    }

    public void setPause(bool result)
    {
        isPaused = result;
    }

    public void setIsInventoryOpen(bool result)
    {
        isInventoryOpen = result;
    }
}
