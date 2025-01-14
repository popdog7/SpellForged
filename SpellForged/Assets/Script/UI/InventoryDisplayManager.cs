using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryDisplayManager : MonoBehaviour
{
    [SerializeField] private GameInput game_input;

    [SerializeField] private GameObject[] inventory_menus;
    private UserInterface[] inventory_menu_interfaces = new UserInterface[2];

    private bool isInventoryOpen;

    private void Awake()
    {
        isInventoryOpen = false;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Confined;

        for (int i = 0; i < inventory_menus.Length; i++)
        {
            inventory_menu_interfaces[i] = inventory_menus[i].GetComponent<UserInterface>();
        }

        game_input.OnToggleInventory += toggleInventory;
    }

    private void toggleInventory(object sender, System.EventArgs e)
    {
        isInventoryOpen = !isInventoryOpen;
        for (int i = 0; i < inventory_menus.Length; i++)
        {
            inventory_menus[i].SetActive(isInventoryOpen);
        }
        toggleCursor(isInventoryOpen);
        game_input.setIsInventoryOpen(isInventoryOpen);
    }

    private void toggleCursor(bool toggle)
    {
        Cursor.visible = toggle;
    }

    private void OnDestroy()
    {
        game_input.OnPause -= toggleInventory;
    }
}
