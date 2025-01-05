using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    [SerializeField] private Camera main_cam;
    [SerializeField] private float interact_distance = 3f;
    [SerializeField] private LayerMask mask;
    [SerializeField] private GameInput game_input;

    private PlayerUI player_UI;

    private void Start()
    {
        player_UI = GetComponent<PlayerUI>();
    }

    private void Update()
    {
        DetectInteractable();
    }

    private void DetectInteractable()
    {
        player_UI.UpdateText(string.Empty);

        RaycastHit hit_info;
        Ray ray = new Ray(main_cam.transform.position, main_cam.transform.forward);

        Debug.DrawRay(ray.origin, ray.direction * interact_distance);
        if (Physics.Raycast(ray, out hit_info, interact_distance, mask))
        {
            PromptInteraction(hit_info);
        }
    }

    private void PromptInteraction(RaycastHit hit_info)
    {
        if (hit_info.collider.GetComponent<Interactable>() != null)
        {
            Interactable interactable = hit_info.collider.GetComponent<Interactable>();
            player_UI.UpdateText(interactable.prompt_message);

            if(game_input.player_input_actions.Player.Interact.triggered)
            {
                interactable.BaseInteract();
            }
        }
    }
}
