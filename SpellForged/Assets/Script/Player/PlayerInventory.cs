using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public InventorySO inventory;
    public InventorySO equipment;

    private void OnApplicationQuit()
    {
        inventory.container.clear();
        equipment.container.clear();
    }

    //TEMP TILL I IMPLEMENT THE REAL EVENT FROM GAME INPUT
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            inventory.Save();
            equipment.Save();
        }

        if (Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            inventory.Load();
            equipment.Load();
        }
    }

}
