using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCastSpell : MonoBehaviour
{
    [SerializeField] private GameInput game_input;
    [SerializeField] private Transform cast_origin;

    [SerializeField] private BaseCastType cast_type;
    [SerializeField] private AttributeModifierSO modifier;

    private void Awake()
    {
        game_input.OnShootAction += GameInputOnShootAction;
        if (CheckSpellFullyCrafted())
        {
            cast_type.setupSpellStats(modifier);
        }
    }

    private void GameInputOnShootAction(object sender, System.EventArgs e)
    {
        if(CheckSpellFullyCrafted())
        {
            cast_type.CommenceSpellCasting(cast_origin);
        }
    }

    private bool CheckSpellFullyCrafted()
    {
        if(cast_type == null)
        {
            Debug.Log("Need Type Rune");
        }
        else if (modifier == null)
        {
            Debug.Log("Need Modifier Rune");
        }

        return true;
    }

    public void SetCastType(BaseCastType type)
    {
        cast_type = type;
        cast_type.setupSpellStats(modifier);
    }

    public void SetAttributeModifier(AttributeModifierSO mod)
    {
        modifier = mod;
        cast_type.setupSpellStats(modifier);
    }
}
