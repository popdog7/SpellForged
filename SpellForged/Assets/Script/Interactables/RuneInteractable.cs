using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RuneInteractable : Interactable
{
    [SerializeField] private ItemSO item;
    [SerializeField] private PlayerInventory player_inventory;

    protected override void Interact()
    {
        Debug.Log("Interacted with " + gameObject.name);
        player_inventory.inventory.AddItem(item, 1);
        Destroy(gameObject);
    }
}
