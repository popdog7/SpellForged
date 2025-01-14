using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerHealth : BaseHealth
{
    [SerializeField] private GameObject game_over_menu;
    [SerializeField] private GameInput game_input;
    [SerializeField] private TextMeshProUGUI status_text;
    [SerializeField] private TextMeshProUGUI health_text;

    private void Start()
    {
        health_text.text = "Health: " + health.ToString() + " / " + max_health.ToString();
    }

    public override void damageHealth(float amount)
    {
        base.damageHealth(amount);
        health_text.text = "Health: " + health.ToString() + " / " + max_health.ToString();
    }

    public override void OnDeath()
    {
        status_text.text = "You Lose";
        Cursor.visible = true;
        Time.timeScale = 0f;
        game_input.setPause(true);
        game_over_menu.SetActive(true);
        Debug.Log("dead");
    }
}
