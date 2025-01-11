using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using Unity.VisualScripting;


public abstract class UserInterface : MonoBehaviour
{
    [SerializeField] protected PlayerInventory player;
    [SerializeField] protected InventorySO inventory;

    protected Dictionary<GameObject, InventorySlot> items_displayed = new Dictionary<GameObject, InventorySlot>();


    private void Start()
    {
        for(int i = 0; i < inventory.container.items.Length; i++)
        {
            inventory.container.items[i].parent_interface = this;
        }

        createInventoryUISlots();

        addEvent(gameObject, EventTriggerType.PointerEnter, delegate { onEnterInterface(gameObject); });
        addEvent(gameObject, EventTriggerType.PointerExit, delegate { onExitInterface(gameObject); });
    }

    private void Update()
    {
        updateInventorySlots();
    }

    public void updateInventorySlots()
    {
        foreach (KeyValuePair<GameObject, InventorySlot> slot in items_displayed)
        {
            if (slot.Value.id >= 0)
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

    public abstract void createInventoryUISlots();

    protected void addEvent(GameObject obj, EventTriggerType type, UnityAction<BaseEventData> action)
    {
        EventTrigger trigger = obj.GetComponent<EventTrigger>();
        var evenTrigger = new EventTrigger.Entry();
        evenTrigger.eventID = type;
        evenTrigger.callback.AddListener(action);
        trigger.triggers.Add(evenTrigger);
    }

    public void onEnter(GameObject obj)
    {
        player.mouse_item.hover_obj = obj;
        if (items_displayed.ContainsKey(obj))
            player.mouse_item.hover_item = items_displayed[obj];
    }

    public void onExit(GameObject obj)
    {
        player.mouse_item.hover_obj = null;
        player.mouse_item.hover_item = null;
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

        player.mouse_item.obj = mouse_object;
        player.mouse_item.item = items_displayed[obj];
    }

    public void onDragEnd(GameObject obj)
    {
        var item_on_mouse = player.mouse_item;
        var mouse_hover_item = item_on_mouse.hover_item;
        var mouse_hover_obj = item_on_mouse.hover_obj;
        var get_item_object = inventory.database.get_item;

        if (item_on_mouse.ui != null)
        {
            if (mouse_hover_obj)
            {
                if (mouse_hover_item.canPlaceInSlot(get_item_object[items_displayed[obj].id]) && (mouse_hover_item.item.id <= -1 || (mouse_hover_item.item.id >= 0 && items_displayed[obj].canPlaceInSlot(get_item_object[mouse_hover_item.item.id]))))
                    inventory.moveItem(items_displayed[obj], mouse_hover_item.parent_interface.items_displayed[item_on_mouse.hover_obj]);
            }
        }
        else
        {
            inventory.RemoveItem(items_displayed[obj].item);
        }
        Destroy(item_on_mouse.obj);
        item_on_mouse.item = null;
    }

    public void onDrag(GameObject obj)
    {
        if (player.mouse_item.obj != null)
        {
            player.mouse_item.obj.GetComponent<RectTransform>().position = Input.mousePosition;
        }
    }

    public void onEnterInterface(GameObject obj)
    {
        player.mouse_item.ui = obj.GetComponent<UserInterface>();
    }

    public void onExitInterface(GameObject obj)
    {
        player.mouse_item.ui = null;
    }
}

public class mouseItem
{
    public UserInterface ui;

    public GameObject obj;
    public InventorySlot item;

    public InventorySlot hover_item;
    public GameObject hover_obj;
}
