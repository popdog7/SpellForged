using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCastSpell : MonoBehaviour
{
    [SerializeField] private GameInput game_input;
    [SerializeField] private Transform cast_origin;
    [SerializeField] private BaseCastType[] cast_objecs;

    private BaseCastType[] cast_type = new BaseCastType[2];
    private ModifierRuneSO[] modifier = new ModifierRuneSO[2];
    private ElementRuneSO[] elemental_type = new ElementRuneSO[2];

    private int active_spell_slot = 0;

    [SerializeField] private int default_cast_type;
    [SerializeField] private ModifierRuneSO default_modifier;
    [SerializeField] private ElementRuneSO default_elemental_type;

    private bool can_shoot = false;

    private void Awake()
    {
        game_input.OnShootActionStart += GameInputOnShootActionStart;
        game_input.OnShootActionEnd += GameInputOnShootActionEnd;
        game_input.OnSwitchSpell += GameInputOnSwitchSpell;

        spellInitialization();
    }


    private void Update()
    {
        if (CheckSpellFullyCrafted() && can_shoot)
        {
            cast_type[active_spell_slot].CommenceSpellCasting(cast_origin);
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
    private void GameInputOnSwitchSpell(object sender, System.EventArgs e)
    {
        if(active_spell_slot == 0)
        {
            active_spell_slot = 1;
        }
        else
        {
            active_spell_slot = 0;
        }
        cast_type[active_spell_slot].setupSpellStats(modifier[active_spell_slot], elemental_type[active_spell_slot]);
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

    public void setCastType(int type_num, int spell_num)
    {
        cast_type[spell_num] = cast_objecs[type_num];
        //Debug.Log("changing to shot: " + cast_objecs[type_num]);
        //Debug.Log("currently to shot: " + cast_type[spell_num]);
        //Debug.Log("spellnum: " + spell_num);
        if (spell_num == active_spell_slot)
        {
            cast_type[spell_num].setupSpellStats(modifier[spell_num], elemental_type[spell_num]);
        }
    }

    public void setAttributeModifier(ModifierRuneSO mod, int spell_num)
    {
        modifier[spell_num] = mod;
        if(spell_num == active_spell_slot)
        {
            cast_type[spell_num].setupSpellStats(modifier[spell_num], elemental_type[spell_num]);
        }
    }

    public void setElementalType(ElementRuneSO element, int spell_num)
    {
        elemental_type[spell_num] = element;
        Debug.Log("Setting slot: " + spell_num + " Type: " + cast_type[spell_num] + " to element: " + element.name);
        if (spell_num == active_spell_slot)
        {
            cast_type[spell_num].setupSpellStats(modifier[spell_num], elemental_type[spell_num]);
        }
    }

    public void setDefault(ItemType type, int spell_num)
    {
        switch (type)
        {
            case ItemType.cast_rune:
                setCastType(default_cast_type, spell_num);
                break;
            case ItemType.element_rune:
                setElementalType(default_elemental_type, spell_num);
                break;
            case ItemType.modifier_rune:
                setAttributeModifier(default_modifier, spell_num);
                break;
            default:
                break;
        }
    }

    public void spellInitialization()
    {
        for(int i =0; i < cast_type.Length; i++)
        {
            cast_type[i] = cast_objecs[default_cast_type];
            modifier[i] = default_modifier;
            elemental_type[i] = default_elemental_type;

            cast_type[i].setupSpellStats(modifier[i], elemental_type[i]);
        }
    }
}