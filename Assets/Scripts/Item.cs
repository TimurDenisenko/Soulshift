using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Inventory/Item")]
public class Item : ScriptableObject
{
    public string Name;
    public bool IsStackable;
    public int Capacity = 1;
    public Sprite Icon;
    public GameObject ItemObject;
    public virtual Item DeepCopy()
    {
        Item newItem = CreateInstance<Item>();
        CopyBaseParameters(newItem);
        return newItem;
    }
    protected void CopyBaseParameters<T>(T newItem) where T : Item
    {
        newItem.Name = Name;
        newItem.IsStackable = IsStackable;
        newItem.Capacity = Capacity;
        newItem.Icon = Icon;
    }
}
