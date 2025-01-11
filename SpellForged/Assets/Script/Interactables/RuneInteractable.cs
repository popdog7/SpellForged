using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class RuneInteractable : Interactable, ISerializationCallbackReceiver
{
    [SerializeField] private ItemSO item;
    [SerializeField] private PlayerInventory player_inventory;

    public void OnAfterDeserialize()
    {
    }

    public void OnBeforeSerialize()
    {
        GetComponentInChildren<SpriteRenderer>().sprite = item.inventory_icon;
        EditorUtility.SetDirty(GetComponentInChildren<SpriteRenderer>());
    }

    protected override void Interact()
    {
        Debug.Log("Interacted with " + gameObject.name);
        player_inventory.inventory.AddItem(new item(item), 1);
        Destroy(gameObject);
    }
}
