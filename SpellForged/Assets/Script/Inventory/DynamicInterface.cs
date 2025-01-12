using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DynamicInterface : UserInterface
{
    [SerializeField] protected GameObject inventory_prefab;
    [SerializeField] private int x_start;
    [SerializeField] private int y_start;

    [SerializeField] private int x_space_between_item;
    [SerializeField] private int y_space_between_item;
    [SerializeField] private int num_columns;

    public override void createInventoryUISlots()
    {
        slots_on_interface = new Dictionary<GameObject, InventorySlot>();
        for (int i = 0; i < inventory.container.items.Length; i++)
        {
            var obj = Instantiate(inventory_prefab, Vector3.zero, Quaternion.identity, transform);
            obj.GetComponent<RectTransform>().localPosition = getPosition(i);

            addEvent(obj, EventTriggerType.PointerEnter, delegate { onEnter(obj); });
            addEvent(obj, EventTriggerType.PointerExit, delegate { onExit(obj); });
            addEvent(obj, EventTriggerType.BeginDrag, delegate { onDragStart(obj); });
            addEvent(obj, EventTriggerType.EndDrag, delegate { onDragEnd(obj); });
            addEvent(obj, EventTriggerType.Drag, delegate { onDrag(obj); });

            slots_on_interface.Add(obj, inventory.container.items[i]);
        }
    }

    public Vector3 getPosition(int i)
    {
        return new Vector3(x_start + (x_space_between_item * (i % num_columns)), y_start + (-y_space_between_item * (i / num_columns)), 0f);
    }
}
