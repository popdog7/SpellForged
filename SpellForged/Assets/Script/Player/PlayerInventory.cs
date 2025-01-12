using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public InventorySO inventory;
    public InventorySO equipment;

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
        switch (slot.parent_interface.inventory.type)
        {
            case InterfaceType.Inventory:
                break;
            case InterfaceType.Equipment:
                print(string.Concat("Placed :", slot.ItemSO, " on ", slot.parent_interface.inventory.type, ", Allowed Item: ", string.Join(", ", slot.allowed_items)));
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
    }

}
