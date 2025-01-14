using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyManager : MonoBehaviour
{
    [SerializeField] private GameObject[] room_one_enemys_array;
    [SerializeField] private GameObject[] room_two_enemys_array;
    [SerializeField] private GameObject[] room_three_enemys_array;

    public void activateRoom(int  room)
    {
        switch (room)
        {
            case 1:
                activeFirstRoom();
                break;
            case 2:
                activeSecondRoom();
                break;
            case 3:
                activeThirdRoom();
                break;
            default:
                break;
        }
    }

    public void activeFirstRoom()
    {
        for (int i = 0; i < room_one_enemys_array.Length; i++)
        {
            room_one_enemys_array[i].SetActive(true);
        }
    }

    public void activeSecondRoom()
    {
        for (int i = 0; i < room_two_enemys_array.Length; i++)
        {
            room_two_enemys_array[i].SetActive(true);
        }
    }

    public void activeThirdRoom()
    {
        for (int i = 0; i < room_three_enemys_array.Length; i++)
        {
            room_three_enemys_array[i].SetActive(true);
        }
    }

    public bool isCleared(int num)
    {
        switch (num)
        {
            case 1:
                return true;
            case 2:
                for (int i = 0; i < room_one_enemys_array.Length; i++)
                {
                    if (room_one_enemys_array[i] != null)
                    {
                        return false;
                    }
                }
                return true;
            case 3:
                for (int i = 0; i < room_two_enemys_array.Length; i++)
                {
                    if (room_two_enemys_array[i] != null)
                    {
                        return false;
                    }
                }
                return true;
            case 4:
                for (int i = 0; i < room_three_enemys_array.Length; i++)
                {
                    if (room_three_enemys_array[i] != null)
                    {
                        return false;
                    }
                }
                return true;
            default:
                return false;
        }
    }
}
