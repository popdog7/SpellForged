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
    public InventorySO inventory;
    protected Dictionary<GameObject, InventorySlot> slots_on_interface = new Dictionary<GameObject, InventorySlot>();

    private void Start()
    {
        for(int i = 0; i < inventory.get_slots.Length; i++)
        {
            inventory.get_slots[i].parent_interface = this;
            inventory.get_slots[i].on_after_update += onSlotUpdate;
        }

        createInventoryUISlots();

        addEvent(gameObject, EventTriggerType.PointerEnter, delegate { onEnterInterface(gameObject); });
        addEvent(gameObject, EventTriggerType.PointerExit, delegate { onExitInterface(gameObject); });
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
        mouseData.slot_hovered_over = obj;
    }

    public void onExit(GameObject obj)
    {
        mouseData.slot_hovered_over = null;
    }

    public void onDragStart(GameObject obj)
    {
        mouseData.temp_item_cur_dragging = createTempItem(obj);   
    }

    public GameObject createTempItem(GameObject obj)
    {
        GameObject temp_item = null;

        if (slots_on_interface[obj].item.id >= 0)
        {
            temp_item = new GameObject();
            var rect_transform = temp_item.AddComponent<RectTransform>();
            rect_transform.sizeDelta = new Vector2(50, 50);
            temp_item.transform.SetParent(transform.parent);
            var img = temp_item.AddComponent<Image>();
            img.sprite = slots_on_interface[obj].ItemSO.inventory_icon;
            img.raycastTarget = false;
        }

        return temp_item;
    }

    public void onDragEnd(GameObject obj)
    {     
        Destroy(mouseData.temp_item_cur_dragging);
        if(mouseData.interface_mouse_is_over == null)
        {
            slots_on_interface[obj].removeItem();
            return;
        }
        if(mouseData.slot_hovered_over)
        {
            InventorySlot slot_hovered_over_data = mouseData.interface_mouse_is_over.slots_on_interface[mouseData.slot_hovered_over];
            inventory.swapItems(slots_on_interface[obj], slot_hovered_over_data);
        }
    }

    public void onDrag(GameObject obj)
    {
        if (mouseData.temp_item_cur_dragging != null)
        {
            mouseData.temp_item_cur_dragging.GetComponent<RectTransform>().position = Input.mousePosition;
        }
    }

    public void onEnterInterface(GameObject obj)
    {
        mouseData.interface_mouse_is_over = obj.GetComponent<UserInterface>();
    }

    public void onExitInterface(GameObject obj)
    {
        mouseData.interface_mouse_is_over = null;
    }

    private void onSlotUpdate(InventorySlot _slot)
    {
        if (_slot.item.id >= 0)
        {
            _slot.slot.transform.GetChild(0).GetComponentInChildren<Image>().sprite = _slot.ItemSO.inventory_icon;
            _slot.slot.transform.GetChild(0).GetComponentInChildren<Image>().color = new Color(1, 1, 1, 1);
            _slot.slot.transform.GetComponentInChildren<TextMeshProUGUI>().text = _slot.amount == 1 ? "" : _slot.amount.ToString();
        }
        else
        {
            _slot.slot.transform.GetChild(0).GetComponentInChildren<Image>().sprite = null;
            _slot.slot.transform.GetChild(0).GetComponentInChildren<Image>().color = new Color(1, 1, 1, 0);
            _slot.slot.transform.GetComponentInChildren<TextMeshProUGUI>().text = "";
        }
    }
}

public static class mouseData
{
    public static UserInterface interface_mouse_is_over;
    public static GameObject temp_item_cur_dragging;
    public static GameObject slot_hovered_over;
}

public static class ExtensionMethods
{
    public static void updateSlotDisplay(this Dictionary<GameObject, InventorySlot> _slots_on_interface)
    {
        foreach (KeyValuePair<GameObject, InventorySlot> slot in _slots_on_interface)
        {
            if (slot.Value.item.id >= 0)
            {
                slot.Key.transform.GetChild(0).GetComponentInChildren<Image>().sprite = slot.Value.ItemSO.inventory_icon;
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
}
