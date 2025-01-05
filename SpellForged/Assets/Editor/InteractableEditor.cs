using UnityEditor;

[CustomEditor(typeof(Interactable), true)]
public class InteractableEditor : Editor
{
    public override void OnInspectorGUI()
    {
        Interactable interactable = (Interactable)target;
        base.OnInspectorGUI();

        if (interactable.use_events)
        {
            addInteractionEvents(interactable);
        }
        else
        {
            removeInteractionEvents(interactable);

        }
    }

    public void addInteractionEvents(Interactable interactable_obj)
    {
        if (interactable_obj.GetComponent<InteractionEvent>() == null)
        {
            interactable_obj.gameObject.AddComponent<InteractionEvent>();
        }
    }

    public void removeInteractionEvents(Interactable interactable_obj)
    {
        if (interactable_obj.GetComponent<InteractionEvent>() != null)
        {
            DestroyImmediate(interactable_obj.GetComponent<InteractionEvent>());
        }
    }
}
