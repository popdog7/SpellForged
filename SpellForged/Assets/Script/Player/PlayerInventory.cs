using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public InventorySO inventory;
    public InventorySO equipment;
    [SerializeField] private PlayerCastSpell player_cast_spell;

    private void Start()
    {
        for (int i = 0; i < equipment.get_slots.Length; i++)
        {
            equipment.get_slots[i].on_before_update += onBeforeSlotUpdate;
            equipment.get_slots[i].on_after_update += onAfterSlotUpdate;
        }
    }

    public void onBeforeSlotUpdate(InventorySlot slot)
    {
        if (slot.ItemSO == null)
            return;

        switch (slot.parent_interface.inventory.type)
        {
            case InterfaceType.Inventory:
                break;
            case InterfaceType.Equipment:
                print(string.Concat("Removed :", slot.ItemSO, " on ", slot.parent_interface.inventory.type, ", Allowed Item: ", string.Join(", ", slot.allowed_items)));
                break;
            default:
                break;
        }

    }

    public void onAfterSlotUpdate(InventorySlot slot)
    {
        if (slot.ItemSO == null)
        {
            if(slot.allowed_items.Length > 0)
            {
                player_cast_spell.setDefault(slot.allowed_items[0], slot.spell_assingment_num);
            }
            return;
        }
        switch (slot.parent_interface.inventory.type)
        {
            case InterfaceType.Inventory:
                break;
            case InterfaceType.Equipment:
                print(string.Concat("Placed :", slot.ItemSO, " on ", slot.parent_interface.inventory.type, ", Allowed Item: ", string.Join(", ", slot.allowed_items)));
                determineItemSetter(slot.ItemSO.type, slot);
                break;
            default:
                break;
        }
    }

    private void determineItemSetter(ItemType type, InventorySlot slot)
    {
        switch (type)
        {
            case ItemType.cast_rune:
                player_cast_spell.setCastType(((CastRuneSO)slot.ItemSO).cast_type, slot.spell_assingment_num);
                break;
            case ItemType.element_rune:
                player_cast_spell.setElementalType((ElementRuneSO)slot.ItemSO, slot.spell_assingment_num);
                break;
            case ItemType.modifier_rune:
                player_cast_spell.setAttributeModifier((ModifierRuneSO)slot.ItemSO, slot.spell_assingment_num);
                break;
            default:
                break;
        }
    }

    private void OnApplicationQuit()
    {
        inventory.clear();
        equipment.clear();
    }


    //TEMP TILL I IMPLEMENT THE REAL EVENT FROM GAME INPUT
    /*
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            inventory.Save();
            equipment.Save();
        }

        if (Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            inventory.Load();
            equipment.Load();
        }
    }*/

}
