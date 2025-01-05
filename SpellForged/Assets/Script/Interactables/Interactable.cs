using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    public string prompt_message;
    public bool use_events;

    public void BaseInteract()
    {
        Interact();
    }

    protected virtual void Interact()
    {

    }

}
