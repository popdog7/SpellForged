using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DisplayInventory : MonoBehaviour
{
    [SerializeField] private InventorySO inventory;
    [SerializeField] private GameObject inventory_prefab;

    [SerializeField] private int x_start;
    [SerializeField] private int y_start;

    [SerializeField] private int x_space_between_item;
    [SerializeField] private int y_space_between_item;
    [SerializeField] private int num_columns;

    Dictionary<InventorySlot, GameObject> items_displayed = new Dictionary<InventorySlot, GameObject> ();

    
    private void Start()
    {
        createDisplay();
    }

    private void Update()
    {
        UpdateDisplay();
    }

    public void UpdateDisplay()
    {
        for (int i = 0; i < inventory.container.items.Count; i++)
        {
            InventorySlot slot = inventory.container.items[i];

            if (items_displayed.ContainsKey(slot))
            {
                items_displayed[slot].GetComponentInChildren<TextMeshProUGUI>().text = slot.amount.ToString("n0");
            }
            else
            {
                var obj = Instantiate(inventory_prefab, Vector3.zero, Quaternion.identity, transform);
                obj.transform.GetChild(0).GetComponentInChildren<Image>().sprite = inventory.database.get_item[slot.item.id].inventory_icon;
                obj.GetComponent<RectTransform>().localPosition = getPosition(i);
                obj.GetComponentInChildren<TextMeshProUGUI>().text = slot.amount.ToString("n0");
                items_displayed.Add(slot, obj);
            }
        }
    }

    public void createDisplay()
    {
        for (int i = 0; i < inventory.container.items.Count; i++)
        {
            InventorySlot slot = inventory.container.items[i];

            var obj = Instantiate(inventory_prefab, Vector3.zero, Quaternion.identity, transform);
            obj.transform.GetChild(0).GetComponentInChildren<Image>().sprite = inventory.database.get_item[slot.item.id].inventory_icon;
            obj.GetComponent<RectTransform>().localPosition = getPosition(i);
            obj.GetComponentInChildren<TextMeshProUGUI>().text = slot.amount.ToString("n0");
            items_displayed.Add(slot, obj);
        }
    }

    public Vector3 getPosition(int i)
    {
        return new Vector3(x_start + (x_space_between_item * (i % num_columns)), y_start + (y_space_between_item * (i / num_columns)), 0f);
    }
    
}
