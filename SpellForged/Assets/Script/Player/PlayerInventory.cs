using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public InventorySO inventory;

    private void OnApplicationQuit()
    {
        inventory.container.Clear();
    }

}
