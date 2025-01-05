using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private CharacterController controller;
    [SerializeField] private GameInput game_input;
    private Vector3 velocity;

    [SerializeField] private float speed = 5f;
    [SerializeField] private float gravity = -9.8f;
    [SerializeField] float jump_height = 3f;

    private bool is_grounded;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        game_input.OnJumpAction += GameInputOnJumpAction;
    }


    private void Update()
    {
        is_grounded = controller.isGrounded;
    }

    private void FixedUpdate()
    {
        Movemnt();
    }

    private void Movemnt()
    {
        Vector2 input = game_input.GetMovementVector();

        Vector3 movement_direction = new Vector3(input.x, 0, input.y);
        controller.Move(transform.TransformDirection(movement_direction) * (speed * Time.deltaTime));

        velocity.y += gravity * Time.deltaTime;
        if (is_grounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }
        controller.Move(velocity* Time.deltaTime);
    }
    private void GameInputOnJumpAction(object sender, System.EventArgs e)
    {
        if(is_grounded)
        {
            velocity.y = Mathf.Sqrt(jump_height * -3f - gravity);
        }
    }
}
