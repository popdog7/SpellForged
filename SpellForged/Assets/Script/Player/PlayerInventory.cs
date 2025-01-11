using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public InventorySO inventory;

    private void OnApplicationQuit()
    {
        inventory.container.items = new InventorySlot[24];
    }

    //TEMP TILL I IMPLEMENT THE REAL EVENT FROM GAME INPUT
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            inventory.Save();
        }

        if (Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            inventory.Load();
        }
    }

}
