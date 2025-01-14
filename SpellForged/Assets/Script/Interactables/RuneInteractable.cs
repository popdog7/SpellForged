using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;

public class RuneInteractable : Interactable, ISerializationCallbackReceiver
{
    [SerializeField] private ItemSO item;
    [SerializeField] private ItemDatabaseSO databaseSO;
    [SerializeField] private InventorySO inventory;
    [SerializeField] private SpriteRenderer spriteRenderer;

    public int after_defaults = 3 ;

    private void Awake()
    {
        if (item == null)
            getRandomRune();
            updateSprite();

    }

    public void OnAfterDeserialize()
    {
    }

    public void OnBeforeSerialize()
    {
#if UNITY_EDITOR
        if (item != null)
        {
            GetComponentInChildren<SpriteRenderer>().sprite = item.inventory_icon;
            EditorUtility.SetDirty(GetComponentInChildren<SpriteRenderer>());
        }
#endif
    }

    protected override void Interact()
    {
        Debug.Log("Interacted with " + gameObject.name);

        if(inventory.AddItem(new item(item), 1))
        {
            Destroy(gameObject);
        }
    }

    public void getRandomRune()
    {
        int random_num = Random.Range(after_defaults, databaseSO.item_objecs.Length);
        item = databaseSO.item_objecs[random_num];
    }

    public void updateSprite()
    {
        spriteRenderer.sprite = item.inventory_icon;
    }
}
