using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Storage : MonoBehaviour
{
    [SerializeField] public Transform Content;
    [SerializeField] int SlotsCount;
    internal List<Slot> slots = new List<Slot>();
    internal void SlotsCreating(string tag)
    {
        for (int i = 0; i < SlotsCount; i++)
        {
            AddItem(i);
            slots[i].LoadComponent();
            slots[i].tag = tag;
        }
    }
    internal void UpdateStorage()
    {
        foreach (Slot slot in slots)
        {
            slot.UpdateSlot();
        }
    }
    internal void AddItem(Item item)
    {
        for (int i = 0; i < slots.Count; i++)
        {
            if (item.IsStackable)
            {
                IEnumerable<Slot> items = slots.Where(x => x.item == item && item.Capacity > x.count);
                if (items.Count() > 0)
                {
                    Slot slot = items.First();
                    slot.count++;
                    slot.UpdateSlot(item);
                    break;
                }
            }
            if (slots[i].item == null)
            {
                slots[i].UpdateSlot(item);
                break;
            }
        }
    }
    internal void AddItem(int i)
    {
        StorageSetting.SlotPrefab.item = null;
        StorageSetting.SlotPrefab.id = i;
        slots.Add(Instantiate(StorageSetting.SlotPrefab, Content));
    }
}
