using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInput : MonoBehaviour
{
    private PlayerInputActions player_input_actions;

    private void Awake()
    {
        player_input_actions = new PlayerInputActions();
        player_input_actions.Player.Enable();
    }

    public Vector2 GetMovementVector()
    {
        Vector2 input_vector = player_input_actions.Player.Move.ReadValue<Vector2>(); ;

        input_vector = input_vector.normalized;

        return input_vector;
    }
}
