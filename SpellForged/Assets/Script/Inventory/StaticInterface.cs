using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class StaticInterface : UserInterface
{
    public GameObject[] slots;
    //kinda temporary
    [SerializeField] private bool is_spell_inventory;
    [SerializeField] private int num_of_spells = 0;

    public override void createInventoryUISlots()
    {
        slots_on_interface = new Dictionary<GameObject, InventorySlot> ();

        for (int i = 0; i < inventory.get_slots.Length; i++)
        {
            var obj = slots[i];

            addEvent(obj, EventTriggerType.PointerEnter, delegate { onEnter(obj); });
            addEvent(obj, EventTriggerType.PointerExit, delegate { onExit(obj); });
            addEvent(obj, EventTriggerType.BeginDrag, delegate { onDragStart(obj); });
            addEvent(obj, EventTriggerType.EndDrag, delegate { onDragEnd(obj); });
            addEvent(obj, EventTriggerType.Drag, delegate { onDrag(obj); });

            if(is_spell_inventory)
            {
                inventory.get_slots[i].spell_assingment_num = (i + 2) % num_of_spells;
            }

            inventory.get_slots[i].slot = obj;

            slots_on_interface.Add(obj, inventory.get_slots[i]);
        }
    }
}
