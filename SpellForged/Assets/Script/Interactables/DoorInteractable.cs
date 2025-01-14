using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorInteractable : Interactable
{
    [SerializeField] private GameObject door;
    [SerializeField] private EnemyManager enemy_manager;
    public int room_num;
    protected override void Interact()
    {
        if(enemy_manager.isCleared(room_num))
        {
            door.SetActive(false);
            enemy_manager.activateRoom(room_num);
        }
    }
}
