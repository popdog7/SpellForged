using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ExitInteractable : Interactable
{
    [SerializeField] private GameObject game_over_menu;
    [SerializeField] private GameInput game_input;
    [SerializeField] private TextMeshProUGUI status_text;
    [SerializeField] private EnemyManager enemy_manager;
    protected override void Interact()
    {
        if(enemy_manager.isCleared(4))
        {
            status_text.text = "You Win";
            Cursor.visible = true;
            Time.timeScale = 0f;
            game_input.setPause(true);
            game_over_menu.SetActive(true);
        }  
    }
}
