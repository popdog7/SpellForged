using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class StaticInterface : UserInterface
{
    public GameObject[] slots;

    public override void createInventoryUISlots()
    {
        slots_on_interface = new Dictionary<GameObject, InventorySlot> ();

        for (int i = 0; i < inventory.container.items.Length; i++)
        {
            var obj = slots[i];

            addEvent(obj, EventTriggerType.PointerEnter, delegate { onEnter(obj); });
            addEvent(obj, EventTriggerType.PointerExit, delegate { onExit(obj); });
            addEvent(obj, EventTriggerType.BeginDrag, delegate { onDragStart(obj); });
            addEvent(obj, EventTriggerType.EndDrag, delegate { onDragEnd(obj); });
            addEvent(obj, EventTriggerType.Drag, delegate { onDrag(obj); });

            slots_on_interface.Add(obj, inventory.container.items[i]);
        }
    }
}