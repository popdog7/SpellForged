using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : BaseHealth
{
    [SerializeField] private GameObject game_over_menu;
    [SerializeField] private GameInput game_input;
    public override void OnDeath()
    {
        Cursor.visible = true;
        Time.timeScale = 0f;
        game_input.setPause(true);
        game_over_menu.SetActive(true);
        Debug.Log("dead");
    }
}
