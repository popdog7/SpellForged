using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;
using TMPro;

public class DisplayInventory : MonoBehaviour
{
    [SerializeField] private InventorySO inventory;

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
        for (int i = 0; i < inventory.container.Count; i++)
        {
            if (items_displayed.ContainsKey(inventory.container[i]))
            {
                items_displayed[inventory.container[i]].GetComponentInChildren<TextMeshProUGUI>().text = inventory.container[i].amount.ToString("n0");
            }
            else
            {
                var obj = Instantiate(inventory.container[i].item.inventory_icon, Vector3.zero, Quaternion.identity, transform);
                obj.GetComponent<RectTransform>().localPosition = getPosition(i);
                obj.GetComponentInChildren<TextMeshProUGUI>().text = inventory.container[i].amount.ToString("n0");
                items_displayed.Add(inventory.container[i], obj);
            }
        }
    }

    public void createDisplay()
    {
        for (int i = 0; i < inventory.container.Count; i++)
        {
            var obj = Instantiate(inventory.container[i].item.inventory_icon, Vector3.zero, Quaternion.identity, transform);
            obj.GetComponent<RectTransform>().localPosition = getPosition(i);
            obj.GetComponentInChildren<TextMeshProUGUI>().text = inventory.container[i].amount.ToString("n0");
            items_displayed.Add(inventory.container[i], obj);
        }
    }

    public Vector3 getPosition(int i)
    {
        return new Vector3(x_start + (x_space_between_item * (i % num_columns)), y_start + (y_space_between_item * (i / num_columns)), 0f);
    }
}
