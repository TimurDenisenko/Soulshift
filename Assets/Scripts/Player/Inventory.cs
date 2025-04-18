using Assets.Scripts.Player;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Inventory : Storage
{
    [SerializeField] public RectTransform inventoryScrollView;
    [SerializeField] public float secondPos;
    [SerializeField] public float secondScale;
    List<Item> defaultItemsInInventory;
    internal float defaultPos;
    internal float defaultScale;

    private void Start()
    {
        SlotsCreating("InventorySlot");
        defaultPos = inventoryScrollView.localPosition.x;
        defaultScale = inventoryScrollView.rect.width;
    }
    internal void SortBy(Type type)
    {
        defaultItemsInInventory = slots.Select(x => x.item?.DeepCopy() ?? null).ToList();
        for (int i = 0; i < slots.Count; i++)
        {
            slots[i].item = slots[i].item != null && slots[i].item.GetType() == type ? slots[i].item : null;
        }
        for (int i = 0; i < slots.Count; i++)
        {
            if (slots[i].item != null)
            {
                Slot emptySlot = slots.First(x => x.item == null);
                Item tempItem = emptySlot.item;
                emptySlot.item = slots[i].item;
                slots[i].item = tempItem;
            }
        }
        UpdateStorage();
    }
    internal void SetDefaultInventory()
    {
        for (int i = 0; i < slots.Count; i++)
        {
            slots[i].item = defaultItemsInInventory[i];
        }
        UpdateStorage();
    }
}
