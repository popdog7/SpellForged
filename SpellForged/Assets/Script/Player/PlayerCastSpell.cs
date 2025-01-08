using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCastSpell : MonoBehaviour
{
    [SerializeField] private GameInput game_input;
    [SerializeField] private Transform cast_origin;

    [SerializeField] private BaseCastType cast_type;
    [SerializeField] private AttributeModifierSO modifier;
    [SerializeField] private ElementalTypeSO elemental_type;

    private bool can_shoot = false;

    private void Awake()
    {
        game_input.OnShootActionStart += GameInputOnShootActionStart;
        game_input.OnShootActionEnd += GameInputOnShootActionEnd;
        if (CheckSpellFullyCrafted())
        {
            cast_type.setupSpellStats(modifier, elemental_type);
        }
    }

    private void Update()
    {
        if (CheckSpellFullyCrafted() && can_shoot)
        {
            cast_type.CommenceSpellCasting(cast_origin);
        }
    }

    private void GameInputOnShootActionEnd(object sender, System.EventArgs e)
    {
        can_shoot = false;
    }

    private void GameInputOnShootActionStart(object sender, System.EventArgs e)
    {
        can_shoot = true;
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

    public void setCastType(BaseCastType type)
    {
        cast_type = type;
        cast_type.setupSpellStats(modifier, elemental_type);
    }

    public void setAttributeModifier(AttributeModifierSO mod)
    {
        modifier = mod;
        cast_type.setupSpellStats(modifier, elemental_type);
    }

    public void setElementalType(ElementalTypeSO element)
    {
        elemental_type = element;
        cast_type.setupSpellStats(modifier, elemental_type);
    }
}
