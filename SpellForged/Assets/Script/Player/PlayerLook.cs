using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLook : MonoBehaviour
{
    [SerializeField] private Camera main_cam;
    [SerializeField] private GameInput game_input;

    private float x_rotation = 0f;

    public float x_sensitivity = 30f;
    public float y_sensitivity = 30f;

    private void Update()
    {
        ProcessLook();
    }

    public void ProcessLook()
    {
        Vector2 input = game_input.GetLookVector();
        float mouse_x = input.x;
        float mouse_y = input.y;

        x_rotation -= (mouse_y * Time.deltaTime) * y_sensitivity;
        x_rotation = Mathf.Clamp(x_rotation, -80f, 80f);

        main_cam.transform.localRotation = Quaternion.Euler(x_rotation, 0, 0);

        transform.Rotate(Vector3.up * (mouse_x * Time.deltaTime) * x_sensitivity);
    }
}
