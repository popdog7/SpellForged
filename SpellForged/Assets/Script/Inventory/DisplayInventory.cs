using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using Unity.VisualScripting;


public class DisplayInventory : MonoBehaviour
{
    public mouseItem mouse_item = new mouseItem();

    [SerializeField] private InventorySO inventory;
    [SerializeField] private GameObject inventory_prefab;

    [SerializeField] private int x_start;
    [SerializeField] private int y_start;

    [SerializeField] private int x_space_between_item;
    [SerializeField] private int y_space_between_item;
    [SerializeField] private int num_columns;

    Dictionary<GameObject, InventorySlot> items_displayed = new Dictionary<GameObject, InventorySlot> ();

    
    private void Start()
    {
        createInventoryUISlots();
    }

    private void Update()
    {
        updateInventorySlots();
    }

    public void updateInventorySlots()
    {
        foreach(KeyValuePair<GameObject, InventorySlot> slot in items_displayed)
        {
            if(slot.Value.id >= 0)
            {
                slot.Key.transform.GetChild(0).GetComponentInChildren<Image>().sprite = inventory.database.get_item[slot.Value.item.id].inventory_icon;
                slot.Key.transform.GetChild(0).GetComponentInChildren<Image>().color = new Color(1, 1, 1, 1);
                slot.Key.transform.GetComponentInChildren<TextMeshProUGUI>().text = slot.Value.amount == 1 ? "" : slot.Value.amount.ToString();
            }
            else
            {
                slot.Key.transform.GetChild(0).GetComponentInChildren<Image>().sprite = null;
                slot.Key.transform.GetChild(0).GetComponentInChildren<Image>().color = new Color(1, 1, 1, 0);
                slot.Key.transform.GetComponentInChildren<TextMeshProUGUI>().text = "";
            }
        }
    }

    public void createInventoryUISlots()
    {
        items_displayed = new Dictionary<GameObject, InventorySlot>();
        for (int i = 0; i < inventory.container.items.Length; i++)
        {
            var obj = Instantiate(inventory_prefab, Vector3.zero, Quaternion.identity, transform);
            obj.GetComponent<RectTransform>().localPosition = getPosition(i);

            addEvent(obj, EventTriggerType.PointerEnter, delegate { onEnter(obj); });
            addEvent(obj, EventTriggerType.PointerExit, delegate { onExit(obj); });
            addEvent(obj, EventTriggerType.BeginDrag, delegate { onDragStart(obj); });
            addEvent(obj, EventTriggerType.EndDrag, delegate { onDragEnd(obj); });
            addEvent(obj, EventTriggerType.Drag, delegate { onDrag(obj); });

            items_displayed.Add(obj, inventory.container.items[i]);
        }
    }

    public Vector3 getPosition(int i)
    {
        return new Vector3(x_start + (x_space_between_item * (i % num_columns)), y_start + (-y_space_between_item * (i / num_columns)), 0f);
    }
    
    private void addEvent(GameObject obj, EventTriggerType type, UnityAction<BaseEventData> action)
    {
        EventTrigger trigger = obj.GetComponent<EventTrigger>();
        var evenTrigger = new EventTrigger.Entry();
        evenTrigger.eventID = type;
        evenTrigger.callback.AddListener(action);
        trigger.triggers.Add(evenTrigger);
    }

    public void onEnter(GameObject obj)
    {
        mouse_item.hover_obj = obj;
        if (items_displayed.ContainsKey(obj))
            mouse_item.hover_item = items_displayed[obj];
    }

    public void onExit(GameObject obj)
    {
        mouse_item.hover_obj = null;
        mouse_item.hover_item = null;
    }

    public void onDragStart(GameObject obj)
    {
        var mouse_object = new GameObject();
        var rect_transform = mouse_object.AddComponent<RectTransform>();
        rect_transform.sizeDelta = new Vector2(50, 50);
        mouse_object.transform.SetParent(transform.parent);
        if (items_displayed[obj].id >= 0)
        {
            var img = mouse_object.AddComponent<Image>();
            img.sprite = inventory.database.get_item[items_displayed[obj].id].inventory_icon;
            img.raycastTarget = false;
        }

        mouse_item.obj = mouse_object;
        mouse_item.item = items_displayed[obj];
    }

    public void onDragEnd(GameObject obj)
    {
        if(mouse_item.hover_obj)
        {
            inventory.moveItem(items_displayed[obj], items_displayed[mouse_item.hover_obj]);
        }
        else
        {
            inventory.RemoveItem(items_displayed[obj].item);
        }
        Destroy(mouse_item.obj);
        mouse_item.item = null;
    }

    public void onDrag(GameObject obj)
    {
        if(mouse_item.obj != null)
        {
            mouse_item.obj.GetComponent<RectTransform>().position = Input.mousePosition;
        }
    }
}

public class mouseItem
{
    public GameObject obj;
    public InventorySlot item;

    public InventorySlot hover_item;
    public GameObject hover_obj;
}
